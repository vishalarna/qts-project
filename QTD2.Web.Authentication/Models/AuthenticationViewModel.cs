using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Web.Authentication.Models
{
    public class AuthenticationViewModel
    {
        public string VerificationCode { get; set; }
        public string UserName { get; set; }
    }
}
