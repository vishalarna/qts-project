using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.MetaILA
{
    public class MetaILACreateOptions
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? MetaILAStatusId { get; set; }

        public string Reason { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int? MetaILA_SummaryFinalTestId { get; set; }
        public int? MetaILA_SummaryRetakeTestId { get; set; }

        public int? StudentEvaluationId { get; set; }
        public int? ProviderId { get; set; }
    }
}
