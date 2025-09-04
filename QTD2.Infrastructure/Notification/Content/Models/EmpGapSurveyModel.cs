using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
    public class EmpGapSurveyModel
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string PositionTitle { get; set; }
        public string SurveyTitle { get; set; }
        public DateTime SurveyStartDate { get; set; }
        public DateTime SurveyEndDate { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public EmpGapSurveyModel(string firstName,string lastName, string positionTitle,string surveytitle,DateTime surveyStartDate,DateTime surveyEndDate, string defaultTimeZoneId)
        {
            EmployeeFirstName = firstName;
            EmployeeLastName = lastName;
            PositionTitle = positionTitle;
            SurveyTitle = surveytitle;
            SurveyStartDate = SurveyStartDate;
            SurveyEndDate = surveyEndDate;
            DefaultTimeZoneId = defaultTimeZoneId;
        }
    }

    
}
