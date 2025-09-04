using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class Dashboard_SettingMap : Common.CommonMap<DashboardSetting>
    {
        public override void Configure(EntityTypeBuilder<DashboardSetting> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
            builder.Property(o => o.GroupName);
            builder.Property(o => o.CategoryName).IsRequired();
        }
    }
}
