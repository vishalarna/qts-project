using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IReportSkeletonCategoriesService : Common.IService<ReportSkeleton_Category>
    {
        System.Threading.Tasks.Task<List<ReportSkeleton_Category>> GetAllActiveAsync();
        System.Threading.Tasks.Task<ReportSkeleton_Category> GetActiveAsync(int reportSkeletonCategoryId);
    }
}
