using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class Position : Common.Entity
    {
        public int PositionNumber { get; set; }

        public string PositionAbbreviation { get; set; }

        public string PositionTitle { get; set; }

        public string PositionDescription { get; set; }

        public string HyperLink { get; set; }
        public bool IsPublished { get; set; }
        public byte[] PositionsFileUpload { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string FileName { get; set; }
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();

        public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();

        public virtual ICollection<Task_Position> Task_Positions { get; set; } = new List<Task_Position>();

        public virtual ICollection<ILA_Position_Link> ILA_Position_Links { get; set; } = new List<ILA_Position_Link>();

        public virtual ICollection<SimulatorScenarioPositon_Link_Old> SimulatorScenarioPositon_Links { get; set; } = new List<SimulatorScenarioPositon_Link_Old>();

        public virtual ICollection<Position_History> Position_Histories { get; set; } = new List<Position_History>();

        public virtual ICollection<Position_Task> Position_Tasks { get; set; } = new List<Position_Task>();

        public virtual ICollection<Positions_SQ> Position_SQs { get; set; } = new List<Positions_SQ>();

        public virtual ICollection<Position_Employee> Position_Employees { get; set; } = new List<Position_Employee>();

        public virtual ICollection<Version_Position> Version_Positions { get; set; } = new List<Version_Position>();

        public virtual ICollection<DIFSurvey> DIFSurveys { get; set; }= new List<DIFSurvey>();
        public virtual ICollection<SimulatorScenario_Position> SimulatorScenarioPositions { get; set; } = new List<SimulatorScenario_Position>();
        public virtual ICollection<TaskListReview_PositionLink> TaskListReview_PositionLinks { get; set; } = new List<TaskListReview_PositionLink>();
        public virtual ICollection<SimulatorScenario_Script> SimulatorScenario_Scripts { get; set; } = new List<SimulatorScenario_Script>();
        public Position()
        {
        }

        public Position(int positionNumber, string positionAbbreviation, string positionTitle, string positionDescription, string hyperLink, bool isPublished, byte[] positionsFileUpload,DateTime effectiveDate, string fileName)
        {
            PositionNumber = positionNumber;
            PositionAbbreviation = positionAbbreviation;
            PositionTitle = positionTitle;
            PositionDescription = positionDescription;
            HyperLink = hyperLink;
            IsPublished = isPublished;
            PositionsFileUpload = positionsFileUpload;
            EffectiveDate = effectiveDate;
            FileName = fileName;
        }
        public Position_Task LinkTask(Task task)
        {
            Position_Task pos_task_link = Position_Tasks.FirstOrDefault(x => x.TaskId == task.Id && x.PositionId == this.Id);
            if (pos_task_link != null)
            {
                return pos_task_link;
            }

            pos_task_link = new Position_Task(this, task);
            AddEntityToNavigationProperty<Position_Task>(pos_task_link);
            return pos_task_link;
        }
        public void UnlinkTask(Task task)
        {
            Position_Task pos_task_link = Position_Tasks.FirstOrDefault(x => x.TaskId == task.Id && x.PositionId == this.Id);
            if (pos_task_link != null)
            {
                RemoveEntityFromNavigationProperty<Position_Task>(pos_task_link);
            }
        }
        public Positions_SQ LinkSQ(EnablingObjective enablingObjective)
        {
            Positions_SQ pos_sqs_link = Position_SQs.FirstOrDefault(x => x.EOId == enablingObjective.Id && x.PositionId == this.Id);
            if (pos_sqs_link != null)
            {
                return pos_sqs_link;
            }

            pos_sqs_link = new Positions_SQ(this, enablingObjective);
            AddEntityToNavigationProperty<Positions_SQ>(pos_sqs_link);
            return pos_sqs_link;
        }
        public void UnlinkSQ(EnablingObjective enablingObjective)
        {
            Positions_SQ pos_sqs_link = Position_SQs.FirstOrDefault(x => x.EOId == enablingObjective.Id && x.PositionId == Id);
            if (pos_sqs_link != null)
            {
                RemoveEntityFromNavigationProperty<Positions_SQ>(pos_sqs_link);
            }
        }

        public EmployeePosition LinkEmployee(Employee employee, DateOnly startDate, bool trainee, int positionId)
        {
            var employeePosition = EmployeePositions.FirstOrDefault(p => p.PositionId == this.Id && p.EmployeeId == employee.Id);
            if (employeePosition != null)
            {
                return employeePosition;
            }

            employeePosition = new EmployeePosition(employee.Id, positionId, startDate, null, trainee, null, null, null, false);

            AddEntityToNavigationProperty<EmployeePosition>(employeePosition);

            return employeePosition;

            //Position_Employee pos_emp_link = Position_Employees.FirstOrDefault(x => x.EmployeeId == employee.Id && x.PositionId == this.Id);
            //if (pos_emp_link != null)
            //{
            //    return pos_emp_link;
            //}
            //pos_emp_link = new Position_Employee(this.Id, employee.Id, startDate, trainee);
            //AddEntityToNavigationProperty<Position_Employee>(pos_emp_link);
            //return pos_emp_link;
        }

        public void UnlinkEmployee(Employee employee)
        {
            EmployeePosition pos_emp_link = EmployeePositions.FirstOrDefault(x => x.EmployeeId == employee.Id && x.PositionId == this.Id);
            if (pos_emp_link != null)
            {
                RemoveEntityFromNavigationProperty<EmployeePosition>(pos_emp_link);
            }
        }
    }
}
