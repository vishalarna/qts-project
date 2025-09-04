using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Employee_TaskMap : Common.CommonMap<Employee_Task>
    {
        public override void Configure(EntityTypeBuilder<Employee_Task> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.MajorVersion).IsRequired();
            builder.Property(o => o.MinorVersion).IsRequired();
            builder.Property(o => o.Archived).HasDefaultValue(false);

            builder.HasOne(o => o.Employee).WithMany(m => m.Employee_Tasks).HasForeignKey(k => k.EmployeeId).IsRequired();
            builder.HasOne(o => o.Task).WithMany(m => m.Employee_Tasks).HasForeignKey(k => k.TaskId).IsRequired();

            builder.HasIndex(i => new { i.EmployeeId, i.TaskId, i.MajorVersion }).IsUnique();
        }
    }
}
