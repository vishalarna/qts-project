using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Data.Mapping.Common;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmployeeCertificationHistoryMap : CommonMap<EmployeeCertifictaionHistory>
    {
        public override void Configure(EntityTypeBuilder<EmployeeCertifictaionHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes).HasMaxLength(200);
            builder.Property(o => o.CertificationNumber);
            builder.HasOne(o => o.EmployeeCertification).WithMany(x => x.EmployeeCertificationHistorys).HasForeignKey(y => y.EmployeeCertificationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
