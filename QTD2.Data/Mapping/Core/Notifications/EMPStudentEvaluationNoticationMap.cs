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
    public class EMPStudentEvaluationNoticationMap : Common.CommonMap<EMPStudentEvaluationNotication>
    {
        public override void Configure(EntityTypeBuilder<EMPStudentEvaluationNotication> builder)
        {
            builder.HasBaseType<Notification>();

            builder
              .HasOne(x=>x.ClassSchedule_Evaluation_Roster)
              .WithMany()
              .HasForeignKey(x => x.ClassSchedule_Evaluation_RosterId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}