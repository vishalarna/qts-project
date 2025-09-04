using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SafetyHazard_SetMap : Common.CommonMap<SafetyHazard_Set>
    {
        public override void Configure(EntityTypeBuilder<SafetyHazard_Set> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.SafetyHzAbatementText);
            builder.Property(x => x.SafetyHzControlsText);
        }
    }
}
