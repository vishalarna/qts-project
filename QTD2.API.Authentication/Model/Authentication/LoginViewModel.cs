using System.ComponentModel.DataAnnotations;

namespace QTD2.API.Authentication.Model.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string Redirect { get; set; }

        public LoginViewModel()
        {
        }

        public LoginViewModel(string redirect)
        {
            Redirect = redirect;
        }
    }
}
