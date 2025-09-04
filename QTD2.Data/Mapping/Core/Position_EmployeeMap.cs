using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class Position_EmployeeMap : Common.CommonMap<Position_Employee>
    {
        public override void Configure(EntityTypeBuilder<Position_Employee> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Position).WithMany(x => x.Position_Employees).HasForeignKey(y => y.PositionId).IsRequired();
            builder.HasOne(o => o.Employee).WithMany(x => x.Position_Employees).HasForeignKey(y => y.EmployeeId).IsRequired();
        }
    }
}
