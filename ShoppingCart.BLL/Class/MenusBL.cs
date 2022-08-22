using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BLL.Class
{
    public class MenusBL
    {
        private readonly IMenusRepository _menusRepository;

        public MenusBL(IMenusRepository menusRepository)
        {
            _menusRepository = menusRepository;
        }

        public async Task<IEnumerable<string>> GetMenus()
        {
            return await _menusRepository.GetMenus();
        }

        public async Task<IEnumerable<string>> GetSubMenuByName(string menu)
        {
            return await _menusRepository.GetSubMenuByName(menu);

        }

        public async Task<IEnumerable<BookDto>> GetBooksBySubMenu(string menu)
        {
            return await _menusRepository.GetBooksBySubMenu(menu);

        }

        public async Task<IEnumerable<BookDto>> GetBookByMenuId(int id)
        {
            return await _menusRepository.GetBookByMenuId(id);

        }
    }
}
