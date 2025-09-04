using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Procedure_Tool_LinkMap : Common.CommonMap<Version_Procedure_Tool_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_Procedure_Tool_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Version_Tool).WithMany(m => m.Version_Procedure_Tool_Links).HasForeignKey(k => k.Version_ToolId).IsRequired();
            builder.HasOne(o => o.Version_Procedure).WithMany(m => m.Version_Procedure_Tool_Links).HasForeignKey(k => k.Version_ProcedureId).IsRequired();
        }
    }
}
