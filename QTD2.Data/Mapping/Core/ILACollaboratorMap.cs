using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILACollaboratorMap : Common.CommonMap<ILACollaborator>
    {
        public override void Configure(EntityTypeBuilder<ILACollaborator> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILACollaborators).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.CollaboratorInvitation).WithMany(m => m.ILACollaborators).HasForeignKey(k => k.CollaboratorInviteId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.CollaboratorInviteId }).IsUnique();
        }
    }
}
