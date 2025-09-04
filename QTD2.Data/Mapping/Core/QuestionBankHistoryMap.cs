using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class QuestionBankHistoryMap : Common.CommonMap<QuestionBankHistory>
    {
        public override void Configure(EntityTypeBuilder<QuestionBankHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.QuestionBankId).IsRequired();
            builder.HasOne(o => o.QuestionBank).WithMany(x => x.QuestionBankHistories).HasForeignKey(y => y.QuestionBankId).IsRequired();
        }
    }
}


