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
    public class MetaIla_CourseworkCompleteNotificationMap : Common.CommonMap<MetaIla_CourseworkCompleteNotification>
    {
        public override void Configure(EntityTypeBuilder<MetaIla_CourseworkCompleteNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                   .HasOne(x=>x.Employee)
                   .WithMany()
                   .HasForeignKey(x=>x.EmployeeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}