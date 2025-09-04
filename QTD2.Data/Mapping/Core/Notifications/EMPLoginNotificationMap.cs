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
    public class EMPLoginNotificationMap : Common.CommonMap<EMPLoginNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPLoginNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}