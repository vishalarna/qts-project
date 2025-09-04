using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Web.Authentication.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string Redirect { get; set; }

        public LoginModel()
        {

        }

        public LoginModel(string redirect)
        {
            Redirect = redirect;
        }
    }
}
