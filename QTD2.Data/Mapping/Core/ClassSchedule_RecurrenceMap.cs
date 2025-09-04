using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassSchedule_RecurrenceMap : Common.CommonMap<ClassSchedule_Recurrence>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Recurrence> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ClassSchedule).WithOne(w => w.ClassSchedule_Recurrence).HasForeignKey<ClassSchedule_Recurrence>(f => f.ClassId).IsRequired();
        }
    }
}
