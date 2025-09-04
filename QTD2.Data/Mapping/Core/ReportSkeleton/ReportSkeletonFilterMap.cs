using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace QTD2.Data.Mapping.Core
{
    public class ReportSkeletonFilterMap : Common.CommonMap<QTD2.Domain.Entities.Core.ReportSkeletonFilter>
    {
        public override void Configure(EntityTypeBuilder<QTD2.Domain.Entities.Core.ReportSkeletonFilter> builder)
        {
            base.Configure(builder);

            var propTypeConverter = new EnumToStringConverter<FilterPropertyTypeEnum>();
            var valueTypeConverter = new EnumToStringConverter<FilterValueTypeEnum>();

            builder.Property(o => o.MinOption);
            builder.Property(o => o.MaxOption);
            builder.Property(o => o.FilterOption).HasMaxLength(200);
            builder.Property(o => o.ReportSkeletonId).IsRequired();
            builder.Property(o => o.PropertyType).IsRequired().HasConversion(propTypeConverter);
            builder.Property(o => o.ValueType).IsRequired().HasConversion(valueTypeConverter);
            builder.Property(o => o.MaxAllowedSelections);
        }
    }
}
