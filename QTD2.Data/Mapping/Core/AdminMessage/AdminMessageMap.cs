using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class AdminMessageMap : Common.CommonMap<AdminMessage>
    {
        public override void Configure(EntityTypeBuilder<AdminMessage> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SourceAdminMessageId).IsRequired();
            builder.Property(o => o.Message).IsRequired();
            builder.Property(o => o.ReceivedDate);
            builder.Property(o => o.ExpirationDate);
        }
    }
}
