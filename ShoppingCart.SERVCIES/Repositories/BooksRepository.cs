using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Globalization;
using CloudinaryDotNet;
using Newtonsoft.Json.Linq;
//using CloudinaryDotNet.Actions;
//using UploadResult = ShoppingCart.Models.UploadResult;

namespace ShoppingCart.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ShoppingCartContext _context;
       
        public BooksRepository(ShoppingCartContext context)
        {
            _context = context;           
        }
        public async Task<IEnumerable<BookDto>> GetBook()
        {
            var book = await _context.Books.Where(x=>x.IsActive==true).ToListAsync();
            var bookDto = BindBook(book);
            return bookDto;
        }
        public async Task<IEnumerable<BookDto>> GetAllBook()
        {
            var book = await _context.Books.ToListAsync();
            var bookDto = BindBook(book);
            return bookDto;
        }
        public void UploadBook(BookDto book, IFormFile files)
        {
            Book _books = new Book();
            try
            {
                if (book != null)
                {
                    _books.Title = book.Title;

                    if (files.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            _books.Image = fileBytes;
                        }
                    }
                    // _books.BookId = book.BookId;
                    _books.ListPrice = book.ListPrice;
                    _books.OurPrice = book.OurPrice;
                    _books.Rating = book.Rating;
                    _books.ReviewCount = book.ReviewCount;
                    _books.Details = book.Details;
                    _books.ProductType = book.ProductType;
                    _books.Description = book.Description;
                    _books.SystemReq = book.SystemReq;
                    _books.Demo = book.Demo;
                    _books.IsActive = book.IsActive;
                    _books.MenuId = book.MenuId;
                    _books.IsBook = book.IsBook;
                    _books.CreatedOn = DateTime.Now;
                    _books.CreatedBy = book.CreatedBy;
                    _books.ContentType = book.ContentType;
                    _books.Certification = book.Certification;
                    _books.Publisher = book.Publisher;
                }
                _context.Books.Add(_books);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<BookDto> GetBookById(int id, int userId)
        {
            var book = await _context.Books.Include(x => x.Wishlists.Where(u=>u.UserId==userId)).Where(x => x.BookId == id && x.IsActive==true ).FirstOrDefaultAsync();
            
            BookDto _convImg = new BookDto();
            if (book is not null)
            {
                if (book.Image != null)
                {
                    string imreBase64Data = Convert.ToBase64String(book.Image);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    _convImg.Image = imgDataURL;
                }
                _convImg.BookId = book.BookId;
                _convImg.Title = book.Title;
                _convImg.ListPrice = book.ListPrice;
                _convImg.OurPrice = book.OurPrice;
                _convImg.Rating = book.Rating;
                _convImg.ReviewCount = book.ReviewCount;
                _convImg.Details = book.Details;
                _convImg.ProductType = book.ProductType;
                _convImg.Description = book.Description;
                _convImg.SystemReq = book.SystemReq;
                _convImg.Demo = book.Demo;
                _convImg.IsActive = book.IsActive;
                _convImg.MenuId = book.MenuId;
                _convImg.IsBook = true;
                _convImg.Publisher = book.Publisher;
                _convImg.Certification = book.Certification;
                _convImg.ContentType = book.ContentType;
                if (book.Wishlists.Count == 0)
                {
                    _convImg.WishlistAdded = true;
                }

                return _convImg;
            }
            return _convImg;
        }

        public async Task<BookDto> GetBookByMenuId(int id)
        {
            var book = await _context.Books.Where(x => x.MenuId == id).SingleOrDefaultAsync();
            BookDto _convImg = new BookDto();
            if (book is not null)
            {
                if (book.Image != null)
                {
                    string imreBase64Data = Convert.ToBase64String(book.Image);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    _convImg.Image = imgDataURL;
                }
                _convImg.BookId = book.BookId;
                _convImg.Title = book.Title;
                _convImg.ListPrice = book.ListPrice;
                _convImg.OurPrice = book.OurPrice;
                _convImg.Rating = book.Rating;
                _convImg.ReviewCount = book.ReviewCount;
                _convImg.Details = book.Details;
                _convImg.ProductType = book.ProductType;
                _convImg.Description = book.Description;
                _convImg.SystemReq = book.SystemReq;
                _convImg.Demo = book.Demo;
                _convImg.IsActive = book.IsActive;
                _convImg.MenuId = book.MenuId;
                _convImg.IsBook = true;
                return _convImg;
            }
            return _convImg;
        }

        public void UploadBookImage(BookImage book)
        {
            _context.BookImages.Add(book);
            _context.SaveChanges();

        }
        public async Task<IEnumerable<Photo>> GetBookImage(int bookId)
        {
            //var bookImages = await _context.BookImages.Where(x => x.BookId == bookId).ToListAsync();
            //return bookImages;
            var cloudimage = await _context.Photos.Where(x => x.BookId == bookId).ToListAsync();
            return cloudimage;
        }

        public async Task<dynamic> BindDropDown(string menuName)
        {          
            if (menuName == "undefined" || menuName == null)
            {
                var certification = await _context.Books.Select(x => x.Certification).Distinct().ToListAsync();
                var contentType = await _context.Books.Select(x => x.ContentType).Distinct().ToListAsync();
                var publisher = await _context.Books.Select(x => x.Publisher).Distinct().ToListAsync();
                var data = new
                {
                    Certifications = certification,
                    ContentType = contentType,
                    Publishers = publisher
                };
                return data;
            }
            else
            {

                int menuId = _context.Menus.Where(x => x.SubMenu == menuName).Select(x => x.MenuId).FirstOrDefault();
                var certification = await _context.Books.Where(x => x.MenuId == menuId).Select(x => x.Certification).Distinct().ToListAsync();
                var contentType = await _context.Books.Where(x => x.MenuId == menuId).Select(x => x.ContentType).Distinct().ToListAsync();
                var publisher = await _context.Books.Where(x => x.MenuId == menuId).Select(x => x.Publisher).Distinct().ToListAsync();
                var data = new
                {
                    Certifications = certification,
                    ContentType = contentType,
                    Publishers = publisher
                };

                return data;

            }
        }

        private async Task<IEnumerable<dynamic>> GetResults(FilterResults filterResults)
        {
            decimal minValue = 0;
            decimal maxValue = 0;
            switch (filterResults.PriceRange)
            {
                case "10 to 50":
                    minValue = 10; maxValue = 50;
                    break;
                case "50 to 100":
                    minValue = 50; maxValue = 100;
                    break;
                case "100 to 200":
                    minValue = 100; maxValue = 200;
                    break;
                case "200 to 300":
                    minValue = 200; maxValue = 300;
                    break;
            }

            if (filterResults.Certifications != "" && filterResults.ContentType != "" && filterResults.Publishers != "" && filterResults.PriceRange != "" &&
                filterResults.Certifications != null && filterResults.ContentType != null && filterResults.Publishers != null && filterResults.PriceRange != null ) 
            {
                var book = await _context.Books.Where(x => x.Certification == filterResults.Certifications && x.ContentType == filterResults.ContentType &&
                               x.Publisher == filterResults.Publishers && x.OurPrice >= minValue && x.OurPrice <= maxValue).ToListAsync();
                return book;
            }
            else if((filterResults.Certifications == "" && filterResults.ContentType == "" && filterResults.Publishers == "" && filterResults.PriceRange == "") ||
                (filterResults.Certifications == null && filterResults.ContentType == null && filterResults.Publishers == null && filterResults.PriceRange == null))
            {
                var book = await _context.Books.ToListAsync();
                return book;
            }
            else
            {            
                var book = await _context.Books.Where(x => (string.IsNullOrWhiteSpace(filterResults.Certifications) || x.Certification == filterResults.Certifications) && 
                (string.IsNullOrWhiteSpace(filterResults.ContentType) || x.ContentType == filterResults.ContentType)
                && ( string.IsNullOrWhiteSpace(filterResults.Publishers) || x.Publisher == filterResults.Publishers)&& (minValue == 0 || x.OurPrice >= minValue)
                && (maxValue == 0 || x.OurPrice <= maxValue)).ToListAsync();

                 return book;

            }

        }


        public async Task<IEnumerable<BookDto>> ApplyFilterOnBooks(FilterResults filterResults)
        {
            var book =await GetResults(filterResults);
            var bookDto = BindBook(book);            

            return bookDto;
        }


        public async Task<IEnumerable<BookDto>> SearchBook(string bookName)
        {
            var book = await _context.Books.Where(x => x.Title.Contains(bookName) && x.IsActive==true).ToListAsync();
            var bookDto = BindBook(book);

            return bookDto;

        }
        private IEnumerable<BookDto> BindBook(IEnumerable<dynamic> book)
        {
            List<BookDto> bookDto = new List<BookDto>();           

            foreach (var items in book)
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

            return  bookDto;

        }

        public async Task<string> UpdateBook(BookDto book)
        {
            string status = "";
            var result = await _context.Books.FirstOrDefaultAsync(e => e.BookId == book.BookId);

            if(result != null)
            {
                result.Title = book.Title;
                result.ListPrice = book.ListPrice;
                result.OurPrice = book.OurPrice;
                result.Rating = book.Rating;
                result.ReviewCount = book.ReviewCount;
                result.Details = book.Details;
                result.ProductType = book.ProductType;
                result.Description = book.Description;
                result.SystemReq = book.SystemReq;
                result.Demo = book.Demo;
                result.IsActive = book.IsActive;
                result.MenuId = book.MenuId;
                result.IsBook = book.IsBook;
                result.CreatedOn = DateTime.Now;
                result.CreatedBy = book.CreatedBy;
                result.ContentType = book.ContentType;
                result.Certification = book.Certification;
                result.Publisher = book.Publisher;
                
                
                await _context.SaveChangesAsync();
                status = "Updated Successfully";
              //  return status;

            }
            

            return status;
        }


        public async Task<string> UpdateStatus(int bookId)
        {
            Book book = new Book();
            string status = "";
            

            book = await _context.Books.Where(x => x.BookId == bookId).FirstOrDefaultAsync();

            try 
            {
                if (book.IsActive != null)
                {
                    if (book.IsActive==true)
                    {
                        book.IsActive = false;

                    }
                    else
                    {
                        book.IsActive = true;
                    }

                    _context.Books.Update(book);
                    await _context.SaveChangesAsync();
                    status = "Updated Successfully";
                }

            }
            catch (Exception ex) 
            {
                status= "Error";
            }

            return status;
        }

    }
}
