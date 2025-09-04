namespace QTD2.API.Infrastructure.Model.Client
{
    public class CreateUserOptions
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public bool EmpEnabled { get; set; }
        public bool TwoFAEnabled { get; set; }

        public string InstanceName { get; set; }
        public string? CreateWithIdentityProvider { get; set; }

    }

    public class UpdateUserOptions
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
