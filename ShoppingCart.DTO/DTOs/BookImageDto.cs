using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DTO.DTOs
{
  public  class BookImageDto
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
    }
}
