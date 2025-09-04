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
    public class ClassSchedule_Roster_SelectionMap : Common.CommonMap<ClassSchedule_Roster_Response_Selection>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Roster_Response_Selection> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Response).WithMany(m => m.Selections).HasForeignKey(y => y.ClassScheduleRosterResponseId);
        }
    }
}

