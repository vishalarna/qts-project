using System.Security.Claims;

namespace QTD2.Web.QTD.Identity
{
    public class QTDIdentity : Infrastructure.Identity.Identity
    {
        public QTDIdentity(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
        }
    }
}
