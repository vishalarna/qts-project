using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EmpDashboardStatistics
    {
        public int TotalTrainee { get; set; }
        public int TotalEmployeesWithNoCertificate { get; set; }
        public int TotalEmployeesWithExpiredCertificate { get; set; }

        public int TotalEmployeeRecordsAvailable { get; set; }
        public EmpDashboardStatistics(int totalTrainee, int totalEmployeesWithNoCertificate, int totalEmployeesWithExpiredCertificate,int totalEmployeeRecordsAvailable)
        {
            TotalTrainee = totalTrainee;
            TotalEmployeesWithNoCertificate = totalEmployeesWithNoCertificate;
            TotalEmployeesWithExpiredCertificate = totalEmployeesWithExpiredCertificate;
            TotalEmployeeRecordsAvailable = totalEmployeeRecordsAvailable;
        }

    }
}
