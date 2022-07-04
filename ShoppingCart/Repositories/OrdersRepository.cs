using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class OrdersRepository: IOrdersRepository
    {
        private readonly ShoppingCartContext _context;

        public OrdersRepository(ShoppingCartContext context)
        {
            _context = context;
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
            
            _cart = await(from c in _context.Wishlists
                     join b in _context.Books on c.BookId equals b.BookId
                     where c.UserId == userId
                       select new WishlistItemDto
                     {
                         BookId=b.BookId,
                         Image= ToBase64String(b.Image),
                         BookName =b.Title,
                         ProductType=b.ProductType,
                         Price=b.OurPrice,
                         Rating=b.Rating                         
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
                    CreatedBy = 1
                };

                _context.Shippings.Add(shippingDetails);
                await _context.SaveChangesAsync();
            } 
            catch(Exception ex)
            {

            }
            
        }

        public async Task CheckOut(decimal totalOrder)
        {
            try
            {
                decimal tax = .18m;
                decimal shippingFee = 50.00m;
                decimal totalTax = totalOrder * (tax);
                decimal total = totalOrder + totalTax;
                CheckOutDto checkOutDto = new CheckOutDto();

                var checkout = new Checkout 
                {
                    CouponId = null,
                    UserId = 1,
                    Tax = totalTax,
                    Shipping = shippingFee,
                    FinalPay = total+ shippingFee,
                    CreatedOn = DateTime.Now,
                    CreatedBy = 1

                };
                _context.Checkouts.Add(checkout);
                await _context.SaveChangesAsync();
                
            }
            catch(Exception ex)
            {

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

        

    }
}
