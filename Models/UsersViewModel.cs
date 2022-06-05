using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDemo.Models
{
    public class UsersViewModel
    {
        [Display(Name ="First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [NotMapped]

        [Display(Name = "Role")]
        public int roleID { get; set; }

        [Display(Name = "Role")]
        public string role { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }
        public string externalId { get; set; }
    }
}
