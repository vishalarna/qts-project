using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReview
{
    public class SubmitProcedureReviewDto
    {
        public int ProcedureReviewId { get; set; }
        public string? Response { get; set; }
        public string Comments { get; set; }
    }
}
