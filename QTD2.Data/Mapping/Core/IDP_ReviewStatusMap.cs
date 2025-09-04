using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class IDP_ReviewStatusMap : Common.CommonMap<IDP_ReviewStatus>
    {
        public override void Configure(EntityTypeBuilder<IDP_ReviewStatus> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name);
            builder.Property(p => p.Description);
        }
    }
}
