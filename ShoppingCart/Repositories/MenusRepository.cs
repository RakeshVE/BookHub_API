using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class MenusRepository : IMenusRepository
    {
        private readonly ShoppingCartContext _context;
        public MenusRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetMenus()
        {
            return await _context.Menus.Select(x => x.MenuName).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSubMenuByName(string menu)
        {
            return await _context.Menus.Where(x => x.MenuName == menu).Select(x => x.SubMenu).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksBySubMenu(string menu)
        {
            var menuid = _context.Menus.Where(x => x.SubMenu == menu).Select(x => x.MenuId).FirstOrDefault();

            return await _context.Books.Where(y => y.MenuId == menuid).ToListAsync();
        }
    }
}
