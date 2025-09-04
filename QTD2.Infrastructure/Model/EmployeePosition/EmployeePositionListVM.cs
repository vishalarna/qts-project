using System;

namespace QTD2.Infrastructure.Model.EmployeePosition
{
    public class EmployeePositionListVM
    {
        public bool Active { get; set; }
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }

        public string PositionTitle { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool Trainee { get; set; }

        public DateOnly? QualificationDate { get; set; }

        public string ManagerName { get; set; }

        public string TrainingProgramVersion { get; set; }

        public bool IsSignificant { get; set; }

        public bool? IsCertificationNotRequired { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
