using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class Version_TrainingProgramMap : Common.CommonMap<Version_TrainingProgram>
    {
        public override void Configure(EntityTypeBuilder<Version_TrainingProgram> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.VersionNumber).HasMaxLength(20);

            builder.HasOne(o => o.trainingProgram).WithMany().HasForeignKey(k => k.TrainingProgramId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Position).WithMany().HasForeignKey(k => k.PositionId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}