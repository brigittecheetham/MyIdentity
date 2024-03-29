﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class LoginViewModel
    {

        //[Required]
        //[Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        //public string UserName { get; set; }

        //[Required]
        //public string Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }
}