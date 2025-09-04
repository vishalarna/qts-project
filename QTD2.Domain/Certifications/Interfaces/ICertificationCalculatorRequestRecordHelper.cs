using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Certifications.Interfaces
{
    public interface ICertificationCalculatorRequestRecordHelper
    {
        Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecords(List<int> employeeIds, List<int> certificationIds);
        Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecords(List<int> employeeCertificationIds);
        Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecordsByDate(List<int> employeeIds, List<int> certificationIds, DateTime date, bool includeHistoryRecords = false);
        Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecordsForYear(List<int> employeeIds, List<int> certificationIds, int year, bool includeHistoryRecords = false, bool generateDummyRecords = false);
    }
}
