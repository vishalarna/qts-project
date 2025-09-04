using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class AdminMessage_QTDUsersMap : Common.CommonMap<AdminMessage_QTDUser>
    {
        public override void Configure(EntityTypeBuilder<AdminMessage_QTDUser> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.AdminMessage).WithMany(o => o.AdminMessage_QTDUsers).HasForeignKey(o => o.AdminMessageId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.QTDUser).WithMany(o => o.AdminMessage_QTDUsers).HasForeignKey(o => o.QTDUserId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(o => o.Dismissed);
        }
    }
}
