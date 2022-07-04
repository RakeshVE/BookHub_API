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
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
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
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpPost("ShippingDetails")]
        public async Task<ActionResult> ShippingDetails([FromQuery] ShippingDto shipping)
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
                return StatusCode(500, $"Internal server error:{ex}");
            }
        }
    }
}
