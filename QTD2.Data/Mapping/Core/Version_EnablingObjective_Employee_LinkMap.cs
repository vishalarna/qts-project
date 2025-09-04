using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjective_Employee_LinkMap : Common.CommonMap<Version_EnablingObjective_Employee_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective_Employee_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Version_EmployeeId).IsRequired();
            builder.Property(o => o.Version_EnablingObjectiveId).IsRequired();
            builder.Property(o => o.StartDate);

            builder.HasOne(o => o.Version_Employee).WithMany(x => x.Version_EnablingObjective_Employee_Links).HasForeignKey(i => i.Version_EmployeeId).IsRequired();
            builder.HasOne(o => o.Version_EnablingObjective).WithMany(x => x.Version_EnablingObjective_Employee_Links).HasForeignKey(i => i.Version_EnablingObjectiveId).IsRequired();
        }
    }
}
