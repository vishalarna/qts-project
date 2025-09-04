using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Data.Mapping.Common;
using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Authentication
{
    public class AdminMessageAuthMap : CommonMap<AdminMessageAuth>
    {
        public override void Configure(EntityTypeBuilder<AdminMessageAuth> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Message).IsRequired();
            builder.Property(o => o.Instance).IsRequired();
            builder.Property(o => o.Received);
            builder.Property(o => o.ReceivedDate);
            builder.Property(o => o.ExpirationDate);
        }
    }
}
