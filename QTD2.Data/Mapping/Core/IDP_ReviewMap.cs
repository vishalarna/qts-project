using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class IDP_ReviewMap : Common.CommonMap<IDP_Review>
    {
        public override void Configure(EntityTypeBuilder<IDP_Review> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.IDP_ReviewStatus).WithMany(m => m.IDP_Reviews).HasForeignKey(f => f.StatusId).IsRequired();
            builder.HasOne(o => o.Employee).WithMany(m => m.IDP_Reviews).HasForeignKey(f => f.EmployeeId).IsRequired();
        }
    }
}
