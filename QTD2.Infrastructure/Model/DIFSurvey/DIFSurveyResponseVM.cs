using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyResponseVM
    {
        public int DIFSurveyTaskId { get; set; }
        public string Number { get; set; }
        public string TaskDescription { get; set; }
        public string Instructions { get; set; }
        public float? Difficulty { get; set; }
        public float? Importance { get; set; }
        public float? Frequency { get; set; }
        public bool? NA { get; set; }
        public string? Comments { get; set; }

        public DIFSurveyResponseVM(int difSurveyTaskId, string number, string taskDescription)
        {
            DIFSurveyTaskId = difSurveyTaskId;
            Number = number;
            TaskDescription = taskDescription;
        }

        public void UpdateResponse(float? difficulty, float? importance, float? frequency, bool? na, string? comments)
        {
            Difficulty = difficulty;
            Importance = importance;
            Frequency = frequency;
            NA = na;
            Comments = comments;
        }
    }
}
