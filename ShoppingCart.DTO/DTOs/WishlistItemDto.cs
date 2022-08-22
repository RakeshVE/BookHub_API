using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
    public class WishlistItemDto
    {
        public int BookId { get; set; }
        public string  Image { get; set; }
        public string BookName { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
    }
}
