using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class CertificationExpiringNotificationMap : Common.CommonMap<CertificationExpiringNotification>
    {
        public override void Configure(EntityTypeBuilder<CertificationExpiringNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                  .HasOne(x=>x.EmployeeCertification)
                           .WithMany()
                           .HasForeignKey(x=>x.EmployeeCertificationId)
                           .OnDelete(DeleteBehavior.NoAction);

        }
    }
}