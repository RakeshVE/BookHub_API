using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class AdminDashBoardDto
    {
        public Int64? TotalUsers { get; set; }
        public Int64? TotalActiveUsers { get; set; }
        public decimal? TotalSales { get; set; }
        public Int64? TotalItemSale { get; set; }



    }
}
