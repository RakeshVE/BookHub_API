using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class StripePaymentRequest
    {
        public string tokenId { get; set; }
        public string productName { get; set; }
        public decimal amount { get; set; }
    }
}
