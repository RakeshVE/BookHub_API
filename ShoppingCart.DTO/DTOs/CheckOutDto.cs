using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
    public class CheckOutDto
    {
        public int CheckoutId { get; set; }
        public int? CouponId { get; set; }
        public int UserId { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal FinalPay { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

    }
}
