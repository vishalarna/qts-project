using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CBT_ScormRegistration : Common.Entity
    {
        public int CBTScormUploadId { get; set; }
        public int ClassScheduleEmployeeId { get; set; }

        // from scorm registration 
        public string LaunchLink { get; set; }

        // results from Scorm
        public CBT_ScormRegistrationCompletion RegistrationCompletion { get; set; }
        public CBT_ScormRegistrationSuccess RegistrationSuccess { get; set; }
        public double? Score { get; set; }
        public string? Grade { get; set; }
        public decimal? PassingScore { get; set; }
        public CBT_PassingScoreSource PassingScoreSource { get; set; }

        public virtual CBT_ScormUpload ScormUpload { get; set; }
        public virtual ClassSchedule_Employee ClassScheduleEmployee { get; set; }
        public DateTime? CompletedDate { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public string? ScoreUpdateMethod { get; set; }
        public string? ScoreUpdatedBy { get; set; }
        public string? GradeUpdateMethod { get; set; }
        public string? GradeUpdatedBy { get; set; }
        public string? CompletedDateUpdateMethod { get; set; }
        public string? CompletedDateUpdatedBy { get; set; }

        public virtual ICollection<CBT_ScormRegistration_Response> CBT_ScormRegistration_Responses { get; set; } = new List<CBT_ScormRegistration_Response>();

        public CBT_ScormRegistration()
        {

        }

        public CBT_ScormRegistration(int cBTScormUploadId, int classScheduleEmployeeId)
        {
            ClassScheduleEmployeeId = classScheduleEmployeeId;
            CBTScormUploadId = cBTScormUploadId;
        }

        public void Register(string launchLink)
        {
            LaunchLink = launchLink;
        }

        public void ReportResults(decimal passingScore, CBT_PassingScoreSource passingScoreSource, double score, CBT_ScormRegistrationCompletion completion, CBT_ScormRegistrationSuccess success, string updateMethod, string updatedBy)
        {
            PassingScore = passingScore;
            PassingScoreSource = passingScoreSource;
            RegistrationCompletion = completion;
            RegistrationSuccess = success;

            UpdateScore(score, updateMethod, updatedBy);
            UpdateGrade((decimal)Score >= passingScore ? "P" : "F", updateMethod, updatedBy);

            if (RegistrationCompletion == CBT_ScormRegistrationCompletion.COMPLETED && !CompletedDate.HasValue)
                UpdateCompletedDate(DateTime.UtcNow, updateMethod, updatedBy);
        }

        public void UpdateScore(double score, string updateMethod, string updatedBy)
        {
            Score = score;
            ScoreUpdateMethod = updateMethod;
            ScoreUpdatedBy = updatedBy;
        }

        public void UpdateGrade(string grade, string updateMethod, string updatedBy)
        {
            Grade = grade;
            GradeUpdateMethod = updateMethod;
            GradeUpdatedBy = updatedBy;
        }

        public void UpdateCompletedDate(DateTime completedDate, string updateMethod, string updatedBy)
        {
            CompletedDate = completedDate;
            CompletedDateUpdateMethod = updateMethod;
            CompletedDateUpdatedBy = updatedBy;
            AddDomainEvent(new Domain.Events.Core.OnCBT_ScormRegistration_Completed(this));
        }

        public void LocalizeRegistrationLink(string domain)
        {
            LaunchLink = domain + LaunchLink;
        }

        public void UpdateScormUpload(int cBTScormUploadId)
        {
            CBTScormUploadId = cBTScormUploadId;
        }

        public void AddResponse(CBT_ScormUpload_Question_Choice choice)
        {
            if (CBT_ScormRegistration_Responses == null) CBT_ScormRegistration_Responses = new List<CBT_ScormRegistration_Response>();

            var response = CBT_ScormRegistration_Responses
                .Where(r => r.CBT_ScormUpload_Question_Choice.CBT_ScormUpload_Question == choice.CBT_ScormUpload_Question)
                .FirstOrDefault();

            if (response == null)
            {
                CBT_ScormRegistration_Responses.Add(new CBT_ScormRegistration_Response(this, choice));
            }
            else
            {
                response.UpdateChoice(choice);
            }
        }

        public void UpdateTimeTracked(List<string> timeTracked)
        {
            if (timeTracked == null || timeTracked.Count() == 0) return;

            TimeSpan? totalTimeTracked = null;

            foreach(var time in timeTracked)
            {
                TimeSpan timeTrackedSpan;
                bool parsed = TimeSpan.TryParse(time, out timeTrackedSpan);

                if (totalTimeTracked == null) totalTimeTracked = timeTrackedSpan;

                else totalTimeTracked?.Add(timeTrackedSpan);
            }
            

             TotalTime = totalTimeTracked;
        }

        public void UpdateTimeTracked(string timeTracked)
        {
            TimeSpan timeTrackedSpan;

            bool parsed = TimeSpan.TryParse(timeTracked, out timeTrackedSpan);

            if (parsed) TotalTime = timeTrackedSpan;
        }

        public void UpdateLastAccessDate(string lastAccessDate)
        {
            DateTime lastAccessDateDateTime;

            bool parsed = DateTime.TryParse(lastAccessDate, out lastAccessDateDateTime);

            if (parsed) LastAccessDate = lastAccessDateDateTime;
        }

        public void UpdateLastAccessDate(DateTime? lastAccessDate)
        {
            LastAccessDate = lastAccessDate;
        }
    }
}
