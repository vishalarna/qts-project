using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationEmpVM
    {

        public int? Id { get; set; }

        public int EmpId { get; set; }

        public int TaskId { get; set; }

        public string EmpName { get; set; }

        public string EmpImage { get; set; }

        public string EmpEmail { get; set; }
        
        public DateTime? EmpReleaseDate { get; set; }

        public DateTime? QualificationDate { get; set; }

        public string EvaluatorName { get; set; }

        public DateTime? DueDate { get; set; }

        public bool CriteriaMet { get; set; }

        public string Comments { get; set; }

        public string TaskNumber { get; set; }

        public string Number { get; set; }

        public string TaskLetter { get; set; }

        public string TaskDescription { get; set; }

        public string Status { get; set; }

        public string ToolTip { get; set; }

        public string RequiredRequals { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public List<string> PosIds { get; set; } = new List<string>();
        public string PosNames { get; set; }
        public bool? IsReliability { get; set; }
        public bool? Active { get; set; }
        public bool? IsRecalled { get; set; }

        public TaskQualificationEmpVM()
        {

        }

        public TaskQualificationEmpVM(string status, string toolTip, int taskId, int empId, string empEmail, string empImage, string empName, 
            DateTime? qualificationDate, string comments, string taskDescription, bool criteriaMet, DateTime? createdDate, DateTime? dueDate, DateTime? modifiedDate, int? id, 
            string taskNumber, string taskLetter,string evaluatorName, DateTime? empReleaseDate, string requiredRequals, bool isReliability, bool active, bool isRecalled)
        {
            Status = status;
            ToolTip = toolTip;
            TaskId = taskId;
            EmpId = empId;
            EmpEmail = empEmail;
            EmpImage = empImage;
            EmpImage = empImage;
            EmpName = empName;
            QualificationDate = qualificationDate;
            Comments = comments;
            TaskDescription = taskDescription;
            CriteriaMet = criteriaMet;
            CreatedDate = createdDate;
            DueDate = dueDate;
            ModifiedDate = modifiedDate;
            Id = id;
            TaskNumber = taskNumber;
            Number = taskNumber;
            TaskLetter = taskLetter;
            EvaluatorName = evaluatorName;
            EmpReleaseDate = empReleaseDate;
            RequiredRequals = requiredRequals;
            IsReliability = isReliability;
            Active = active;
            IsRecalled = isRecalled;
        }
    }
}
