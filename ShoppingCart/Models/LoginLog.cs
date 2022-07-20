using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class LoginLog
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
