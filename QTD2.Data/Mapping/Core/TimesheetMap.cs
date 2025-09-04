using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TimesheetMap : Common.CommonMap<Timesheet>
    {
        public override void Configure(EntityTypeBuilder<Timesheet> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Date).IsRequired();
            builder.Property(o => o.MethodId).IsRequired();
            builder.Property(o => o.Note).IsRequired();
            builder.HasOne(o => o.Employee_Task).WithMany(m => m.Timesheets).HasForeignKey(k => k.EmployeeTaskId).IsRequired();
        }
    }
}
