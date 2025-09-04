using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.Task;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQEmpWithTasksVM
    {
        public int EmpId { get; set; }

        public string EmpFName { get; set; }

        public string EmpLName { get; set; }

        public string EmpImage { get; set; }

        public string EmpEmail { get; set; }

        public List<TaskWithNumberVM> TasksWithNumber { get; set; } = new List<TaskWithNumberVM>();

        public string Positions { get; set; }

        public string Image { get; set; }

        public string EmployeeNumber { get; set; }

        public int? TqId { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public string Status { get; set; }

        public bool isLocked { get; set; }
    }

    public class TQTasksWithEmployeesVM
    {
        public int DANumber { get; set; }

        public int SDANumber { get; set; }

        public string Letter { get; set; }

        public Domain.Entities.Core.Task Task { get; set; }

       
        public List<TQTaskWithEmployeesListVM> TQTaskWithEmployeesList { get; set; } = new List<TQTaskWithEmployeesListVM>();
    }

    public class TQTaskWithEmployeesListVM
    {
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string EmpImage { get; set; }
        public string Positions { get; set; }

        public int TQId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string RequiredRequals { get; set; }

        public bool ReleaseToAllSingleSignOff { get; set; }

        public bool SignOffOrderEnabled { get; set; }

        public List<EvaluatorNameWithStatus> EvaluatorListWithStatus { get; set; } = new List<EvaluatorNameWithStatus>();
        public string TQStatus { get; set; }


    }

    public class EvaluatorNameWithStatus
    {
        public int EvaluatorId { get; set; }
        public string EvaluatorName { get; set; }
        public string Status { get; set; }
    }

}
