using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Elearning.Website.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="User name")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
    }
}