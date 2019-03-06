using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Infrastructure.Identity;
namespace Web.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [MaxLength(10)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Surname { get; set; }

        [MaxLength(10)]
        public string Cellphone { get; set; }

        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }

        public IEnumerable<ApplicationRole> Roles { get; set; }

        public int SelectedRole { get; set; }
    }
}