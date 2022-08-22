using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
    public class AddWishListDto
    {
        public int BookId { get; set; }
        public int UserID  { get; set; }
        public bool IsLiked  { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
