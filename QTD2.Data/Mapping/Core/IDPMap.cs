using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class IDPMap : Common.CommonMap<IDP>
    {
        public override void Configure(EntityTypeBuilder<IDP> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.IDPYear);
            builder.HasOne(o => o.Employee).WithMany(m => m.IDPs).HasForeignKey(f => f.EmployeeId).IsRequired();
            builder.HasOne(o => o.ILA).WithMany(m => m.IDPs).HasForeignKey(f => f.ILAId).IsRequired();
        }
    }
}
