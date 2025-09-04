using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace QTD2.Data.Mapping.Core
{
   public class ReportFilterMap : Common.CommonMap<ReportFilter>
    {
        public override void Configure(EntityTypeBuilder<ReportFilter> builder)
        {
            base.Configure(builder);

            var propTypeConverter = new EnumToStringConverter<FilterPropertyTypeEnum>();
            var valueTypeConverter = new EnumToStringConverter<FilterValueTypeEnum>();

            builder.Property(o => o.ReportId).IsRequired();
            builder.Property(o => o.PropertyType).IsRequired().HasConversion(propTypeConverter);
            builder.Property(o => o.ValueType).IsRequired().HasConversion(valueTypeConverter);
            builder.Property(o => o.Value);
        }
    }
}
