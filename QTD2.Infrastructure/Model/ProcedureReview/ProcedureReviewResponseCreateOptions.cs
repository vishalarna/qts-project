using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReview
{
    public class ProcedureReviewResponseCreateOptions
    {
        public string Response { get; set; }
        public string Comments { get; set; }
        public int ProcedureReviewId { get; set; }
    }
}
