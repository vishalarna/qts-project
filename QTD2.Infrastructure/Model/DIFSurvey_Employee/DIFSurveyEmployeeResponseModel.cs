using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyEmployeeResponseModel
    {
        public int DIFSurveyTaskId { get; set; }
        public float? Difficulty { get; set; }
        public float? Importance { get; set; }
        public float? Frequency { get; set; }
        public bool NA { get; set; }
        public string? Comments { get; set; }
        public DIFSurveyEmployeeResponseModel()
        {

        }
    }

}
