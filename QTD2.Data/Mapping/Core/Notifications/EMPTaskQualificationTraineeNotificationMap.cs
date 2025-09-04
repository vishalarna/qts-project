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
    public class EMPTaskQualificationTraineeNotificationMap : Common.CommonMap<EMPTaskQualificationTraineeNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPTaskQualificationTraineeNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.TaskQualification)
                .WithMany()
                .HasForeignKey(x=>x.TaskQualificationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}