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
    public class Version_ILAMap : Common.CommonMap<Version_ILA>
    {
        public override void Configure(EntityTypeBuilder<Version_ILA> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(500);
            builder.Property(o => o.NickName).HasMaxLength(500);
            builder.Property(o => o.VersionNumber).IsRequired();
            builder.Property(o => o.Number);
            builder.Property(o => o.Description).IsRequired().HasMaxLength(4000);
            builder.Property(o => o.Image);
            builder.Property(o => o.TrainingPlan);
            builder.Property(o => o.IsSelfPaced).HasDefaultValue(false);
            builder.Property(o => o.IsOptional).HasDefaultValue(true);
            builder.Property(o => o.IsPublished).HasDefaultValue(false);
            builder.Property(o => o.PublishDate);
            builder.Property(o => o.HasPilotData).HasDefaultValue(false);
            builder.Property(o => o.IsProgramManual).HasDefaultValue(false);
            builder.Property(o => o.SubmissionDate);
            builder.Property(o => o.ApprovalDate);
            builder.Property(o => o.ExpirationDate);
            builder.HasOne(o => o.ILA).WithMany(m => m.Version_ILAs).HasForeignKey(k => k.ILAId).IsRequired();
        }
    }
}
