using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username is invalid")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is invalid")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}