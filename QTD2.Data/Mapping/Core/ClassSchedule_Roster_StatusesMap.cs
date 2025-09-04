using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassSchedule_Roster_StatusesMap : Common.CommonMap<ClassSchedule_Roster_Statuses>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Roster_Statuses> builder)
        {
            base.Configure(builder);
        }
    }
}
