using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_SuggestionMap : Common.CommonMap<EnablingObjective_Suggestion>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Suggestion> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Number).IsRequired();
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.EnablingObjective_Suggestions).HasForeignKey(k => k.EOId).IsRequired();
        }
    }
}
