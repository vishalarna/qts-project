using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core.Notifications
{
    public class PublicClassScheduleRequestNotificationMap : IEntityTypeConfiguration<PublicClassScheduleRequestNotification>
    {
        public void Configure(EntityTypeBuilder<PublicClassScheduleRequestNotification> builder)
        {
            builder.HasOne(n => n.PublicClassScheduleRequest)
                .WithMany()
                .HasForeignKey(n => n.PublicClassScheduleRequestNotification_PublicClassScheduleRequestId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
