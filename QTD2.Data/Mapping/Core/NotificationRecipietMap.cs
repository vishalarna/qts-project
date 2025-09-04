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
   public class NotificationRecipietMap : Common.CommonMap<NotificationRecipiet>
    {
        public override void Configure(EntityTypeBuilder<NotificationRecipiet> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Notification).WithMany().HasForeignKey(k => k.NotificationId).IsRequired();
            builder.HasOne(o => o.ToPerson).WithMany().HasForeignKey(k => k.ToPersonId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            
            builder.Property(o => o.NotificationId).IsRequired();
            builder.Property(o => o.ToPersonId).IsRequired();
            builder.Property(o => o.AttemptDate).IsRequired();
        }
    }
}
