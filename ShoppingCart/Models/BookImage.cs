using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class BookImage
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
    }
}
