namespace QTD2.API.Authentication.Model.Authentication
{
    public class AuthenticationViewModel
    {
        public string UserName { get; set; }
        public string VerificationCode { get; set; }
        public bool? DoNotAsk { get; set; }
    }
}
