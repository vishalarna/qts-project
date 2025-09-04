using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassSchedule_RosterMap : Common.CommonMap<ClassSchedule_Roster>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Roster> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Employee).WithMany(w => w.ClassSchedule_Rosters).HasForeignKey(f => f.EmpId).IsRequired();
            builder.HasOne(o => o.Test).WithMany(w => w.ClassSchedule_Rosters).HasForeignKey(f => f.TestId).IsRequired();
            builder.HasOne(o => o.TestType).WithMany(w => w.ClassSchedule_Rosters).HasForeignKey(f => f.TestTypeId).IsRequired();

            builder.HasOne(o => o.ClassSchedule).WithMany(w => w.ClassSchedule_Rosters).HasForeignKey(f => f.ClassScheduleId);
            builder.HasOne(o => o.MetaILA_Employee).WithMany().HasForeignKey(f => f.MetaIla_EmployeeId);
        }
    }
}
