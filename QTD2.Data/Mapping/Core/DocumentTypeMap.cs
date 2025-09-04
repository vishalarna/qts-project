using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;


namespace QTD2.Data.Mapping.Core
{
    public class DocumentTypeMap : Common.CommonMap<DocumentType>
    {
        public override void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.LinkedDataType).IsRequired();
        }
    }
}
