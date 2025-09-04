using System.IO;

namespace QTD2.Domain
{
    public class CustomClaimTypes
    {
        // todo add domain
        private static readonly string _domain = "qtd";

        public static string Prefix
        {
            get { return _domain + "/claims/"; }
        }

        public static string UserName
        {
            get { return Prefix + "/username"; }
        }

        public static string IsAdmin
        {
            get { return Prefix + "/isAdmin"; }
        }

        public static string isManager
        {
            get { return Prefix + "/isManager"; }
        }

        public static string isInstructor
        {
            get { return Prefix + "/isInstructor"; }
        }

        public static string ClientName
        {
            get { return Prefix + "/clientName"; }
        }

        public static string IsClientAdmin
        {
            get { return Prefix + "/isClientAdmin"; }
        }

        public static string ClientUserName
        {
            get { return Prefix + "/clientUserName"; }
        }

        public static string TfaRequired
        {
            get { return Prefix + "/2faRequired"; }
        }

        public static string Revoked
        {
            get { return Prefix + "/revoked"; }
        }

        public static string InstanceName
        {
            get { return Prefix + "/instanceName"; }
        }

        // Almost certainly want to change this to some identifer
        public static string ClientAdmin
        {
            get { return Prefix + "/clientAdmin"; }
        }

        public static string IsEmployeeUser
        {
            get { return Prefix + "/isEmployee"; }
        }
        public static string EmployeeId
        {
            get { return Prefix + "/employeeId"; }
        }
        public static string IsImpersonatingUser
        {
            get { return Prefix + "/isImpersonatingUser"; }
        }

        public static string ClientUserId
        {
            get { return Prefix + "/clientId"; }
        }

        public static string IsClientUser
        {
            get { return Prefix + "/isClientUser"; }
        }
        public static string InstructorId
        {
            get { return Prefix + "/instructorId"; }
        }

        public static string InstanceAdminId
        {
            get { return Prefix + "/instanceAdminId"; }
        }

        public static string IsInstanceAdmin
        {
            get { return Prefix + "/isInstanceAdmin"; }
        }

        public static string IsQTDUser
        {
            get { return Prefix + "/isQtdUser"; }
        }

        public static string QTDUserId
        {
            get { return Prefix + "/qtdUserId"; }
        }

        public static string HasMultipleInstances
        {
            get { return Prefix + "/hasMultipleInstances"; }
        }
        public static string IsBetaInstance
        {
            get { return Prefix + "/isBetaInstance"; }
        }
        public static string HasZeroInstance
        {
            get { return Prefix + "/hasZeroInstance"; }
        }
        public static string HasIdentityProvider
        {
            get { return Prefix + "/identityProvider"; }
        }
        public static string TfaTrustedDevice
        {
            get { return Prefix + "/2faTrustedDevice"; }
        }
    }
}
