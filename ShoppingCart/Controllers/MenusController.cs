using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using ShoppingCart.DTO.DTOs;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class MenusController : ControllerBase
    {
        private readonly IMenusRepository _menusRepository;

        public MenusController(IMenusRepository menusRepository)
        {
            _menusRepository = menusRepository;
        }

        [HttpGet("GetMenus")]
        public async Task<ActionResult<IEnumerable<string>>> GetMenus()
        {
            var menus = await _menusRepository.GetMenus();
            return Ok(menus);
        }

        [HttpGet("GetSubMenuByName")]
        public async Task<ActionResult<IEnumerable<string>>> GetSubMenuByName(string name)
        {
            var menu = await _menusRepository.GetSubMenuByName(name);
            return Ok(menu);
        }

        [HttpGet("GetBooksBySubMenu")]
        public async Task<ActionResult<BookDto>> GetBooksBySubMenu(string menu)
        {
            var books = await _menusRepository.GetBooksBySubMenu(menu);
            return Ok(books);
        }

        [HttpGet("GetBookByMenuId")]
        public async Task<ActionResult> GetBookByMenuId(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("Invalid id");
                }
                var book = await _menusRepository.GetBookByMenuId(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
