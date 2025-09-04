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
    public class DeliveryMethodMap : Common.CommonMap<DeliveryMethod>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.DisplayName).IsRequired();
            builder.Property(o => o.IsNerc).IsRequired().HasDefaultValue(false);
        }
    }
}
