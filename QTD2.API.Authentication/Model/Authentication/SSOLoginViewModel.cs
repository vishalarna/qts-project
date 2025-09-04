using System.ComponentModel.DataAnnotations;

namespace QTD2.API.Authentication.Model.Authentication
{
    public class SSOLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        public string Redirect { get; set; }

        public SSOLoginViewModel()
        {
        }

        public SSOLoginViewModel(string redirect)
        {
            Redirect = redirect;
        }
    }
}
