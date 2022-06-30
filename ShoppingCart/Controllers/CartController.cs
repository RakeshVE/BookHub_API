using Microsoft.AspNetCore.Hosting;
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
        public ActionResult AddToCart([FromBody] cartReqDto cart)
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
                _cartRepositories.AddToCart(cart);
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
                var book = _cartRepositories.GetItemToCart(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPost("RemoveToCart")]
        public ActionResult RemoveToCart([FromBody] cartReqDto cart)
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
                _cartRepositories.RemoveToCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpPost("EmptyCart")]
        public ActionResult EmptyCart([FromBody] cartReqDto cart)
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
                _cartRepositories.EmptyCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
    }
}
