using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjective_SuggestionsMap : Common.CommonMap<Version_EnablingObjective_Suggestions>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective_Suggestions> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Number).IsRequired();
            builder.HasOne(o => o.EnablingObjective_Suggestion).WithMany(k => k.Version_EnablingObjective_Suggestions).HasForeignKey(f => f.EnablingObjective_SuugestionId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Version_EnablingObjective).WithMany(k => k.Version_EnablingObjective_Suggestions).HasForeignKey(f => f.Version_EOId).IsRequired();
        }
    }
}
