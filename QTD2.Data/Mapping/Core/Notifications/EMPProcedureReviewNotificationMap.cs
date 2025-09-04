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
    public class EMPProcedureReviewNotificationMap : Common.CommonMap<EMPProcedureReviewNotification>
    {
        public override void Configure(EntityTypeBuilder<EMPProcedureReviewNotification> builder)
        {
            builder.HasBaseType<Notification>();

            builder
                .HasOne(x=>x.ProcedureReview_Employee)
                .WithMany()
                .HasForeignKey(x => x.ProcedureReview_EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}