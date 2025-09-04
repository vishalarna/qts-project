using System;

namespace QTD2.Infrastructure.Model.EmployeePosition
{
    public class EmployeePositionUpdateOptions
    {
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool Trainee { get; set; }

        public DateOnly? QualificationDate { get; set; }

        public string ManagerName { get; set; }

        public string TrainingProgramVersion { get; set; }

        public bool? IsCertificationRequired { get; set; }
        public bool IsSignificant { get; set; }
        public int EmployeePositionId { get; set; }
    }
}
