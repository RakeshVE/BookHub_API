using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.DAL.Models
{
    public partial class Shipping
    {
        public int ShippingId { get; set; }
        public int CheckoutId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public long ZipCode { get; set; }
        public string Phone { get; set; }
        public string AddressType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Checkout Checkout { get; set; }
    }
}
