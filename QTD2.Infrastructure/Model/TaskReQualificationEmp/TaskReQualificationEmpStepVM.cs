using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskReQualificationEmp
{
    public class TaskReQualificationEmpStepVM
    {
        public List<Steps> StepsList { get; set; }=new List<Steps>();
        public string TaskDescription { get; set; }
        public int TraineeId { get; set; }
        public int TaskQualificationId { get; set; }
        public int TaskId { get; set; }

    }
    public class Steps
    {
        public int StepId { get; set; }

        public string StepDescription { get; set; }

        public string Comments { get; set; }

        public bool? IsCompleted { get; set; }

        public List<StepCommentsVM> EvaluatorsStepComments { get; set; } = new List<StepCommentsVM>();

    }
    public class TaskReQualificatioFeedBackVM
    {
        public string TaskDescription { get; set; }
        public string concatednatedTaskNumber { get; set; }
        public List<Steps> StepsList { get; set; } = new List<Steps>();
        public List<QuesionAnswer> QuesionAnswerList { get; set; } = new List<QuesionAnswer>();

    }
    public class StepCommentsVM
    {
        public string EmployeeName { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public string Image { get; set; }

    }
}

