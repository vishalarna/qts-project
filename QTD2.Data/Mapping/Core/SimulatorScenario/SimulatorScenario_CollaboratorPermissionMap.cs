using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_CollaboratorPermissionMap : Common.CommonMap<SimulatorScenario_CollaboratorPermission>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_CollaboratorPermission> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Permission).IsRequired();
        }
    }
}