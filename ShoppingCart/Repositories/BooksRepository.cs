using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
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
            var book1 = await _context.Books.ToListAsync();
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


        public void UploadBook(BookDto book, IFormFile files)
        {
            Book _books = new Book();
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
                _books.BookId = book.BookId;
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
                _books.CreatedBy = 1;
            }
            _context.Books.Add(_books);
            _context.SaveChanges();
        }

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _context.Books.Where(x => x.BookId == id).SingleOrDefaultAsync();
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

        public void AddToCart(cartReqDto cart)
        {
            Cart _cart = new Cart();
            if (cart != null)
            {
             var   _cartchk = _context.Carts.Where(x => x.UserId == cart.UserId && x.BookId == cart.BookId).FirstOrDefault();
                if (_cartchk != null)
                {
                    _cartchk.IsActive = true;

                    _cartchk.ModifiedOn = DateTime.Now;
                    _cartchk.ModifiedBy = 1;
                    _context.Carts.Update(_cartchk);
                    _context.SaveChanges();
                }
                else
                {
                    _cart.UserId = cart.UserId;
                    _cart.BookId = cart.BookId;
                    _cart.Quantity = cart.Quantity;

                    _cart.CartTotal = cart.CartTotal;
                    _cart.DiscountPer = cart.DiscountPer;
                    _cart.NetPay = cart.NetPay;
                    _cart.IsActive = cart.IsActive;

                    _cart.CreatedOn = DateTime.Now;
                    _cart.CreatedBy = 1;
                    _context.Carts.Add(_cart);
                    _context.SaveChanges();
                }
              
            }
        }
        public void RemoveToCart(cartReqDto cart)
        {
            Cart _cart = new Cart();
            if (cart != null)
            {
                _cart = _context.Carts.Where(x => x.UserId == cart.UserId &&x.BookId== cart.BookId).FirstOrDefault();


                _cart.IsActive = false;

                _cart.ModifiedOn = DateTime.Now;
                _cart.ModifiedBy = 1;
            }
            _context.Carts.Update(_cart);
            _context.SaveChanges();
        }


        public List<CartDto> GetItemToCart(int userId)
        {

            // var Item = await _context.Carts.Where(x => x.UserId == userId).ToListAsync();
            //return Item;

            List<CartDto> _cart = new List<CartDto>();
            //_cart = (Cart)_context.Carts.Where(x => x.UserId == userId).ToListAsync();
            //_cart.Book = (Book)_context.Books.Where(x => x.BookId == _cart.BookId).ToListAsync();
            //return (IEnumerable<Cart>)_cart;

            _cart = (from c in _context.Carts
                     join b in _context.Books on c.BookId equals b.BookId
                     where c.UserId == userId
                     where c.IsActive==true
                     select new CartDto
                     {
                         CartId = c.CartId,
                         UserId = c.UserId,
                         BookId = c.BookId,
                         Quantity = c.Quantity,
                         CartTotal = c.CartTotal,
                         DiscountPer = c.DiscountPer,
                         NetPay = c.NetPay,
                         IsActive = c.IsActive,
                         Title = b.Title,
                         Image = b.Image,
                         OurPrice = b.OurPrice
                     }).ToList();
            return _cart;
        }
        public void EmptyCart(cartReqDto cart)
        {
            List<Cart> _cart = new List<Cart>();
            if (cart != null)
            {
                _cart = _context.Carts.Where(x => x.UserId == cart.UserId).ToList();

                foreach (var item in _cart)
                {
                    item.IsActive = false;
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = 1;
                    _context.Carts.Update(item);
                    _context.SaveChanges();
                }


            }
        }
    }
}
