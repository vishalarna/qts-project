using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyEmployeeVM
    {
        public int DifSurveyId { get; set; }
        public string Title { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
    }
}
