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
    public class ClassSchedule_Roster_ResponseMap : Common.CommonMap<ClassSchedule_Roster_Response>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Roster_Response> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.ClassSchedule_Roster).WithMany(r => r.Responses).HasForeignKey(y => y.ClassScheduleRosterId);
        }
    }
}

