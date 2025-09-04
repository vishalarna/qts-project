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

namespace QTD2.Application.Services.Shared
{
    public class ReportSkeletonService : IReportSkeletonService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Domain.Interfaces.Service.Core.IReportSkeletonService _reportSkeletonService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ReportSkeletonService> _localizer;

        public ReportSkeletonService(
                   IHttpContextAccessor httpContextAccessor,
                   Domain.Interfaces.Service.Core.IReportSkeletonService reportSkeletonService,
                   IAuthorizationService authorizationService,
                   IStringLocalizer<ReportSkeletonService> localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _reportSkeletonService = reportSkeletonService;
            _authorizationService = authorizationService;
            _localizer = localizer;
        }

        public async Task<List<ReportSkeleton>> GetReportSkeletonsAsync()
        {
            var reportSkeletons = await _reportSkeletonService.GetAllActiveAsync();
            reportSkeletons = reportSkeletons.Where(skeleton => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, skeleton, ReportSkeletonOperations.Read).Result.Succeeded).ToList();
            return reportSkeletons.ToList();
        }

        public async Task<ReportSkeleton> GetReportSkeletonAsync(int reportSkeletonId)
        {
            var reportSkeleton = await _reportSkeletonService.GetWithIncludeAsync(reportSkeletonId, new[] { "DisplayColumns", "AvailableFilters" });
            if (reportSkeleton != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, reportSkeleton, EmployeeOperations.Read);
                if (result.Succeeded)
                {
                    return reportSkeleton;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return reportSkeleton;
        }

        public async Task<ReportSkeleton> GetReportSkeletonByNameAsync(string name)
        {
            var reportSkeleton = (await _reportSkeletonService.FindAsync(x=>x.DefaultTitle==name)).FirstOrDefault();
            if (reportSkeleton == null)
            {
                throw new BadHttpRequestException(message: _localizer["ReportSkeletonNotFound"]);
            }
            else
            {
                var reportSkeletonData = await GetReportSkeletonAsync(reportSkeleton.Id);
                return reportSkeletonData;
            }
        }
     }
}
