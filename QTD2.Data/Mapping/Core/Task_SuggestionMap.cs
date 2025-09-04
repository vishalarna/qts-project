using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_SuggestionMap : Common.CommonMap<Task_Suggestion>
    {
        public override void Configure(EntityTypeBuilder<Task_Suggestion> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Number).IsRequired();
            builder.HasOne(o => o.Task).WithMany(m => m.Task_Suggestions).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.TaskSuggestionType).WithMany().HasForeignKey(k => k.TaskSuggestionTypeId);
        }
    }
}
