using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Task_QuestionMap : Common.CommonMap<Version_Task_Question>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_Question> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Question).IsRequired();
            builder.Property(o => o.Answer).IsRequired();

            builder.HasOne(o => o.Version_Task).WithMany(m => m.Version_Task_Questions).HasForeignKey(k => k.VersionTaskId).IsRequired().OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(o => o.Task_Question).WithMany(m => m.Version_Task_Questions).HasForeignKey(k => k.TaskQuestionId).IsRequired().OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
