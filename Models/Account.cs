using System;
using System.Collections.Generic;

#nullable disable

namespace MVCG4.Models
{
    public partial class Account
    {
        public int AccId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int IsAdmin { get; set; }
        public int IsDelete { get; set; }
    }
}
