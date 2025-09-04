using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyVM
    {
        public int Id { get; set; }
        public string SurveyTitle { get; set; }
        public int PositionId{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Instructions { get; set; }
        public bool ReleasedToEMP { get; set; }
        public int DevStatusId { get; set; }
        public bool IsActive { get; set; }
        public List<DIFSurveyTaskVM> Tasks { get; set; } = new List<DIFSurveyTaskVM>();
        public List<DIFSurvey_EmployeeVM> Employees { get; set; } = new List<DIFSurvey_EmployeeVM>();
        public string SurveyStatus { get; set; }

        public DIFSurveyVM(int id, string surveyTitle, int positionId, DateTime startDate, DateTime dueDate,string instructions,bool releasedToEMP, int devStatusId, bool isActive, string surveyStatus)
        {
            Id = id;
            SurveyTitle = surveyTitle;
            PositionId = positionId;
            StartDate = startDate;
            DueDate = dueDate;
            Instructions = instructions;
            DevStatusId = devStatusId;
            ReleasedToEMP = releasedToEMP;
            IsActive = isActive;
            SurveyStatus = surveyStatus;
        }
        public DIFSurveyVM(){
            
        }
    }
}
