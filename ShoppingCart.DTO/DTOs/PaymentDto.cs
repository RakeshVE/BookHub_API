using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
   public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int CheckoutId { get; set; }
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
