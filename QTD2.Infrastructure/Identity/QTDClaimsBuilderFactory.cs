using QTD2.Infrastructure.Identity.ClaimsBuilders;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Infrastructure.Database.Interfaces;

namespace QTD2.Infrastructure.Identity
{
    public class QTDClaimsBuilderFactory : IClaimsBuilderFactory
    {
        public QTDClaimsBuilderFactory()
        {
        }

        public IClaimsBuilder GetBuilder(ClaimsBuilderOptions options)
        {
            if (options.CloneClaims != null)
            {
                return new CloneClaimsBuilder();
            }

            // todo -> move this to an injected setting
            if (!string.IsNullOrEmpty(options.Server) && options.Server.ToLower() == "qtd")
            {
                return new QTDServerClaimsBuilder();
            }

            if (options.IsSystem)
            {
                return new QTDSystemClaimsBuilder();
            }

            throw new System.Exception("Unknown claims builder");
        }
    }
}
