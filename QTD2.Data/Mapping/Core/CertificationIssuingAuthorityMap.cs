using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class CertificationIssuingAuthorityMap : Common.CommonMap<CertificationIssuingAuthority>
    {
        public override void Configure(EntityTypeBuilder<CertificationIssuingAuthority> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Description);
            builder.Property(o => o.Website).HasMaxLength(200);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Notes).HasMaxLength(2000);
        }
    }
}
