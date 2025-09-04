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
    public class MetaIla_Employee_SelfRegistrationRequiredNotificationMap : Common.CommonMap<MetaIla_Employee_SelfRegistrationRequiredNotification>
    {
        public override void Configure(EntityTypeBuilder<MetaIla_Employee_SelfRegistrationRequiredNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
              .HasOne(x=>x.Employee)
              .WithMany()
              .HasForeignKey(x=>x.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);

            builder
             .HasOne(x=>x.NextMeta_ILAMembers_Link)
             .WithMany()
             .HasForeignKey(x => x.NextMeta_ILAMembers_LinkId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}