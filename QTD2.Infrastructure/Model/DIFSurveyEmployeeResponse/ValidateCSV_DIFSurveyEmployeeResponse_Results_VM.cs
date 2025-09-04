using QTD2.Infrastructure.Model.Import_CSV_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurveyEmployeeResponse
{
   public class ValidateCSV_DIFSurveyEmployeeResponse_Results_VM : ValidateCSV_Results_VM
    {
        public List<ImportDatum_DIFSurveyEmployeeResponse_VM> Data { get; set; }
    }
}
