using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class PersonActivityNotificationMap : Common.CommonMap<PersonActivityNotification>
    {
        public override void Configure(EntityTypeBuilder<PersonActivityNotification> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Person).WithMany(m => m.PersonActivityNotifications).HasForeignKey(k => k.PersonId).IsRequired();
            builder.HasOne(o => o.ActivityNotification).WithMany(m => m.PersonActivityNotifications).HasForeignKey(k => k.ActivityNotificationId).IsRequired();

            builder.HasIndex(i => new { i.PersonId, i.ActivityNotificationId }).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}

