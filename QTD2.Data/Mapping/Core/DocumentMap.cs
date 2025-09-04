using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;


namespace QTD2.Data.Mapping.Core
{
    public class DocumentMap : Common.CommonMap<Document>
    {
        public override void Configure(EntityTypeBuilder<Document> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.DocumentType).WithMany().HasForeignKey(f => f.DocumentTypeId).IsRequired();
            builder.Property(o => o.FileName).IsRequired();
            builder.Property(o => o.FilePath).IsRequired();
            builder.Property(o => o.DateAdded).IsRequired();
            builder.Property(o => o.LinkedDataId).IsRequired();
            builder.Property(o => o.Comments);
        }
    }
}
