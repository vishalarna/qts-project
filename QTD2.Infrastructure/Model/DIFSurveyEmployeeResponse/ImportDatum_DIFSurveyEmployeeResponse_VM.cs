using QTD2.Infrastructure.Model.Import_CSV_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurveyEmployeeResponse
{
   public class ImportDatum_DIFSurveyEmployeeResponse_VM : ImportDatum_VM
    {
        public string EmployeeNumber { get; set; }
        public string TaskNumber { get; set; }
        public string Difficulty { get; set; }
        public string Importance { get; set; }
        public string Frequency { get; set; }
        public string NA { get; set; }
        public string Comments { get; set; }

    }
}
