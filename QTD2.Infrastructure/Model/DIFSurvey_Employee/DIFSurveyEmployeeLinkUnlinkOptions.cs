using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyEmployeeLinkUnlinkOptions
    {
        public int DifSurveyId{ get; set; }
        public List<int> EmployeeIds{ get; set; }
       
    }
}
