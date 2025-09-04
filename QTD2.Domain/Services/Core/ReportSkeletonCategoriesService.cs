using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ReportSkeletonCategoriesService : Common.Service<ReportSkeleton_Category>, IReportSkeletonCategoriesService
    {
        public ReportSkeletonCategoriesService(IReportSkeletonCategoryRepository repository, IReportSkeletonCategoryValidation validation)
   : base(repository, validation)
        {
        }

        public async Task<List<ReportSkeleton_Category>> GetAllActiveAsync()
        {
            return (await FindAsync(r => r.Active)).ToList();
        }

        public async Task<ReportSkeleton_Category> GetActiveAsync(int reportSkeletonCategoryId)
        {
            List<Expression<Func<ReportSkeleton_Category, bool>>> predicates = new List<Expression<Func<ReportSkeleton_Category, bool>>>();
            if (reportSkeletonCategoryId > 0)
            {
                predicates.Add(r => r.Active && r.Id == reportSkeletonCategoryId);
            }
            var reportSkeletonCategory = (await FindWithIncludeAsync(predicates, new[] { "ReportSkeleton_Subcategories.ReportSkeleton_Subcategory_Reports.ReportSkeleton" })).FirstOrDefault();
            reportSkeletonCategory.ReportSkeleton_Subcategories = reportSkeletonCategory.ReportSkeleton_Subcategories.Where(x => x.Active).ToList();
            foreach (var subcategory in reportSkeletonCategory.ReportSkeleton_Subcategories)
            {
                subcategory.ReportSkeleton_Subcategory_Reports = subcategory.ReportSkeleton_Subcategory_Reports.Where(x => x.Active && x.ReportSkeleton.Active).ToList();
            }
            return reportSkeletonCategory;
        }
    }
}
