using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenarioMap : Common.CommonMap<SimulatorScenario>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description);
            builder.Property(o => o.DurationHours);
            builder.Property(o => o.DurationMinutes);
            builder.Property(o => o.DifficultyId);
            builder.Property(o => o.NetworkConfiguration);
            builder.Property(o => o.LoadingConditions);
            builder.Property(o => o.Generation);
            builder.Property(o => o.Interchange);
            builder.Property(o => o.OtherBaseCase);
            builder.Property(o => o.ValidityChecks);
            builder.Property(o => o.RolePlays);
            builder.Property(o => o.Documentation);
            builder.Property(o => o.RatingScaleId);
            builder.Property(o => o.OperatingSkillsEvaluationMethod);
            builder.Property(o => o.Notes);
            builder.Property(o => o.StatusId);
            builder.Property(o => o.Message);
            builder.Property(o => o.PublishedDate);
            builder.Property(o => o.PublishedReason);

            builder.HasOne(o => o.Difficulty).WithMany().HasForeignKey(f => f.DifficultyId).OnDelete(DeleteBehavior.NoAction); 
            builder.HasOne(o => o.RatingScale).WithMany().HasForeignKey(f => f.RatingScaleId).OnDelete(DeleteBehavior.NoAction); 
            builder.HasOne(o => o.Status).WithMany().HasForeignKey(f => f.StatusId).OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
