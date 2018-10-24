using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage = "Username is empty")]
        public string Username { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 words")]
        [Required(ErrorMessage = "Password is empty")]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Minimum 6 words")]
        [Compare("Password", ErrorMessage = "Confirm password is invalid")]
        [Required(ErrorMessage = "Confirm password is empty")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is empty")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Confirm password is empty")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Precinct { get; set; }    
    }
}