using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILoggerManager _loggerManager;
        public OrdersController(IOrdersRepository ordersRepository,ILoggerManager loggerManager)
        {
            _ordersRepository = ordersRepository;
            _loggerManager = loggerManager;
        }
        [HttpPost("AddBookToWishlist")]
        public async Task<ActionResult> AddBookToWishlist([FromBody]AddWishListDto wishlist)
        {
            try
            {
                if (wishlist is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _ordersRepository.AddToWishList(wishlist);
                return Ok();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpGet("GetWishListItem")]
        public async Task<ActionResult> GetWishListItem( int userId)
        {
            try
            {
                if (userId < 0)
                {
                    return BadRequest();
                }
                
              var wishListItem= await _ordersRepository.GetWishListItemByUserId(userId);
                return Ok(wishListItem);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpPost("ShippingDetails")]
        public async Task<ActionResult> ShippingDetails([FromBody] ShippingDto shipping)
        {
            try
            {
                if (shipping is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _ordersRepository.AddShippingDetails(shipping);
                return Ok();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders()
        {
            var orderdetail = await _ordersRepository.GetOrdersAsync();
            return Ok(orderdetail);
        }

        [HttpGet("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var orders = await _ordersRepository.GetOrdersStatus();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var orderdetail = await _ordersRepository.GetOrderByIdAsync(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            return Ok(orderdetail);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDetailDto orderdto)
        {
            var orderdetail = await _ordersRepository.AddOrderAsync(orderdto);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderdetail, Controller = "OrderDetail" }, orderdto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDetailDto orderdto, [FromRoute] int id)
        {
            await _ordersRepository.UpdateOrderAsync(id, orderdto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await _ordersRepository.DeleteOrderAsync(id);
            return Ok();
        }

        [HttpPost("Checkout")]
        public async Task<ActionResult<CheckOutDto>> Checkout([FromBody]int orderTotal)
        {
            try
            {
                if (orderTotal ==0)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var data = await _ordersRepository.CheckOut(orderTotal);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }
        }

        [HttpPost("AddOrderDetails")]
        public async Task<ActionResult> AddOrderDetails(int[] bookId, int userId, int checkoutId)
        {
            try
            {
                if(bookId.Length > 0 && userId > 0 && checkoutId > 0)
                {
                    await _ordersRepository.AddOrderDetails(bookId, userId, checkoutId);
                }

                else
                {
                    return BadRequest("Invalid Input");
                }
            }
            catch(Exception ex)
            {
                _loggerManager.LogError(ex.Message);
            }

            return Ok();
        }

        [HttpGet("GetOrdersPlaced")]
        public async Task<IEnumerable<OrderPlcedDTO>> GetOrdersPlaced (int userId)
        {
            var orders = await _ordersRepository.GetOrdersPlaced(userId);
            return orders;
        }

    }
}
