using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public int CheckoutId { get; set; }
        public int BookId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
