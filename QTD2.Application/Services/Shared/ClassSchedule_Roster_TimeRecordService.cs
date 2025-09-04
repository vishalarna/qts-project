using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Roster_TimeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IClassSchedule_Roster_TimeRecordDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_TimeRecordService;

namespace QTD2.Application.Services.Shared
{
    public class ClassSchedule_Roster_TimeRecordService : IClassSchedule_Roster_TimeRecordService
    {
        private readonly IClassSchedule_Roster_TimeRecordDomainService _classSchedule_Roster_TimeRecordDomainService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<ClassSchedule_Roster_TimeRecordService> _localizer;
        public ClassSchedule_Roster_TimeRecordService(IClassSchedule_Roster_TimeRecordDomainService classSchedule_Roster_TimeRecordDomainService, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IStringLocalizer<ClassSchedule_Roster_TimeRecordService> localizer) {
            _classSchedule_Roster_TimeRecordDomainService = classSchedule_Roster_TimeRecordDomainService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
        }

        public async Task<ClassSchedule_RosterTimeRecord_VM> CreateTimeRecordAsync(ClassSchedule_RosterTimeRecord_VM options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var lastTimeRecord = (await _classSchedule_Roster_TimeRecordDomainService.GetClassScheduleRosterTimeRecordByRosterId(options.ClassSchedule_RosterId)).LastOrDefault();

            var newSequence = lastTimeRecord == null ? 1 : lastTimeRecord.Sequence + 1;

            var newClassScheduleTimeRecord = new ClassSchedule_Roster_TimeRecord(options.ClassSchedule_RosterId,options.StartDateTime,options.EndDateTime,newSequence);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,newClassScheduleTimeRecord,AuthorizationOperations.Create
            );

            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException(_localizer["OperationNotAllowed"].Value);
            }

            var validationResult = await _classSchedule_Roster_TimeRecordDomainService.AddAsync(newClassScheduleTimeRecord);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', validationResult.Errors));
            }

            var resultVM = new ClassSchedule_RosterTimeRecord_VM(newClassScheduleTimeRecord.ClassSchedule_RosterId,newClassScheduleTimeRecord.StartDateTime,newClassScheduleTimeRecord.EndDateTime);

            return resultVM;
        }

    }
}
