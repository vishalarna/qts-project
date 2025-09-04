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
    public class EMPIdpReviewNotificationMap : Common.CommonMap<EMPIdpReviewNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPIdpReviewNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasOne(x=>x.IDP)
                .WithMany()
                .HasForeignKey(x =>x.IDPId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}