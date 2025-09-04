using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyTaskVM
    {
        public int Id{ get; set; }
        public int TaskId{ get; set; }
        public int DifSurveyId{ get; set; }
        public string TaskNumber{ get; set; }
        public string TaskDescription{ get; set; }
        public DIFSurveyTaskVM(int id, int taskId, int difSurveyId, string taskNumber, string taskDescription)
        {
            Id = id;
            TaskId = taskId;
            DifSurveyId = difSurveyId;
            TaskNumber = taskNumber;
            TaskDescription = taskDescription;
        }
        public DIFSurveyTaskVM()
        {

        }
    }

}
