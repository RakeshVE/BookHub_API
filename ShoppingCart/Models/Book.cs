using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
            Coupons = new HashSet<Coupon>();
            CustomerReviews = new HashSet<CustomerReview>();
            OrderDetails = new HashSet<OrderDetail>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public decimal ListPrice { get; set; }
        public decimal OurPrice { get; set; }
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        public string Details { get; set; }
        public string ProductType { get; set; }
        public string Description { get; set; }
        public string SystemReq { get; set; }
        public string Demo { get; set; }
        public bool? IsActive { get; set; }
        public int MenuId { get; set; }
        public bool IsBook { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ContentType { get; set; }
        public string Certification { get; set; }
        public string Publisher { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
