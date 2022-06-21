using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class Cart
    {
        public Cart()
        {
            Checkouts = new HashSet<Checkout>();
        }

        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int CartTotal { get; set; }
        public decimal? DiscountPer { get; set; }
        public decimal NetPay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}
