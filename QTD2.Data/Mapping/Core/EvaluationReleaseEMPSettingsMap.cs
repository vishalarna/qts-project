using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EvaluationReleaseEMPSettingsMap : Common.CommonMap<EvaluationReleaseEMPSettings>
    {
        public override void Configure(EntityTypeBuilder<EvaluationReleaseEMPSettings> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithOne(w => w.EvaluationReleaseEMPSetting).HasForeignKey<EvaluationReleaseEMPSettings>(f => f.ILAId).IsRequired();
            builder.HasOne(o => o.EmpSettingsReleaseType).WithMany().HasForeignKey(f => f.EmpSettingsReleaseTypeId);
        }
    }
}
