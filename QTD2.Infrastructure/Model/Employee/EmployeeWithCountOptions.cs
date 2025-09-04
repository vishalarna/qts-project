using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeWithCountOptions
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public bool Active { get; set; }
        public string StartDate { get; set; }
        public string PosQualificationDate { get; set; }
        public bool TraineeStatus { get; set; }
        public string PositionTitle { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public EmployeeWithCountOptions()
        {
        }

        public EmployeeWithCountOptions(string number, string description, int id, int linkCount, bool active, bool traineeStatus, string startDate, string posQualficationDate, string positionTitle, string email = "", string image = "")
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
            TraineeStatus = traineeStatus;
            StartDate = startDate;
            PosQualificationDate = posQualficationDate;
            PositionTitle = positionTitle;
            Email = email;
            Image = image;
        }
        public EmployeeWithCountOptions(string number, string description, int id, int linkCount, bool active, dynamic position, string email = "", string image = "")
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
            Email = email;
            Image = image;

            if (position!= null)
            {
                PositionTitle= position.Position.PositionTitle;
                PosQualificationDate = position.QualificationDate == null ? "No Qualification Date" : position.QualificationDate.ToString();
                StartDate= position.StartDate.ToString();
                TraineeStatus = position.Trainee;
            }
        }
    }
}
