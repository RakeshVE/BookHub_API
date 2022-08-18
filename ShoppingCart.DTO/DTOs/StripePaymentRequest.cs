using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
    public class StripePaymentRequest
    {
        public string tokenId { get; set; }
        public string productName { get; set; }
        public decimal amount { get; set; }
        public string email { get; set; }
        public int card_expMonth { get; set; }
        public int card_expYear { get; set; }
        public string card_id { get; set; }

    }
}
