using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Authentication
{
    public class IdentityProviderMap : Common.CommonMap<IdentityProvider>
    {
        public override void Configure(EntityTypeBuilder<IdentityProvider> builder)
        {
            base.Configure(builder);
            builder.HasIndex(o => new { o.Name }).IsUnique();
            builder.Ignore("Type");
            builder.HasDiscriminator<string>("SubType")
               .HasValue<IdentityProvider>("IdentityProvider")
               .HasValue<PasswordProvider>("Password")
               .HasValue<SamlProvider>("Saml")
               .HasValue<OAuthProvider>("OAuth");
        }
    }
}
