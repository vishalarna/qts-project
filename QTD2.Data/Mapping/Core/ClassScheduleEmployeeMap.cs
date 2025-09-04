using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassScheduleEmployeeMap : Common.CommonMap<ClassSchedule_Employee>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Employee> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Employee).WithMany(o => o.ClassSchedule_Employee).HasForeignKey(k => k.EmployeeId).IsRequired(); // Relation
            builder.HasOne(o => o.ClassSchedule).WithMany(o => o.ClassSchedule_Employee).HasForeignKey(k => k.ClassScheduleId).IsRequired(); // Relation
            builder.HasOne(o => o.CBTStatus).WithMany().HasForeignKey(f => f.CBTStatusId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.PreTestStatus).WithMany().HasForeignKey(f => f.PreTestStatusId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.TestStatus).WithMany().HasForeignKey(f => f.TestStatusId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.ReTakeStatus).WithMany().HasForeignKey(f => f.RetakeStatusId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict).IsRequired();
        }
    }
}