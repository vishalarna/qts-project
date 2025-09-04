using QTD2.Domain.Entities.Common;
using System;

namespace QTD2.Domain.Entities.Core
{
    public class EmployeePosition : Entity
    {
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool Trainee { get; set; }

        public DateOnly? QualificationDate { get; set; }

        public string ManagerName { get; set; }

        public string TrainingProgramVersion { get; set; }

        public bool IsSignificant { get; set; }

        public bool? IsCertificationNotRequired { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Position Position { get; set; }

        public EmployeePosition(int employeeId, int positionId, DateOnly startDate, DateOnly? endDate, bool trainee, DateOnly? qualificationDate, string managerName, string trainingProgramVersion, bool isSignificant,bool? isCertificationNotRequired = false)
        {
            EmployeeId = employeeId;
            PositionId = positionId;
            StartDate = startDate;
            SetEndDate(endDate);
            Trainee = trainee;
            QualificationDate = qualificationDate;
            ManagerName = managerName;
            TrainingProgramVersion = trainingProgramVersion;
            IsSignificant = isSignificant;
            IsCertificationNotRequired = isCertificationNotRequired;
        }

        public EmployeePosition(int employeeId, int positionId)
        {
            EmployeeId = employeeId;
            PositionId = positionId;
        }
        public EmployeePosition()
        {
        }

        public EmployeePosition SetAsQualified(DateOnly qualificationDate)
        {
            QualificationDate = qualificationDate;
            Trainee = false;
            return this;
        }

        public EmployeePosition SetEndDate(DateOnly? endDate)
        {
            EndDate = endDate;
            if (EndDate.HasValue && EndDate.Value < DateOnly.FromDateTime(DateTime.Today))
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
            return this;
        }
    }
}
