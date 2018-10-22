using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "You must fill username to login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must fill password to login")]
        public string Password { get; set; }
    }
}