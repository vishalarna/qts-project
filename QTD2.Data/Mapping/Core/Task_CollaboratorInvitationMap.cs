using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_CollaboratorInvitationMap : Common.CommonMap<Task_CollaboratorInvitation>
    {
        public override void Configure(EntityTypeBuilder<Task_CollaboratorInvitation> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.InvitedByEId).IsRequired();
            builder.Property(o => o.InviteeEId);
            builder.Property(o => o.InviteeEmail).HasMaxLength(100).IsRequired();
            builder.Property(o => o.InvitedForTaskId).IsRequired();
            builder.Property(o => o.InviteDate).IsRequired();
            builder.Property(o => o.Message).HasMaxLength(200);
        }
    }
}
