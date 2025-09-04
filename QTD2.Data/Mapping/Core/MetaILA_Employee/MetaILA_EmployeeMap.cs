using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class MetaILA_EmployeeMap : Common.CommonMap<MetaILA_Employee>
    {
        public override void Configure(EntityTypeBuilder<MetaILA_Employee> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EmployeeId).IsRequired();
            builder.Property(o => o.MetaILAId).IsRequired();

            builder.HasOne(o => o.Employee).WithMany(m => m.MetaILA_Employees).HasForeignKey(f => f.EmployeeId).IsRequired();
            builder.HasOne(o => o.MetaILA).WithMany(m => m.MetaILA_Employees).HasForeignKey(f => f.MetaILAId).IsRequired();
        }
    }
}
