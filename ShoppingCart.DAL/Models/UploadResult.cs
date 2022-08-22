using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class UploadResult
    {
        public int Id { get; set; }
        public string UploadResultAsJson { get; set; }
        public int? BookId { get; set; }
    }
}
