using Microsoft.AspNetCore.Http;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BLL.Class
{
    public class BooksBL
    {
        private readonly IBooksRepository _bookRepository;

        public BooksBL(IBooksRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IEnumerable<BookDto>> GetBook() {
            return await _bookRepository.GetBook();
        }
        public async Task<IEnumerable<BookDto>> GetAllBook() {
            return await _bookRepository.GetAllBook();
        }
        public void UploadBook(BookDto book, IFormFile formFile) {
              _bookRepository.UploadBook(book,formFile);
        }
        public async Task<BookDto> GetBookById(int id, int userId) {
            return await _bookRepository.GetBookById(id,userId);
        }
        public async Task<BookDto> GetBookByMenuId(int id) {
            return await _bookRepository.GetBookByMenuId(id);
        }

        public  void UploadBookImage(BookImageDto bookImage){

            _bookRepository.UploadBookImage(bookImage);
        }
          public async Task<IEnumerable<PhotoDto>> GetBookImage(int bookId) {
          return await  _bookRepository.GetBookImage(bookId);
        }

        public async Task<dynamic> BindDropDown(string menuName) {
            return await _bookRepository.BindDropDown(menuName);
        }
        public async Task<IEnumerable<BookDto>> ApplyFilterOnBooks(FilterResults filterResults) {
            return await _bookRepository.ApplyFilterOnBooks(filterResults);
        }
        public async Task<IEnumerable<BookDto>> SearchBook(string bookName) {
            return await _bookRepository.SearchBook(bookName);
        }

        public async Task<string> UpdateStatus(int bookId) {

            return await _bookRepository.UpdateStatus(bookId);
        }
        public async Task<string> UpdateBook(BookDto book) {
            return await _bookRepository.UpdateBook(book);
        }
    }
}
