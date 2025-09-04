using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILAMap : Common.CommonMap<ILA>
    {
        public override void Configure(EntityTypeBuilder<ILA> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(500);
            builder.Property(o => o.NickName).HasMaxLength(500);
            builder.Property(o => o.Number);
            builder.Property(o => o.Description);
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
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.Property(o => o.Prerequisites);

            builder.HasOne(o => o.Provider).WithMany(m => m.ILAs).HasForeignKey(k => k.ProviderId).IsRequired();
            builder.HasOne(o => o.DeliveryMethod).WithMany(m => m.ILAs).HasForeignKey(k => k.DeliveryMethodId);
        }
    }
}
