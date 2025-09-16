using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskReQualificationEmp
{
    public class TaskReQualificationEmpSuggestionVM
    {
        public List<Suggestion> SuggestionList { get; set; } = new List<Suggestion>();
        public string? TaskDescription { get; set; }
        public int TraineeId { get; set; }
        public int TaskQualificationId { get; set; }
        public int? TaskId { get; set; }
        public string concateNatedTaskNumber { get; set; }
        public string? SkillNumber { get; set; }
        public string? SkillDescription { get; set; }
        public int? SkillId { get; set; }
        public string? concateNatedSkillNumber { get; set; }
        public int SkillQualificationId { get; set; }

    }
    public class Suggestion
    {
        public int SuggestionId { get; set; }

        public string SuggesntionDescription { get; set; }

        public string Comments { get; set; }

        public bool IsCompleted { get; set; }
    }

}
