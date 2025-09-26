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
    public class EMPSkillQualitificationEvaluatorNotificationMap : Common.CommonMap<EMPSkillQualitificationEvaluatorNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPSkillQualitificationEvaluatorNotification> builder)
        {
            builder.HasBaseType<Notification>();
            builder.HasOne(x => x.SkillQualification_Evaluator_Link).WithMany().HasForeignKey(x => x.SkillQualification_Evaluator_LinkId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}