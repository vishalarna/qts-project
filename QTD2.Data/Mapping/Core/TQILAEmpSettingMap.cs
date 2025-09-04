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
    public class TQILAEmpSettingMap : Common.CommonMap<TQILAEmpSetting>
    {
        public override void Configure(EntityTypeBuilder<TQILAEmpSetting> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.TQILAEmpSettings).HasForeignKey(f => f.ILAId).IsRequired();
            builder.HasOne(o => o.EmpSettingsReleaseType).WithMany().HasForeignKey(f => f.EmpSettingsReleaseTypeId);
        }
    }
}
