using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Certifications.Models;

namespace QTD2.Domain.Certifications
{
    public interface ICertificationFulfillmentCalculator
    {
        Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesAsync(List<int> employeeIds, List<int> certificationIds);
        Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWithinEmployeeCertificationDateRangeAsync(List<int> employeeIds, List<int> certificationIds);
        Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesForDateAsync(List<int> employeeIds, List<int> certificationIds, DateTime date);
        Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesAsync(List<int> employeeCertificationIds);
        Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWhenRelatedEmployeeCertificationExistsAsync(List<int> employeeIds, List<int> certificationIds, List<int> relatedCertificationIds);
        Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesForYearAsync(List<int> employeeIds, List<int> certificationIds, int year);

    }
}
