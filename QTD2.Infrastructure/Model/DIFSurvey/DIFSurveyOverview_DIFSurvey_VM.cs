using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyOverview_DIFSurvey_VM
    {
        public int Id { get; set; }
        public string SurveyTitle { get; set; }
        public string PositionTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string DevStatus { get; set; }
        public bool Editable { get; set; }
        public bool IsEmployeeCompleted { get; set; }
        public bool IsActive { get; set; }
        public string SurveyStatus { get; set; }

        public DIFSurveyOverview_DIFSurvey_VM(int _id, string _surveyTitle, string _positionTitle, DateTime _startDate, DateTime _dueDate, string _devStatus, bool _editable, bool _isEmployeeCompleted, bool _isActive, string _surveyStatus)
        {
            Id = _id;
            SurveyTitle = _surveyTitle;
            PositionTitle = _positionTitle;
            StartDate = _startDate;
            DueDate = _dueDate;
            DevStatus = _devStatus;
            Editable = _editable;
            IsEmployeeCompleted = _isEmployeeCompleted;
            IsActive = _isActive;
            SurveyStatus = _surveyStatus;
        }
    }
}
