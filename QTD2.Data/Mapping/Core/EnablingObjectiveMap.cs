using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjectiveMap : Common.CommonMap<EnablingObjective>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Number).IsRequired();
            builder.Property(o => o.CategoryId).IsRequired();
            builder.Property(o => o.SubCategoryId).IsRequired();
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.TopicId);
            builder.Property(o => o.IsSkillQualification).HasDefaultValue(false);
            builder.Property(o => o.EffectiveDate).IsRequired();

            builder.Property(o => o.Conditions);
            builder.Property(o => o.References);
            builder.Property(o => o.Criteria).HasMaxLength(200);
            builder.HasOne(o => o.EnablingObjective_Topic).WithMany(m => m.EnablingObjectives).HasForeignKey(k => k.TopicId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.EnablingObjective_Category).WithMany(m => m.EnablingObjectives).HasForeignKey(k => k.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.EnablingObjective_SubCategory).WithMany(m => m.EnablingObjectives).HasForeignKey(k => k.SubCategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
