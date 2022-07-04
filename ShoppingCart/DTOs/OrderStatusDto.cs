using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class OrderStatusDto
    {
        public int OrderId { get; set; }
        public int CheckoutId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal ListPrice { get; set; }
        public decimal OurPrice { get; set; }
        public string ProductType { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
