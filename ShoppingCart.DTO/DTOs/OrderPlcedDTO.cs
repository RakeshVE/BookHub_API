using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
    public class OrderPlcedDTO
    {
        public int CheckoutId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public decimal OurPrice { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
