using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_QuestionMap : Common.CommonMap<Task_Question>
    {
        public override void Configure(EntityTypeBuilder<Task_Question> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Question).IsRequired();
            builder.Property(o => o.Answer).IsRequired();
            builder.Property(o => o.QuestionNumber);
            builder.HasOne(o => o.Task).WithMany(o => o.Task_Questions).HasForeignKey(k => k.TaskId).IsRequired();
        }
    }
}
