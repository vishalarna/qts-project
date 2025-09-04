using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskReQualificationEmp
{
    public class TaskReQualificationEmpSignOffVM
    {
        public int TaskQualificationId { get; set; }
        public bool? IsCriteriaMet { get; set; }
        public string Comments { get; set; }

        public int EvaluatorId { get; set; }
        public int? EvaluationMethodId { get; set; }
        public DateTime? TaskQualificationDate { get; set; }

        public int TraineeId { get; set; }
        public DateTime? SignOffDate { get; set; }
        public string EvaluatorName { get; set; }
        public string TraineeName { get; set; }
        public bool? IsFormSubmitted { get; set; }
        public bool? IsTraineeSignOff { get; set; }
        public bool? IsEvaluatorSignOff { get; set; }

    }

    public class TQEvaluatorDate {
    
    public string Name { get; set; }
    public DateTime? signOffDate { get; set; }
    
    }

    public class TaskReQualificationCompletedVM
    {
        public int TaskQualificationId { get; set; }
        public int TaskId { get; set; }
        public int TaskNumber { get; set; }

        public string TaskDescription { get; set; }
        public string Positions { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime? DueDate { get; set; }
      
        public int TraineeId { get; set; }

        public int EvaluatorId { get; set; }
        public DateTime? SignOffDate { get; set; }
        public List<string> EvaluatorNamesWithDates { get; set; }
        public List<TQEvaluatorDate> EvaluatorNameDates { get; set; }

        public string RequiredRequals { get; set; }

        public string EmployeeName { get; set; }

        public string ConcatenatedTaskNumber { get; set; }
        public TaskReQualificationCompletedVM()
        {

        }
        public TaskReQualificationCompletedVM(int taskQualificationId, int taskId, int taskNumber, string taskDescription, DateTime? releaseDate, DateTime? dueDate, int traineeId,
            int evaluatorId, DateTime? signOffDate, string concatenatedTaskNumber)
        {
            TaskQualificationId = taskQualificationId;
            TaskId = taskId;
            TaskNumber = taskNumber;
            TaskDescription = taskDescription;
            ReleaseDate = releaseDate;
            DueDate = dueDate;
            TraineeId = traineeId;
            EvaluatorId = evaluatorId;
            SignOffDate = signOffDate;
            ConcatenatedTaskNumber = concatenatedTaskNumber;
        }

        public void SetPositions(string positions)
        {
            Positions = positions;
        }

        public void SetEmployeeName(string name)
        {
            EmployeeName = name;
        }

        public void SetEvaluatorNamesWithDates(List<string> evaluatorNames)
        {
            EvaluatorNamesWithDates = evaluatorNames;
        }
    }
    
}
