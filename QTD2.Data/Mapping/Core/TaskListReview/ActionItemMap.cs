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
    public class ActionItemMap : Common.CommonMap<ActionItem>
    {
        public override void Configure(EntityTypeBuilder<ActionItem> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TaskReviewId).IsRequired();
            builder.Property(o => o.AssigneeId);
            builder.Property(o => o.PriorityId).IsRequired();
            builder.Property(o => o.AssignedDate).IsRequired();
            builder.Property(o => o.DueDate).IsRequired();
            builder.Property(o => o.Notes);
            builder.HasOne(o => o.TaskReview).WithMany(o=>o.ActionItems).HasForeignKey(k => k.TaskReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Priority).WithMany().HasForeignKey(k => k.PriorityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Assignee).WithMany().HasForeignKey(k => k.AssigneeId).OnDelete(DeleteBehavior.NoAction);
            builder.Ignore("ActionItemTypeToDisplay");


            builder.HasDiscriminator<string>("ActionItemType")
               .HasValue<ActionItem>("ActionItem")
               .HasValue<DutyArea_ActionItem>("DutyArea_ActionItem")
               .HasValue<SubDutyArea_ActionItem>("SubDutyArea_ActionItem")
               .HasValue<Task_ActionItem>("Task_ActionItem")
               .HasValue<Steps_ActionItem>("Step_ActionItem")
               .HasValue<QuestionAndAnswer_ActionItem>("QuestionAndAnswer_ActionItem")
               .HasValue<Task_Specific_Suggestions_ActionItem>("Suggestion_ActionItem")
               .HasValue<Conditions_ActionItem>("Conditions_ActionItem")
               .HasValue<Criteria_ActionItem>("Criteria_ActionItem")
               .HasValue<References_ActionItem>("References_ActionItem")
               .HasValue<Tools_ActionItem>("Tool_ActionItem")
               .HasValue<EnablingObjective_ActionItem>("EnablingObjective_ActionItem")
               .HasValue<Procedure_ActionItem>("Procedure_ActionItem")
               .HasValue<RegulatoryRequirements_ActionItem>("RegulatoryRequirement_ActionItem")
               .HasValue<SafetyHazards_ActionItem>("SafetyHazard_ActionItem")
               .HasValue<MetaTask_ActionItem>("MetaTask_ActionItem")
               .HasValue<Other_ActionItem>("Other_ActionItem")
               .HasValue<PrepareForTaskRequalification_ActionItem>("PrepareForTaskRequalification_ActionItem")
               .HasValue<MakeTaskInactive_ActionItem>("MakeTaskInactive_ActionItem");

        builder.Property<string>("ActionItemType").HasMaxLength(100);
        }
    }
}