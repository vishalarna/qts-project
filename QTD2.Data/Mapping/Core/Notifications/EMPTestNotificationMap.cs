using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class EMPTestNotificationMap : Common.CommonMap<EMPTestNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPTestNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.ClassScheduleRoster)
                .WithMany()
                .HasForeignKey(x=>x.ClassScheduleRosterId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}