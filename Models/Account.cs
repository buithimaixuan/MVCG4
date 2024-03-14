using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCG4.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Account
    {
        [Key]
        public int AccId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        // [Phone(ErrorMessage = "Invalid phone!!!")]



        private const string PhoneNumberRegexPattern = @"^0\d{9}$";

        // [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(PhoneNumberRegexPattern, ErrorMessage = "Invalid Vietnam phone number.")]
        public string PhoneNumber { get; set; }

        [Required]
        // [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email!!!")]
        public string Email { get; set; }
        public int IsAdmin { get; set; }
        public int IsDelete { get; set; }
    }
}
