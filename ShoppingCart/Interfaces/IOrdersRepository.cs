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
        Task<IEnumerable<WishlistItemDto>> GetWishListItemByUserId(int userId);
        Task AddToWishList(AddWishListDto wishlist);
        Task AddShippingDetails(ShippingDto shipping);
    }
}
