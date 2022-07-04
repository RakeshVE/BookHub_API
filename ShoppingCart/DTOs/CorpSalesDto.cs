using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.DTOs
{
    public class CorpSalesDto
    {
        public int CorpSalesId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Purpose { get; set; }
        public string Details { get; set; }
        public DateTime? Createdn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
