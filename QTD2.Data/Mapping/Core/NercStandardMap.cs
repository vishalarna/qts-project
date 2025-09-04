using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class NercStandardMap : Common.CommonMap<NercStandard>
    {
        public override void Configure(EntityTypeBuilder<NercStandard> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(500);
            builder.Property(o => o.IsUserDefined).HasDefaultValue(false);
            builder.Property(o => o.IsNercStandard).IsRequired();
        }
    }
}
