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
    public class EMPSelfRegistrationApprovalNotificationMap : Common.CommonMap<EMPSelfRegistrationApprovalNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPSelfRegistrationApprovalNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.ClassScheduleEmployee)
                .WithMany()
                .HasForeignKey(x => x.ClassScheduleEmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}