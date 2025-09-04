namespace QTD2.Application.Authorization
{
    public class Policies
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string AdminSiteAccess = "AdminSiteAccess";
        public const string Authenticated = "Authenticated";
        public const string InstanceAdminOrBetter = "InstanceAdminOrBetter";
        public const string QtdSystem = "QtdSystem";
    }
}
