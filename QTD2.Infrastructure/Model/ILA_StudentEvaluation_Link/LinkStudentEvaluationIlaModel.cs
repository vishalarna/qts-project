using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_StudentEvaluation_Link
{
    public  class LinkStudentEvaluationIlaModel
    {

        public int studentEvalFormID { get; set; }
        public int? studentEvalAvailabilityID { get; set; }
        public int studentEvalAudienceID { get; set; }
        public int? ilaStudenEvaluationLinkId { get; set; }
        public bool? isAllQuestionMandatory { get; set; }
        public bool isEvalRemove { get; set; }

    }

    public class LinkStudentEvaluationIlaModelData
    {
        public List<LinkStudentEvaluationIlaModel> LinkStudentEvaluationIlaModelListIds { get; set; }
    }
}
