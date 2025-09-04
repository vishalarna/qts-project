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
  public  class ActionItem_OperationTypesMap : Common.CommonMap<ActionItem_OperationTypes>
    {
        public override void Configure(EntityTypeBuilder<ActionItem_OperationTypes> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Type);
        }
    }
}