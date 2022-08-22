using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            Checkouts = new HashSet<Checkout>();
        }

        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public int BookId { get; set; }
        public decimal DiscountPer { get; set; }
        public DateTime Validity { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Book Book { get; set; }
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}
