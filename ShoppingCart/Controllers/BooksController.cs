using Microsoft.AspNetCore.Authorization;
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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UploadResult = ShoppingCart.Models.UploadResult;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _bookRepository;
        private readonly ShoppingCartContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string Tags = "backend_PhotoAlbum";

        private readonly Cloudinary _cloudinary;

        public BooksController(IBooksRepository booksRepository, IWebHostEnvironment hostEnvironment, Cloudinary cloudinary, ShoppingCartContext context)
        {
            _bookRepository = booksRepository;
            _webHostEnvironment = hostEnvironment;
            _context = context;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            var books = await _bookRepository.GetBook();
            return Ok(books);
        }

        [HttpPost("UploadBook")]
        public ActionResult UploadBook([FromForm] IFormFile file, [FromQuery] BookDto book)
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpGet("GetBookById")]
        public async Task<ActionResult> GetBookById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("Invalid id");
                }
                var book = await _bookRepository.GetBookById(id);
                return Ok(book);
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
                var booksImage = await _bookRepository.GetBookImage(bookId);
                return Ok(booksImage);                             
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
        [HttpPost("ImageUploadCloud")]
        public async Task<IActionResult> ImageUploadCloud([FromForm] IFormFile image,int bookId)
        {
            var results = new List<Dictionary<string, string>>();

            if (image == null || image.Length == 0)
            {
                return BadRequest("Server error");
            }

            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            //foreach (var image in images)
            //{
                if (image.Length == 0) return BadRequest("Server error");

                var result = await _cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(image.FileName,
                        image.OpenReadStream()),
                    Tags = Tags
                }).ConfigureAwait(false);

                var imageProperties = new Dictionary<string, string>();
                foreach (var token in result.JsonObj.Children())
                {
                    if (token is JProperty prop)
                    {
                        imageProperties.Add(prop.Name, prop.Value.ToString());
                    }
                }

                results.Add(imageProperties);

                await _context.Photos.AddAsync(new Photo
                {                    
                    Bytes = (int)result.Bytes,
                    CreatedAt = DateTime.Now,
                    Format = result.Format,
                    Height = result.Height,
                    BookId=bookId,
                    Path = result.Url.AbsolutePath,
                    PublicId = result.PublicId,
                    ResourceType = result.ResourceType,
                    SecureUrl = result.SecureUrl.AbsoluteUri,
                    Signature = result.Signature,
                    Type = result.JsonObj["type"]?.ToString(),
                    Url = result.Url.AbsoluteUri,
                    Version = int.Parse(result.Version, provider),
                    Width = result.Width
                }).ConfigureAwait(false);
          //  }

            //await _context.UploadResults.AddAsync(new UploadResult { UploadResultAsJson = JsonConvert.SerializeObject(results),BookId=bookId }).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }

        [HttpPost("AddToCart")]
        public ActionResult AddToCart( [FromBody] cartReqDto cart)
        {
            try
            {
                if (cart is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _bookRepository.AddToCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpGet("GetItemToCart")]
        public async Task<ActionResult> GetItemToCart(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("Invalid id");
                }
                var book = _bookRepository.GetItemToCart(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPost("RemoveToCart")]
        public ActionResult RemoveToCart([FromBody] cartReqDto cart)
        {
            try
            {
                if (cart is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _bookRepository.RemoveToCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
        [HttpPost("EmptyCart")]
        public ActionResult EmptyCart([FromBody] cartReqDto cart)
        {
            try
            {
                if (cart is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _bookRepository.EmptyCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }


        }
    }
}
