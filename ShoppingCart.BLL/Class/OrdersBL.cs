using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BLL.Class
{
    public class OrdersBL
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersBL(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task AddToWishList(AddWishListDto wishlist)
        {
             await _ordersRepository.AddToWishList(wishlist);

        }
        public async Task<IEnumerable<WishlistItemDto>> GetWishListItemByUserId(int userId)
        {
            return await _ordersRepository.GetWishListItemByUserId(userId);
        }
        public async Task AddShippingDetails(ShippingDto shipping)
        {
             await _ordersRepository.AddShippingDetails(shipping);

        }
        public async Task<List<AdminOrderDetailsDto>> GetOrdersAsync()
        {
            return await _ordersRepository.GetOrdersAsync();

        }
        public async Task<OrderDetailDto> GetOrderByIdAsync(int id)
        {
            return await _ordersRepository.GetOrderByIdAsync(id);

        }

        public async Task<int> AddOrderAsync(OrderDetailDto orderdto)
        {
            return await _ordersRepository.AddOrderAsync(orderdto);

        }
        public async Task UpdateOrderAsync(int id, OrderDetailDto orderdto)
        {
             await _ordersRepository.UpdateOrderAsync(id,orderdto);

        }
        public async Task DeleteOrderAsync(int id)
        {
             await _ordersRepository.DeleteOrderAsync(id);

        }
        public async Task<List<OrderStatusDto>> GetOrdersStatus()
        {
            return await _ordersRepository.GetOrdersStatus();

        }

        public async Task<CheckOutDto> CheckOut(decimal totalOrder, int userId)
        {
            return await _ordersRepository.CheckOut(totalOrder,userId);


        }


        public async Task AddOrderDetails([FromBody] int[] bookId, int userId, int checkoutId)
        {
             await _ordersRepository.AddOrderDetails(bookId, userId,checkoutId);

        }

        public async Task<IEnumerable<OrderPlcedDTO>> GetOrdersPlaced(int userId)
        {
            return await _ordersRepository.GetOrdersPlaced( userId);

        }
    }
}
