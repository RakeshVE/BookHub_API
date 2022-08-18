using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.DTO.DTOs;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepositories _cartRepositories;
        private readonly ShoppingCartContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CartController(ICartRepositories cartRepositories, ShoppingCartContext context, IWebHostEnvironment webHostEnvironment)
        {
            _cartRepositories = cartRepositories;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCart([FromBody] cartReqDto cart)
        {
            try
            {
                if (cart is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
              await  _cartRepositories.AddToCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpGet("GetItemToCart")]
        public async Task<ActionResult> GetItemToCart(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("Invalid id");
                }
                var book = await _cartRepositories.GetItemToCart(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPost("RemoveToCart")]
        public async Task<ActionResult> RemoveToCart([FromBody] cartReqDto cart)
        {
            try
            {
                if (cart is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _cartRepositories.RemoveToCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpPost("EmptyCart")]
        public async Task<ActionResult> EmptyCart([FromBody] int userId)
        {
            try
            {
                if (userId < 0)
                {
                    return BadRequest();
                }
                await _cartRepositories.EmptyCart(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpPost("UpdateCart")]
        public async Task<ActionResult> UpdateCart([FromBody] cartReqDto cart)
        {
            try
            {
                if (cart is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _cartRepositories.UpdateCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
    }
}
