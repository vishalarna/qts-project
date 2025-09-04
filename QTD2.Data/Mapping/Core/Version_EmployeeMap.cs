using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EmployeeMap : Common.CommonMap<Version_Employee>
    {
        public override void Configure(EntityTypeBuilder<Version_Employee> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EmployeeId).IsRequired();
            builder.Property(o => o.PersonId).IsRequired();
            builder.Property(o => o.Version_Number);
            builder.Property(o => o.EmployeeNumber).IsRequired();

            builder.HasOne(o => o.Employee).WithMany(x => x.Version_Employees).HasForeignKey(k => k.EmployeeId).IsRequired();
        }
    }
}
