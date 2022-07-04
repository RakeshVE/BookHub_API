using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class CheckOutDto
    {
        public int? CouponId { get; set; }
        public int CartId { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal FinalPay { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

    }
}
