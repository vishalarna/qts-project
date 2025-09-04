using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Procedure_Task_LinkMap : Common.CommonMap<Procedure_Task_Link>
    {
        public override void Configure(EntityTypeBuilder<Procedure_Task_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Procedure).WithMany(x => x.Procedure_Task_Links).HasForeignKey(x => x.ProcedureId).IsRequired();
            builder.HasOne(x => x.Task).WithMany(x => x.Procedure_Task_Links).HasForeignKey(x => x.TaskId).IsRequired();
            builder.HasIndex(x => new { x.ProcedureId, x.TaskId }).IsUnique();
        }
    }
}
