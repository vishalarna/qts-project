using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyEmployeeResponseOptions
    {
        public List<DIFSurveyEmployeeResponseModel> Responses { get; set; } = new List<DIFSurveyEmployeeResponseModel>();
        public string? Comments { get; set; }

        public DIFSurveyEmployeeResponseOptions()
        {

        }
    }

}
