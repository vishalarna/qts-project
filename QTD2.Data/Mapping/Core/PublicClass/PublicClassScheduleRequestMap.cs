using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core.PublicClass
{
    public class PublicClassScheduleRequestMap : Common.CommonMap<PublicClassScheduleRequest>
    {
        public override void Configure(EntityTypeBuilder<PublicClassScheduleRequest> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.FirstName).HasMaxLength(100);
            builder.Property(o => o.LastName).HasMaxLength(100);
            builder.Property(o => o.Company);
            builder.Property(o => o.EmailAddress).HasMaxLength(100);
            builder.Property(o => o.IpAddress);
            builder.Property(o => o.NercCertNumber);
            builder.Property(o => o.NercCertificationType);
            builder.Property(o => o.CertificationExpirationDate);
            builder.Property(o => o.RequestDate);
            builder.Property(o => o.Status);
            builder.HasOne(o => o.ClassSchedule).WithMany(o => o.PublicClassScheduleRequests).HasForeignKey(o => o.ClassScheduleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ClassSchedule_Employee).WithOne(o => o.PublicClassScheduleRequest).HasForeignKey<PublicClassScheduleRequest>(o => o.ClassScheduleEmployeeId).OnDelete(DeleteBehavior.NoAction); 

        }
    }
}
