using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            CustomerReviews = new HashSet<CustomerReview>();
            PaymentDetails = new HashSet<PaymentDetail>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
