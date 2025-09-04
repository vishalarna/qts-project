using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DIFSurvey : Common.Entity
    {
        public string Title { get; set; }
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Instructions { get; set; }
        public int? DevStatusId { get; set; }
        public bool? ReleasedToEMP { get; set; }
        public bool? HistoricalOnly { get; set; }

        public virtual Position Position { get; set; }
        public virtual DIFSurvey_DevStatus DevStatus { get; set; }
        public virtual List<DIFSurvey_Employee> Employees { get; set; } = new List<DIFSurvey_Employee>();
        public virtual List<DIFSurvey_Task> Tasks { get; set; } = new List<DIFSurvey_Task>();

        public DIFSurvey() { }

        public DIFSurvey(string title, int positionId, DateTime startDate, DateTime dueDate, string? instructions,int? devStatusId)
        {
            Title = title;
            PositionId = positionId;
            StartDate = startDate;
            DueDate = dueDate;
            Instructions = instructions;
            DevStatusId = devStatusId;
        }

        public void Publish(string? licenseType, bool isReleaseToEMP)
        {
            if (licenseType?.ToUpper() == "DELUXE VERSION")
            {
                ReleasedToEMP = isReleaseToEMP;
            }

            DevStatusId = 2;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetPositionId(int positionId)
        {
            PositionId = positionId;
        }

        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        public void SetDueDate(DateTime dueDate)
        {
            DueDate = dueDate;
        }

        public void SetInstructions(string instruction)
        {
            Instructions = instruction;
        }

        public DIFSurvey_Task LinkTask(Task task)
        {
            DIFSurvey_Task difSurvey_task_link = Tasks.FirstOrDefault(x => x.DifSurveyId == this.Id && x.TaskId == task.Id);
            if (difSurvey_task_link != null)
            {
                return difSurvey_task_link;
            }

            difSurvey_task_link = new DIFSurvey_Task(this.Id, task.Id,null,null,null);
            AddEntityToNavigationProperty<DIFSurvey_Task>(difSurvey_task_link);
            return difSurvey_task_link;
        }

        public DIFSurvey_Employee LinkEmployee(Employee employee)
        {
            DIFSurvey_Employee difSurvey_emp_link = Employees.FirstOrDefault(x => x.DIFSurveyId == this.Id && x.EmployeeId == employee.Id);
            if (difSurvey_emp_link != null)
            {
                return difSurvey_emp_link;
            }

            difSurvey_emp_link = new DIFSurvey_Employee(this,employee,1);
            AddEntityToNavigationProperty<DIFSurvey_Employee>(difSurvey_emp_link);
            return difSurvey_emp_link;
        }
        public string SurveyStatus
        {
            get
            {
                if (StartDate > DateTime.Now.Date)
                    return "Scheduled";
                else if (DueDate >= DateTime.Now.Date)
                    return "Open";
                else
                    return "Closed";
            }
        }

    }
}
