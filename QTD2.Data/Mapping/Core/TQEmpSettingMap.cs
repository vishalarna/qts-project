using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TQEmpSettingMap : Common.CommonMap<TQEmpSetting>
    {
        public override void Configure(EntityTypeBuilder<TQEmpSetting> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TaskQualification).WithOne(o => o.TQEmpSetting).HasForeignKey<TQEmpSetting>(f => f.TaskQualificationId).IsRequired();
            builder.Ignore("MultipleSignOffDisplay");
        }
    }
}
