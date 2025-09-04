using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class IlaApplicationOptions
    {
        public int ilaId { get; set; }
        public string? OtherAssesmentTool { get; set; }
        public string? OtherNercTargetAudience { get; set; }
        public DateOnly? startDate { get; set; }
        public DateOnly? applicationSubmissionDatertDate { get; set; }
        public DateOnly? approvalDate { get; set; }
        public DateOnly? expirationDate { get; set; }
        public bool? hasPilotData { get; set; }  //2nd checkbox make nullable
        public bool? hasPilotDataNA { get; set; }  //2nd checkbox make nullable
        public bool? DoesActivityConform { get; set; }  //2nd checkbox make nullable

        public bool? WriitenOrOnlineAssessmentTool { get; set; }

        public bool? PerformAssessmentTool { get; set; }
        //public List<int> assesmentIds { get; set; }
        public List<int> trainingTopicIds { get; set; }
        public List<int> nercTargetIds { get; set; }

    }
}
