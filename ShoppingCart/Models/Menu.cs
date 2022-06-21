using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Books = new HashSet<Book>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
