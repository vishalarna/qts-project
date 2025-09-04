using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDomainReportSkeletonCategoriesService = QTD2.Domain.Interfaces.Service.Core.IReportSkeletonCategoriesService;

namespace QTD2.Application.Services.Shared
{
    public class ReportSkeletonCategoriesService : IReportSkeletonCategoriesService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ReportSkeletonCategoriesService> _localizer;
        private readonly IDomainReportSkeletonCategoriesService _reportSkeletonCategoriesService;

        public ReportSkeletonCategoriesService(
                   IHttpContextAccessor httpContextAccessor,
                   IAuthorizationService authorizationService,
                   IStringLocalizer<ReportSkeletonCategoriesService> localizer,
                   IDomainReportSkeletonCategoriesService reportSkeletonCategoriesService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _reportSkeletonCategoriesService = reportSkeletonCategoriesService;
        }

        public async Task<List<ReportSkeleton_Category>> GetActiveReportSkeletonCategoriesAsync()
        {
            var reportSkeletonCategories = await _reportSkeletonCategoriesService.GetAllActiveAsync();
            reportSkeletonCategories = reportSkeletonCategories.Where(skeleton => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, skeleton, ReportSkeletonCategoryOperations.Read).Result.Succeeded).ToList();
            return reportSkeletonCategories.ToList();
        }

        public async Task<ReportSkeleton_Category> GetActiveReportSkeletonCategoryByIdAsync(int reportSkeletonCategoryId)
        {
            var reportSkeletonCategory = await _reportSkeletonCategoriesService.GetActiveAsync(reportSkeletonCategoryId);
            if (reportSkeletonCategory != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, reportSkeletonCategory, ReportSkeletonCategoryOperations.Read);
                if (result.Succeeded)
                {
                    return reportSkeletonCategory;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            return reportSkeletonCategory;
        }
     }
}
