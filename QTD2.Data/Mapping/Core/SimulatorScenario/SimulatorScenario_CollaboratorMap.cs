using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_CollaboratorMap : Common.CommonMap<SimulatorScenario_Collaborator>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Collaborator> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.PermissionId).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(o=>o.Collaborators).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.User).WithMany(o=>o.SimulatorScenario_Collaborators).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Permission).WithMany(o=>o.SimulatorScenario_Collaborators).HasForeignKey(f => f.PermissionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
