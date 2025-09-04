using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ClassScheduleEmployee_ILACertificationLink_PartialCreditMap : Common.CommonMap<ClassScheduleEmployee_ILACertificationLink_PartialCredit>
    {
        public override void Configure(EntityTypeBuilder<ClassScheduleEmployee_ILACertificationLink_PartialCredit> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ClassScheduleEmployeeId).IsRequired();
            builder.Property(o => o.ILACertificationLinkId).IsRequired();
            builder.Property(o => o.PartialCreditHours);
            builder.HasOne(o => o.ClassSchedule_Employee).WithMany(m => m.ClassScheduleEmployee_ILACertificationLink_PartialCredits).HasForeignKey(k => k.ClassScheduleEmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ILACertificationLink).WithMany(m => m.ClassScheduleEmployee_ILACertificationLink_PartialCredits).HasForeignKey(k => k.ILACertificationLinkId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}