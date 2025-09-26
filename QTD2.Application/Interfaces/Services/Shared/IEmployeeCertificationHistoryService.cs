using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EmployeeCertification;
using QTD2.Infrastructure.Model.EmployeeCertificationHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEmployeeCertificationHistoryService
    {
        public Task<EmployeeCertifictaionHistory> CreateAsync(EmployeeCertificationHistoryCreateOptions options);

        public System.Threading.Tasks.Task UpdateAsync(int id, EmployeeCertificateUpdateOptions options);
        public System.Threading.Tasks.Task DeleteHistAsync(int certLinkId);
        public Task<List<EmployeeCertifictaionHistory>> GetCertificationWithEmpCertificationHistory(int empCertId);
        public System.Threading.Tasks.Task DeleteBulkHistoryAsync(EmployeeCertificationHistoryDeleteOptions options);
    }
}
