using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQEmpWithPosAndTaskVM
    {

        public int? Id { get; set; }
        public int EmpId { get; set; }
        public int TaskId { get; set; }
        public DateTime? EmpReleaseDate { get; set; }
        public string EmployeeName { get; set; }
        public string EvaluatorNames { get; set; }
        public DateTime? DueDate { get; set; }
        public string TaskDescription { get; set; }
        public string PosNames { get; set; }
        public string Number { get; set; }
        public int? TotalRequiredSignOffs { get; set; }
        public int TotalCompletedSignOffs { get; set; }
        public List<TQEvalSignOffModel> TQEvalSignOffModels { get; set; }
        public DateTime? TaskQualificationDate { get; set; }
        public bool CriteriaMet { get; set; }
        public string? Comment { get; set; }

        public TQEmpWithPosAndTaskVM()
        {

        }

        public TQEmpWithPosAndTaskVM(
                int id, 
                int taskId, 
                int empId,
                string number,
                string taskDescription, 
                DateTime? dueDate, 
                List<TQEvalSignOffModel> tQEvalSignOffModels, 
                DateTime? empReleaseDate,
                string posNames,
                int? totalRequiredSignOffs, 
                string evaluatorNames, string? comment)
        {
            Id = id;
            TaskId = taskId;
            EmpId = empId;
            TaskDescription = taskDescription;
            DueDate = dueDate;
            TQEvalSignOffModels = tQEvalSignOffModels;
            EmpReleaseDate = empReleaseDate;
            Number = number;
            PosNames = posNames;
            TotalRequiredSignOffs = totalRequiredSignOffs;
            EvaluatorNames = evaluatorNames;
            Comment = comment;
        }

        public TQEmpWithPosAndTaskVM(int id, int taskId,string number, string taskDescription,int empId,string employeeName,string posNames, string evaluatorName, DateTime? empReleaseDate, DateTime? dueDate, int totalRequiredSignOffs)
        {
            Id = id;
            TaskId = taskId;
            Number = number;
            TaskDescription = taskDescription;
            EvaluatorNames = evaluatorName;
            EmpId = empId;
            EmployeeName = employeeName;
            PosNames = posNames;
            EmpReleaseDate = empReleaseDate;
            DueDate = dueDate;
            TotalRequiredSignOffs = totalRequiredSignOffs;
        }

        public TQEmpWithPosAndTaskVM(int id, int taskId, string number, string taskDescription, DateTime? dueDate,DateTime? empReleaseDate,int empId,string employeeName,DateTime? taskqualificationDate,bool criteriaMet)
        {
            Id = id;
            TaskId = taskId;
            EmpId = empId;
            TaskDescription = taskDescription;
            DueDate = dueDate;
            EmpReleaseDate = empReleaseDate;
            Number = number;
            EmployeeName = employeeName;
            TaskQualificationDate = taskqualificationDate;
            CriteriaMet = criteriaMet;
        }

        public void SetCompletedSignOffCount(int totalCompletedSignOffs)
        {
            TotalCompletedSignOffs = totalCompletedSignOffs;
        } 
    }
}
