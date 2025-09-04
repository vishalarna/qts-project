using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IReportSkeletonCategoriesService
    {
        public Task<ReportSkeleton_Category> GetActiveReportSkeletonCategoryByIdAsync(int reportSkeletonCategoryId);

        public Task<List<ReportSkeleton_Category>> GetActiveReportSkeletonCategoriesAsync();
    }
}
