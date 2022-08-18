using Microsoft.EntityFrameworkCore;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.DTO.DTOs;

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

        public async Task<IEnumerable<BookDto>> GetBooksBySubMenu(string menu)
        {
            var menuid = _context.Menus.Where(x => x.SubMenu == menu).Select(x => x.MenuId).FirstOrDefault();

            var books = await GetBookByMenuId(menuid);

            return books;
        }

        public async Task<IEnumerable<BookDto>> GetBookByMenuId(int id)
        {
            var book1 = await _context.Books.Where(x => x.MenuId == id).ToListAsync();
            List<BookDto> bookDto = new List<BookDto>();

            foreach (var items in book1)
            {
                BookDto _convImg = new BookDto();
                _convImg.BookId = items.BookId;
                if (items.Image != null)
                {
                    string imreBase64Data = Convert.ToBase64String(items.Image);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    _convImg.Image = imgDataURL;
                }

                _convImg.BookId = items.BookId;
                _convImg.Title = items.Title;
                _convImg.ListPrice = items.ListPrice;
                _convImg.OurPrice = items.OurPrice;
                _convImg.Rating = items.Rating;
                _convImg.ReviewCount = items.ReviewCount;
                _convImg.Details = items.Details;
                _convImg.ProductType = items.ProductType;
                _convImg.Description = items.Description;
                _convImg.SystemReq = items.SystemReq;
                _convImg.Demo = items.Demo;
                _convImg.IsActive = items.IsActive;
                _convImg.MenuId = items.MenuId;
                _convImg.IsBook = true;

                bookDto.Add(_convImg);
            }
            return bookDto;
        }
    }
}
