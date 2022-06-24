using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class BookDto
    {
        
        public string Title { get; set; }
        public string? Image { get; set; }
        public decimal ListPrice { get; set; }
        public decimal OurPrice { get; set; }
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        public string Details { get; set; }
        public string ProductType { get; set; }
        public string Description { get; set; }
        public string SystemReq { get; set; }
        public string Demo { get; set; }
        public bool? IsActive { get; set; }
        public int MenuId { get; set; }
        public bool IsBook { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ContentType { get; set; }
        public string Certification { get; set; }
        public string Publisher { get; set; }
    }
}
