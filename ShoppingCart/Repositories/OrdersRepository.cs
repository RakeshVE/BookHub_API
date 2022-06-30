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

        public void AddToWishList(AddWishListDto wishlist)
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
            _context.SaveChanges();
        }
        public List<WishlistItemDto> GetWishListItemByUserId(int userId)
        {
            List<WishlistItemDto> _cart = new List<WishlistItemDto>();
            
            _cart = (from c in _context.Wishlists
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
                     }).ToList();
            return _cart;
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
