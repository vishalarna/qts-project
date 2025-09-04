using System.Collections.Generic;
using System.Security.Claims;

namespace QTD2.Infrastructure.Identity.Settings
{
    public class ClaimsBuilderOptions
    {
        public bool IsRefreshToken { get; set; }

        public bool Is2FAApproved { get; set; }

        public bool IsSystem { get; set; }

        public string SelectInstance { get; set; }

        public string ImpersonateUser { get; set; }

        public string Server { get; set; }

        public IList<Claim> CloneClaims { get; protected set; }

        public bool BuildQtdIdentity { get; set; }

        public string QTDDatabase { get; set; }

        public ClaimsBuilderOptions()
        {
        }

        public ClaimsBuilderOptions(bool isSystem)
        {
            IsSystem = isSystem;
        }

        public ClaimsBuilderOptions(bool isRefreshToken, bool is2FaApproved, string selectInstance = null, string impersonateUser = null)
        {
            IsRefreshToken = isRefreshToken;
            Is2FAApproved = is2FaApproved;
            SelectInstance = selectInstance;
            ImpersonateUser = impersonateUser;
        }

        public ClaimsBuilderOptions(string server)
            : this(false, true)
        {
            Server = server;
        }

        public ClaimsBuilderOptions(IList<Claim> fromClaims)
            : this(false, true)
        {
            CloneClaims = fromClaims;
        }
    }
}
