using ShoppingCart.DTOs;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IOrdersRepository
    {
        List<WishlistItemDto> GetWishListItemByUserId(int userId);
        void AddToWishList(AddWishListDto wishlist);
    }
}
