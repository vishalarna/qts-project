using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class PersonMap : Common.CommonMap<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.MiddleName).HasMaxLength(50);
            builder.Property(o => o.LastName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.Username).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Image);
        }
    }
}
