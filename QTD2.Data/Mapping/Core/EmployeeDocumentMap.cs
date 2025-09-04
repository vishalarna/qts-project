using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmployeeDocumentMap : Common.CommonMap<EmployeeDocument>
    {
        public override void Configure(EntityTypeBuilder<EmployeeDocument> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.FileAsBase64);
            builder.Property(o => o.FileName);
            builder.Property(o => o.FileSize);
            builder.Property(o => o.FileSize);
            builder.HasOne(o => o.Employee).WithMany(o => o.EmployeeDocuments).HasForeignKey(k => k.EmployeeID).IsRequired();
        }
    }
}
