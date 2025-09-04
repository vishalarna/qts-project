using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IReportSkeletonService
    {
        public Task<List<ReportSkeleton>> GetReportSkeletonsAsync();
        public Task<ReportSkeleton> GetReportSkeletonAsync(int reportSkeletonId);
        public Task<ReportSkeleton> GetReportSkeletonByNameAsync(string name);
    }
}
