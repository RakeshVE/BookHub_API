using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
   
    public class CartRepositories: ICartRepositories
    {
        private readonly ShoppingCartContext _context;

        public CartRepositories(ShoppingCartContext context)
        {
            _context = context;
        }
        public void AddToCart(cartReqDto cart)
        {
            Cart _cart = new Cart();
            if (cart != null)
            {
                var _cartchk = _context.Carts.Where(x => x.UserId == cart.UserId && x.BookId == cart.BookId).FirstOrDefault();
                if (_cartchk != null)
                {
                    _cartchk.IsActive = true;

                    _cartchk.ModifiedOn = DateTime.Now;
                    _cartchk.ModifiedBy = 1;
                    _context.Carts.Update(_cartchk);
                    _context.SaveChanges();
                }
                else
                {
                    _cart.UserId = cart.UserId;
                    _cart.BookId = cart.BookId;
                    _cart.Quantity = cart.Quantity;

                    _cart.CartTotal = cart.CartTotal;
                    _cart.DiscountPer = cart.DiscountPer;
                    _cart.NetPay = cart.NetPay;
                    _cart.IsActive = true;

                    _cart.CreatedOn = DateTime.Now;
                    _cart.CreatedBy = 1;
                    _context.Carts.Add(_cart);
                    _context.SaveChanges();
                }

            }
        }
        public void RemoveToCart(cartReqDto cart)
        {
            Cart _cart = new Cart();
            if (cart != null)
            {
                _cart = _context.Carts.Where(x => x.UserId == cart.UserId && x.BookId == cart.BookId).FirstOrDefault();


                _cart.IsActive = false;

                _cart.ModifiedOn = DateTime.Now;
                _cart.ModifiedBy = 1;
            }
            _context.Carts.Update(_cart);
            _context.SaveChanges();
        }


        public List<CartDto> GetItemToCart(int userId)
        {

            // var Item = await _context.Carts.Where(x => x.UserId == userId).ToListAsync();
            //return Item;

            List<CartDto> _cart = new List<CartDto>();
            //_cart = (Cart)_context.Carts.Where(x => x.UserId == userId).ToListAsync();
            //_cart.Book = (Book)_context.Books.Where(x => x.BookId == _cart.BookId).ToListAsync();
            //return (IEnumerable<Cart>)_cart;

            _cart = (from c in _context.Carts
                     join b in _context.Books on c.BookId equals b.BookId
                     where c.UserId == userId
                     where c.IsActive == true
                     select new CartDto
                     {
                         CartId = c.CartId,
                         UserId = c.UserId,
                         BookId = c.BookId,
                         Quantity = c.Quantity,
                         CartTotal = c.CartTotal,
                         DiscountPer = c.DiscountPer,
                         NetPay = c.NetPay,
                         IsActive = c.IsActive,
                         Title = b.Title,
                         Image = ToBase64String(b.Image),
                         OurPrice = b.OurPrice
                     }).ToList();
            return _cart;
        }
        public void EmptyCart(cartReqDto cart)
        {
            List<Cart> _cart = new List<Cart>();
            if (cart != null)
            {
                _cart = _context.Carts.Where(x => x.UserId == cart.UserId).ToList();

                foreach (var item in _cart)
                {
                    item.IsActive = false;
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = 1;
                    _context.Carts.Update(item);
                    _context.SaveChanges();
                }


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
