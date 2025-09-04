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
    public class EMPTaskQualitificationEvaluatorNotificationMap : Common.CommonMap<EMPTaskQualitificationEvaluatorNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPTaskQualitificationEvaluatorNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.TaskQualification_Evaluator_Link)
                .WithMany()
                .HasForeignKey(x=>x.TaskQualification_Evaluator_LinkId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}