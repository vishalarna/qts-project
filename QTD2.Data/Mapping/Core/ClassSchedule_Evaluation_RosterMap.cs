using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ClassSchedule_Evaluation_RosterMap : Common.CommonMap<ClassSchedule_Evaluation_Roster>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Evaluation_Roster> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ClassScheduleInfo).WithMany(x => x.ClassSchedule_Evaluation_Rosters).HasForeignKey(y => y.ClassScheduleId);
            builder.HasOne(o => o.MetaILA).WithMany(x => x.ClassSchedule_Evaluation_Rosters).HasForeignKey(y => y.MetaIlaId);
            builder.HasOne(o => o.Employee).WithMany(x => x.ClassSchedule_Evaluation_Rosters).HasForeignKey(y => y.EmployeeId).IsRequired();
            builder.HasOne(o => o.StudentEvaluationInfo).WithMany(x => x.ClassSchedule_Evaluation_Rosters).HasForeignKey(y => y.StudentEvaluationId).IsRequired();
            builder.Property(p => p.IsAllowed).HasDefaultValue(true);

        }
    }
}
