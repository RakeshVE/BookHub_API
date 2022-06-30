using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int? Quantity { get; set; }
        public int? CartTotal { get; set; }
        public decimal? DiscountPer { get; set; }
        public decimal NetPay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal OurPrice { get; set; }



    }
}
