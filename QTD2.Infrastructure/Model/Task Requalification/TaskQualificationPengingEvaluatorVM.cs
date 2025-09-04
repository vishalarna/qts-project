using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationPengingEvaluatorVM
    {

        public int? Id { get; set; }
        public int EmpId { get; set; }
        public int TaskId { get; set; }
        public string EmpFName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpImage { get; set; }
        public string EmpEmail { get; set; }
        public string EmpNumber { get; set; }
        public string EmpPositions { get; set; }
        public string TaskFullNumber { get; set; }
        public int TaskNumber { get; set; }
        public string TaskLetter { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? EmpReleaseDate { get; set; }
        public string RequiredRequals { get; set; }
        public bool ReleaseToAllSingleSignOff { get; set; }
        public bool SignOffOrderEnabled { get; set; }
        public List<EvaluatorNameWithStatus> EvaluatorListWithStatus { get; set; } = new List<EvaluatorNameWithStatus>();
        public string Comments { get; set; }
        public string Status { get; set; }
        public string ToolTip { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool CanStart { get; set; }

        public TaskQualificationPengingEvaluatorVM()
        {

        }

        public TaskQualificationPengingEvaluatorVM(int id,  int empId, int taskId, string empFName, string empLastName, string empEmail, string empImage, string empNumber,string empPositions, string taskFullNumber,int taskNumber, string taskLetter, string taskDescription, DateTime? dueDate, DateTime? empReleaseDate, string requiredRequals, string tqStatus)
        {
            Id = id;
            EmpId = empId;
            TaskId = taskId;
            EmpFName = empFName;
            EmpLastName = empLastName;
            EmpEmail = empEmail;
            EmpImage = empImage;
            EmpNumber = empNumber;
            EmpPositions = empPositions;
            TaskFullNumber = taskFullNumber;
            TaskNumber = taskNumber;
            TaskLetter = taskLetter;
            TaskDescription = taskDescription;
            DueDate = dueDate;
            EmpReleaseDate = empReleaseDate;
            RequiredRequals = requiredRequals;
            Status = tqStatus;
        }
    }
}
