using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReview_Employee
{
    public class ProcedureReview_EmployeeCreateOptions
    {
        public int procedureReviewId { get; set; }

        public int[] employeeIds { get; set; }
    }
}
