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
    public class EMPSkillQualificationTraineeNotificationMap : Common.CommonMap<EMPSkillQualificationTraineeNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPSkillQualificationTraineeNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.SkillQualification)
                .WithMany()
                .HasForeignKey(x=>x.SkillQualificationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}