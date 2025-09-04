using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_Collaborator_LinkMap : Common.CommonMap<Task_Collaborator_Link>
    {
        public override void Configure(EntityTypeBuilder<Task_Collaborator_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task).WithMany(m => m.Task_Collaborator_Links).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.Task_CollaboratorInvitation).WithMany(m => m.Task_Collaborator_Links).HasForeignKey(k => k.TaskCollabInviteId).IsRequired();
            builder.Property(o => o.isTaskCreator).IsRequired();
            builder.HasIndex(i => new { i.TaskCollabInviteId, i.TaskId }).IsUnique();
        }
    }
}
