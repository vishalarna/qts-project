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
    public class EnablingObjective_Employee_LinkMap : Common.CommonMap<EnablingObjective_Employee_Link>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Employee_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.StartDate);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.EnablingObjective_Employee_Links).HasForeignKey(k => k.EOID).IsRequired();
            builder.HasOne(o => o.Employee).WithMany(m => m.EnablingObjective_Employee_Links).HasForeignKey(k => k.EmployeeId).IsRequired();

            builder.HasIndex(i => new { i.EOID, i.EmployeeId }).IsUnique();

        }
    }
}
