using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CheckoutId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Book Book { get; set; }
        public virtual Checkout Checkout { get; set; }
        public virtual User User { get; set; }
    }
}
