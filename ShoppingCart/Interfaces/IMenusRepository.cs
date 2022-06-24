using ShoppingCart.DTOs;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IMenusRepository
    {
        Task<IEnumerable<string>> GetMenus();
        Task<IEnumerable<string>> GetSubMenuByName(string menu);
        Task<IEnumerable<Book>> GetBooksBySubMenu(string menu);
    }
}
