using System;

namespace QTD2.Infrastructure.Model.EmployeePosition
{
    public class EmployeePositionCreateOptions
    {
        public int PositionId { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? posQualificationDate { get; set; }
        public DateOnly? endDate { get; set; }

        public bool IsTrainee { get; set; }

        public bool IsSignificant { get; set; }

        public string ManagerName { get; set; }

        public string TrainingProgramVersion { get; set; }

        public bool? IsCertificationRequired { get; set; }
    }
}
