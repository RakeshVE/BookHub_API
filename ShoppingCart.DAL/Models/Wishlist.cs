using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public bool IsLiked { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
