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
        Task<List<OrderDetailDto>> GetOrdersAsync();
        Task<OrderDetailDto> GetOrderByIdAsync(int id);
        Task<int> AddOrderAsync(OrderDetailDto orderdto);
        Task UpdateOrderAsync(int id, OrderDetailDto orderdto);
        //Task UpdateOrderPatchAsync(int id, JsonPatchDocument orderdto);
        Task DeleteOrderAsync(int id);
        Task <List<OrderStatusDto>> GetOrdersStatus();
        Task CheckOut(decimal totalOrder);


    }
}
