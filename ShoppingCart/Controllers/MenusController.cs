using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;

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
        public async Task<ActionResult<Book>> GetBooksBySubMenu(string menu)
        {
            var books = await _menusRepository.GetBooksBySubMenu(menu);
            return Ok(books);
        }

    }
}
