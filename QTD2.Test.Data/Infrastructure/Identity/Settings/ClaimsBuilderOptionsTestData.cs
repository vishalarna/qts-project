
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Test.Data.Infrastructure.Identity.Settings
{
    public class ClaimsBuilderOptionsTestData
    {
        public static ClaimsBuilderOptions RefreshToken()
        {
            return new ClaimsBuilderOptions(true, false);
        }

        public static ClaimsBuilderOptions AuthTokenNot2FAApproved
        {
            get

            {
                return new ClaimsBuilderOptions(false, false);
            }
        }

        public static ClaimsBuilderOptions AuthToken2FAApproved
        {
            get

            {
                return new ClaimsBuilderOptions(false, true);
            }
        }
        public static ClaimsBuilderOptions IsSystem
        {
            get
            {
                return new ClaimsBuilderOptions(true);
            }
        }
    }
}
