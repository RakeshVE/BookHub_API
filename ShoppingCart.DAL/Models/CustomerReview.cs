using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class CustomerReview
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
        public string Headline { get; set; }
        public string Comments { get; set; }
        public string BottomLine { get; set; }
        public string NickName { get; set; }
        public string Location { get; set; }
        public string Industry { get; set; }
        public string JobTitle { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
