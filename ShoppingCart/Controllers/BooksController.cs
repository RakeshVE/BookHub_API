using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(IBooksRepository booksRepository, IWebHostEnvironment hostEnvironment)
        {
            _bookRepository = booksRepository;
            _webHostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            var books = await _bookRepository.GetBook();
            return Ok(books);
        }

        [HttpPost("UploadBook")]       
        public ActionResult UploadBook([FromForm] IFormFile file ,[FromQuery] BookDto book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _bookRepository.UploadBook(book, file);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }
          
            
        }      
        [HttpGet("GetBookById")]
        public async Task<ActionResult> GetBookById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("Invalid id");
                }
                var book = await _bookRepository.GetBookById(id);
                return Ok(book);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("UploadBookImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadBookImage([FromForm] IFormFile files, [FromQuery] int bookId)
        {
            try
            {
                //var file = Request.Form.Files[0];
                BookImage bookImage = new BookImage();
                var folderName = Path.Combine("Resources", "Images");
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    bookImage.BookId=bookId;
                    bookImage.ImageName = fileName;
                    bookImage.ImageUrl = fullPath;
                    _bookRepository.UploadBookImage(bookImage);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpGet("GetBookImage")]
        public async Task<IActionResult> GetBookImage(int bookId)
        {
            try
            {
               // string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Resource","Image");
                var booksImage = await _bookRepository.GetBookImage(bookId);
                return Ok(booksImage);
              
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }



        }

    }
}
