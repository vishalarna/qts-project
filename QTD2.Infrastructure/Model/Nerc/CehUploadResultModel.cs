using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace QTD2.Infrastructure.Model.Nerc
{
    public class CehUploadResultModel
    {
        public int ClassScheduleId { get; set; }
        public string CourseNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string CertificationNumber { get; set; }
        public string OperatingTopicsCEH { get; set; }
        public string Standards { get; set; }
        public string Simulations { get; set; }
        public bool IsValid { get; set; }

        public CehUploadResultModel(string courseId, DateTime enrollmentDate, string certificationNumber, string operatingHours, string standards, string simulations, int classScheduleId, bool isValid)
        {
            CourseNumber = courseId;
            EnrollmentDate = enrollmentDate;
            CertificationNumber = certificationNumber;
            OperatingTopicsCEH = operatingHours;
            Standards = standards;
            Simulations = simulations;
            ClassScheduleId = classScheduleId;
            IsValid = isValid;
        }

        public CehUploadResultModel() { }

        public CehUploadResultModel(int classScheduleId, bool isValid) {
            ClassScheduleId = classScheduleId;
            IsValid = isValid;
        }
    }
}
