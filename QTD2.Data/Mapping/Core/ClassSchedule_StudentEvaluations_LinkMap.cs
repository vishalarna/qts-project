using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassSchedule_StudentEvaluations_LinkMap : Common.CommonMap<ClassSchedule_StudentEvaluations_Link>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_StudentEvaluations_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.StudentEvaluation).WithMany(m => m.ClassSchedule_StudentEvaluations_Links).HasForeignKey(k => k.StudentEvaluationId).IsRequired();
            builder.HasOne(o => o.ClassSchedule).WithMany(m => m.ClassSchedule_StudentEvaluations_Links).HasForeignKey(k => k.ClassScheduleId).IsRequired();
            
        }
    }
}
