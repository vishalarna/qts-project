using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReview
{
    public class ProcedureReviewVM
    {
        public int ProcedureReviewPublished { get; set; }

        public int ProcedureReviewNumberofEmployeesPending { get; set; }

        public int ProcedureReviewInDrafts { get; set; }
    }
}
