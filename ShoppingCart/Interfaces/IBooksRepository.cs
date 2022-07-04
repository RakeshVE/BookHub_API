using Microsoft.AspNetCore.Http;
using ShoppingCart.DTOs;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IBooksRepository
    {
        Task<IEnumerable<BookDto>> GetBook();
        void UploadBook(BookDto book, IFormFile formFile);
        Task<BookDto> GetBookById(int id);
        Task<BookDto> GetBookByMenuId(int id);

        void UploadBookImage(BookImage bookImage);
        Task<IEnumerable<Photo>> GetBookImage(int bookId);
        

    }
}
