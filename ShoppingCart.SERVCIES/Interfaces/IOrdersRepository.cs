using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.DTO.DTOs;

namespace ShoppingCart.Interfaces
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<WishlistItemDto>> GetWishListItemByUserId(int userId);
        Task AddToWishList(AddWishListDto wishlist);
        Task AddShippingDetails(ShippingDto shipping);
        Task<OrderDetailDto> GetOrderByIdAsync(int id);
        Task<int> AddOrderAsync(OrderDetailDto orderdto);
        Task UpdateOrderAsync(int id, OrderDetailDto orderdto);
        //Task UpdateOrderPatchAsync(int id, JsonPatchDocument orderdto);
        Task DeleteOrderAsync(int id);
        Task <List<OrderStatusDto>> GetOrdersStatus();
        Task<CheckOutDto> CheckOut(decimal totalOrder,int userId);
        Task AddOrderDetails(int[] bookId, int userId, int checkoutId);
        Task<IEnumerable<OrderPlcedDTO>> GetOrdersPlaced(int userId);
        Task<List<AdminOrderDetailsDto>> GetOrdersAsync();
        Task AddPaymentDetails(PaymentDto payment);

    }
}
