namespace QTD2.API.Authentication.Model.Authentication
{
    public class CreatePasswordViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string OldPassword { get; set; }

        public string Token { get; set; }
    }
}
