using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class Supplier
    {
        public int SupId { get; set; }
        public int ProId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
