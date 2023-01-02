using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KTS_Testing_System.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}