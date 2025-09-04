using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class CollaboratorInvitationMap : Common.CommonMap<CollaboratorInvitation>
    {
        public override void Configure(EntityTypeBuilder<CollaboratorInvitation> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.InviteeEmailID).IsRequired().HasMaxLength(100);
            builder.Property(o => o.InvitedMessage).IsRequired().HasMaxLength(100);
            builder.Property(o => o.InviteDate).IsRequired();
            builder.HasOne(o => o.InviteeEmployee).WithMany(m => m.CollaboratorInvitations).HasForeignKey(k => k.InviteeEID).IsRequired();
            builder.HasOne(o => o.InvitedByEmployee).WithMany().HasForeignKey(k => k.InvitedByEID).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
