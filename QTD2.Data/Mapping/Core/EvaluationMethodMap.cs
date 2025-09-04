using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EvaluationMethodMap : Common.CommonMap<EvaluationMethod>
    {
        public override void Configure(EntityTypeBuilder<EvaluationMethod> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
