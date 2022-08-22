using Microsoft.AspNetCore.Http;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.DTO.DTOs;

namespace ShoppingCart.Interfaces
{
    public interface IBooksRepository
    {
        Task<IEnumerable<BookDto>> GetBook();
        Task<IEnumerable<BookDto>> GetAllBook();
        void UploadBook(BookDto book, IFormFile formFile);
        Task<BookDto> GetBookById(int id, int userId);
        Task<BookDto> GetBookByMenuId(int id);

        void UploadBookImage(BookImageDto bookImage);
        Task<IEnumerable<PhotoDto>> GetBookImage(int bookId);

        Task<dynamic> BindDropDown(string menuName);
        Task<IEnumerable<BookDto>> ApplyFilterOnBooks(FilterResults filterResults);
        Task<IEnumerable<BookDto>> SearchBook(string bookName);

        Task<string> UpdateStatus(int bookId);
        Task<string> UpdateBook(BookDto book);
    }
}
