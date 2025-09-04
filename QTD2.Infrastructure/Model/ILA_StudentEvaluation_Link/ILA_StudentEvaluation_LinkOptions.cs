using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_StudentEvaluation_Link
{
    public class ILA_StudentEvaluation_LinkOptions
    {
        public int ILAId { get; set; }

        public List<int> studentEvalFormIDs { get; set; }

        public int? studentEvalAvailabilityID { get; set; }

        public int? studentEvalAudienceID { get; set; }

        public bool isAllQuestionMandatory { get; set; }
    }
}
