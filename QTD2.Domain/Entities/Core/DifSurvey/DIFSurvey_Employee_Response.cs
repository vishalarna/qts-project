using QTD2.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DIFSurvey_Employee_Response : Common.Entity
    {
        public int DIFSurvey_EmployeeId { get; set; }
        public int DIFSurvey_TaskId { get; set; }
        public float? Difficulty { get; set; }
        public float? Importance { get; set; }
        public float? Frequency { get; set; }
        public bool NA { get; set; }
        public string? Comments { get; set; }
        public virtual DIFSurvey_Employee DIFSurvey_Employee { get; set; }
        public virtual DIFSurvey_Task DIFSurvey_Task { get; set; }


        public void UpdateResponse(float? difficulty, float? importance, float? frequency, bool nA, string? comments)
        {
            if (nA)
            {
                Difficulty = null;
                Importance = null;
                Frequency = null;
                NA = true;
            }
            else
            {
                Difficulty = difficulty;
                Importance = importance;
                Frequency = frequency;
                NA = false;
            }
            Comments = comments;
            AddDomainEvent(new On_DIFSurvey_Employee_Response_Updated(this));
        }
        public DIFSurvey_Employee_Response(int difSurveyEmployeeId, int difSurveyTaskId)
        {
            DIFSurvey_EmployeeId = difSurveyEmployeeId;
            DIFSurvey_TaskId = difSurveyTaskId;
        }
        public DIFSurvey_Employee_Response(int difSurveyEmployeeId, int difSurveyTaskId, float? difficulty, float? importance, float? frequency, bool nA, string? comments)
        {
            DIFSurvey_EmployeeId = difSurveyEmployeeId;
            DIFSurvey_TaskId = difSurveyTaskId;
            Difficulty = difficulty;
            Importance = importance;
            Frequency = frequency;
            NA = nA;
            Comments = comments;
        }
        public DIFSurvey_Employee_Response()
        {

        }
    }
}
