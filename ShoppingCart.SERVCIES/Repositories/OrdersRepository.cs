
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ShoppingCart.DTO.DTOs;

namespace ShoppingCart.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;

        public OrdersRepository(ShoppingCartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddToWishList(AddWishListDto wishlist)
        {
            var wishlistItem = new Wishlist
            {
                BookId = wishlist.BookId,
                UserId = wishlist.UserID,
                IsLiked = wishlist.IsLiked,
                CreatedOn = wishlist.CreatedOn,
                CreatedBy = wishlist.CreatedBy
            };
            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<WishlistItemDto>> GetWishListItemByUserId(int userId)
        {
            List<WishlistItemDto> _cart = new List<WishlistItemDto>();
            _cart = await (from c in _context.Wishlists
                           join b in _context.Books on c.BookId equals b.BookId
                           where c.UserId == userId
                           select new WishlistItemDto
                           {
                               BookId = b.BookId,
                               Image = ToBase64String(b.Image),
                               BookName = b.Title,
                               ProductType = b.ProductType,
                               Price = b.OurPrice,
                               Rating = b.Rating
                           }).ToListAsync();

            return _cart;
        }
        public async Task AddShippingDetails(ShippingDto shipping)
        {
            try
            {
                var shippingDetails = new Shipping
                {
                    CheckoutId = shipping.CheckoutId,
                    FirstName = shipping.FirstName,
                    LastName = shipping.LastName,
                    Address = shipping.Address,
                    City = shipping.City,
                    State = shipping.State,
                    Country = shipping.Country,
                    ZipCode = shipping.ZipCode,
                    Phone = shipping.Phone,
                    AddressType = shipping.AddressType,
                    CreatedOn = DateTime.Now,
                    CreatedBy = shipping.CreatedBy
                };
                _context.Shippings.Add(shippingDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<List<AdminOrderDetailsDto>> GetOrdersAsync()
        {
            List<AdminOrderDetailsDto> _order = new List<AdminOrderDetailsDto>();
            _order = await (from c in _context.OrderDetails
                           join b in _context.Books on c.BookId equals b.BookId
                           //  where c.UserId == userId o
                           orderby c.OrderId descending

                           select new AdminOrderDetailsDto
                           {
                               BookId = b.BookId,
                               OrderId=c.OrderId,
                               ImageUrl = ToBase64String(b.Image),
                               BookName = b.Title,
                               ProductType = b.ProductType,
                               OurPrice = b.OurPrice,
                               Rating = b.Rating,
                               CreatedOn= String.Format("{0:dd/MM/yyyy}", c.CreatedOn)


                           }).ToListAsync();

            return _order;
            //var orderdetail = await _context.OrderDetails.ToListAsync();
            //return _mapper.Map<List<AdminOrderDetailsDto>>(orderdetail);
        }
        public async Task<OrderDetailDto> GetOrderByIdAsync(int id)
        {
            var orderdetail = await _context.OrderDetails.FindAsync(id);
            return _mapper.Map<OrderDetailDto>(orderdetail);
        }

        public async Task<int> AddOrderAsync(OrderDetailDto orderdto)
        {
            var addorder = new OrderDetail()
            {
                OrderId = orderdto.OrderId,
                CheckoutId = orderdto.CheckoutId,
                BookId = orderdto.BookId,
                CreatedOn = orderdto.CreatedOn,
                CreatedBy = orderdto.CreatedBy,
                ModifiedOn = orderdto.ModifiedOn,
                ModifiedBy = orderdto.ModifiedBy
            };
            _context.OrderDetails.Add(addorder);
            await _context.SaveChangesAsync();
            return addorder.OrderId;
        }
        public async Task UpdateOrderAsync(int id, OrderDetailDto orderdto)
        {
            var orderdetail = await _context.OrderDetails.FindAsync(id);
            if (orderdetail != null)
            {
                orderdetail.CheckoutId = orderdto.CheckoutId;
                orderdetail.BookId = orderdto.BookId;
                orderdetail.CreatedOn = orderdto.CreatedOn;
                orderdetail.CreatedBy = orderdto.CreatedBy;
                orderdetail.ModifiedOn = orderdto.ModifiedOn;
                orderdetail.ModifiedBy = orderdto.ModifiedBy;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteOrderAsync(int id)
        {
            var orderdetail = new OrderDetail() { OrderId = id };
            _context.OrderDetails.Remove(orderdetail);
            await _context.SaveChangesAsync();
        }
        public async Task<List<OrderStatusDto>> GetOrdersStatus()
        {
            var orderList = (from b in _context.Books
                             join o in _context.OrderDetails on b.BookId equals o.BookId
                             join i in _context.BookImages on b.BookId equals i.BookId
                             select new OrderStatusDto()
                             {
                                 OrderId = o.OrderId,
                                 CheckoutId = o.CheckoutId,
                                 BookId = b.BookId,
                                 Title = b.Title,
                                 ListPrice = b.ListPrice,
                                 OurPrice = b.OurPrice,
                                 ProductType = b.ProductType,
                                 ImageUrl = ToBase64String(b.Image),
                                 ImageName = i.ImageName,
                                 CreatedBy = o.CreatedBy,
                                 ModifiedBy = o.ModifiedBy
                             }).ToListAsync();
            return await orderList;
        }

        public async Task<CheckOutDto> CheckOut(decimal totalOrder, int userId)
        {
            try
            {
                decimal tax = .18m;
                decimal shippingFee = 50.00m;
                decimal totalTax = totalOrder * (tax);
                decimal total = totalOrder + totalTax;

                var checkout = new Checkout
                {
                    CouponId = null,
                    UserId = userId,
                    Tax = totalTax,
                    Shipping = shippingFee,
                    FinalPay = total + shippingFee,
                    CreatedOn = DateTime.Now,
                    CreatedBy = 1

                };
                _context.Checkouts.Add(checkout);
                await _context.SaveChangesAsync();

                CheckOutDto checkOutDto = new CheckOutDto
                {
                    CouponId = checkout.CouponId,
                    CreatedBy = checkout.CreatedBy,
                    CreatedOn = checkout.CreatedOn,
                    FinalPay = checkout.FinalPay,
                    Shipping = checkout.Shipping,
                    Tax = checkout.Tax,
                    UserId = checkout.UserId,
                    CheckoutId = checkout.CheckoutId
                };

                return checkOutDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string ToBase64String(byte[] inArray)
        {
            string imgbase64 = "";
            if (inArray != null)
            {
                string imreBase64Data = Convert.ToBase64String(inArray);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                imgbase64 = imgDataURL;
            }
            return imgbase64;
        }

        public async Task AddOrderDetails([FromBody] int[] bookId, int userId, int checkoutId)
        {
            var length = bookId.Length;

            for (int i = 0; i < length; i++)
            {
                var cart = _context.Carts.Where(x => x.UserId == userId && x.IsActive == true && x.BookId == bookId[i]).FirstOrDefault();

                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = checkoutId,
                    CheckoutId = checkoutId,
                    BookId = bookId[i],
                    UserId = userId,
                    Status = "Placed",
                    Quantity = cart.Quantity,
                    Price = (cart.NetPay * cart.Quantity * 1.18m) + 50,
                    CreatedOn = DateTime.Now,
                    CreatedBy = userId
                };

                _context.OrderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderPlcedDTO>> GetOrdersPlaced(int userId)
        {
            List<OrderPlcedDTO> orderPlcedDTO = new List<OrderPlcedDTO>();

            orderPlcedDTO = await (from o in _context.OrderDetails
                                   join
                                   b in _context.Books on o.BookId equals b.BookId
                                   where o.UserId == userId
                                   select new OrderPlcedDTO
                                   {
                                       CheckoutId = o.CheckoutId,
                                       BookId = o.BookId,
                                       UserId = o.UserId,
                                       Status = o.Status,
                                       Title = b.Title,
                                       OurPrice = b.OurPrice,
                                       CreatedOn = o.CreatedOn
                                   }).OrderByDescending(x => x.CreatedOn).ToListAsync();
            return orderPlcedDTO;
        }

        public async Task AddPaymentDetails(PaymentDto payment)
        {
            try
            {
                var paymentdetails = new PaymentDetail
                {
                    CheckoutId = payment.CheckoutId,
                    UserId = payment.UserId,
                    TransactionId= payment.TransactionId,
                    TransactionType= payment.TransactionType,
                    PaymentMode= payment.PaymentMode,
                    Status= payment.Status,
                    Amount= payment.Amount,

                    CreatedOn = DateTime.Now,
                    CreatedBy = payment.CreatedBy
                };
                _context.PaymentDetails.Add(paymentdetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

