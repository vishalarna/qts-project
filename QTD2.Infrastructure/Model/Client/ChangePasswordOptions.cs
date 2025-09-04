using System.ComponentModel.DataAnnotations;

namespace QTD2.API.Infrastructure.Model.Client
{
    public class ChangePasswordOptions
    {
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "PasswordDon'tMatch")]
        public string ConfirmPassword { get; set; }
    }
}
