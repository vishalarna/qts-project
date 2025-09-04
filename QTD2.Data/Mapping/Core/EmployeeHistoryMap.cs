using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmployeeHistoryMap : Common.CommonMap<EmployeeHistory>
    {
        public override void Configure(EntityTypeBuilder<EmployeeHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.Property(o => o.OperationType);
            builder.Property(o => o.CurrentActiveStatus);
            builder.HasOne(o => o.Employee).WithMany(x => x.EmployeeHistorys).HasForeignKey(y => y.EmployeeID).IsRequired();
        }
    }
}
