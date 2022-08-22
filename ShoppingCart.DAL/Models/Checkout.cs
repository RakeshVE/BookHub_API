using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class Checkout
    {
        public Checkout()
        {
            Billings = new HashSet<Billing>();
            OrderDetails = new HashSet<OrderDetail>();
            PaymentDetails = new HashSet<PaymentDetail>();
            Shippings = new HashSet<Shipping>();
        }

        public int CheckoutId { get; set; }
        public int? CouponId { get; set; }
        public int UserId { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal FinalPay { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
