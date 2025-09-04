using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Rustici.EngineApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using QTD2.Domain.Entities.Core;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QTD2.Domain.ClassScheduleEmployee.GradeEvaluation;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("ScormAPIPolicy")]
    [ApiController]
    public class ScormHooksController : ControllerBase
    {
        IDbContextBuilder _dbContextBuilder;
        QtdAuthenticationService _authService;
        IServiceProvider _serviceProvider;
        IScormHelpersService _scormHelpers;
        private ILogger<ScormHooksController> _logger;
        private readonly IGradeEvaluator _gradeEvaluator;
        private readonly IDatabaseResolver _databaseResolver;

        public ScormHooksController(
            IDbContextBuilder dbContextBuilder,
            QtdAuthenticationService authService,
            IScormHelpersService scormHelpers,
            ILogger<ScormHooksController> logger,
            IServiceProvider serviceProvider,
            IGradeEvaluator gradeEvaluator,
            IDatabaseResolver databaseResolver)
        {
            _dbContextBuilder = dbContextBuilder;
            _authService = authService;
            _serviceProvider = serviceProvider;
            _scormHelpers = scormHelpers;
            _logger = logger;
            _gradeEvaluator = gradeEvaluator;
            _databaseResolver = databaseResolver;
        }

        [HttpPost]
        [Route("/scorm/registration")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrationChangedAsync([FromBody] RegistrationChangedSchema request)
        {
            var instance = await _authService.Instances.GetInstanceSettingsByScormTenant(request.TenantName);
            var context = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

            var registrationIds = request.Resources.Registration.Id.Split(".");
            var cbtScormUploadId = Convert.ToInt32(registrationIds[0]);
            var classScheduleEmployeeId = Convert.ToInt32(registrationIds[1]);
            var cbtScormUpload = await context.CBT_ScormUpload.FirstAsync(r => r.Id == cbtScormUploadId);


            var registration = context.CBT_ScormRegistration
                                        .Include("CBT_ScormRegistration_Responses.CBT_ScormUpload_Question_Choice.CBT_ScormUpload_Question")
                                        .Include("ScormUpload.CBT_ScormUpload_Question.CBT_ScormUpload_Question_Choices")
                                        .Where(r => r.CBTScormUploadId == cbtScormUploadId && r.ClassScheduleEmployeeId == classScheduleEmployeeId)
                                        .First();

            var classScheduleEmployee = context.ClassScheduleEmployees.Include(cse => cse.ClassSchedule).ThenInclude(cs => cs.ClassSchedule_TestReleaseEMPSettings).Where(r => r.Id == classScheduleEmployeeId).First();
            var settings = context.ClientSettings_GeneralSettings.Where(r => !r.Deleted).First();

            var dbPassingScore = settings.CompanySpecificCoursePassingScore > 1 ? settings.CompanySpecificCoursePassingScore : settings.CompanySpecificCoursePassingScore * 100;
            var passingScore = request.Body.ActivityDetails.StaticProperties.ScaledPassingScoreUsed ? (int)(request.Body.ActivityDetails.StaticProperties.ScaledPassingScore * 100) : dbPassingScore;
            var passingScoreSource = request.Body.ActivityDetails.StaticProperties.ScaledPassingScoreUsed ? CBT_PassingScoreSource.ScormPackage : CBT_PassingScoreSource.CompanySpecificPassingScore;
            var score = request.Body.Score == null ? 0.00 : request.Body.Score.Scaled;
            var registrationCompletion = (CBT_ScormRegistrationCompletion)Enum.Parse(typeof(CBT_ScormRegistrationCompletion), request.Body.RegistrationCompletion.ToString());
            var registrationSuccess = (CBT_ScormRegistrationSuccess)Enum.Parse(typeof(CBT_ScormRegistrationSuccess), request.Body.RegistrationSuccess.ToString());

            var timeTracked = TimeSpan.FromSeconds(request.Body.TotalSecondsTracked).ToString();
            var lastAccessDate = request.Body.LastAccessDate;

            registration.UpdateTimeTracked(timeTracked);
            registration.UpdateLastAccessDate(lastAccessDate);
            registration.ReportResults(passingScore, passingScoreSource, score, registrationCompletion, registrationSuccess, "SCORM", "");

            try
            {
                var runtimeInteractions = request.Body?.ActivityDetails?.Children?.SelectMany(r => r.Runtime?.RuntimeInteractions).ToList() ?? new List<RuntimeInteractionSchema>();
            
                registration = _scormHelpers.ProcessRuntimeInteraction(runtimeInteractions, registration, cbtScormUpload);
            }
            catch (Exception e)
            {
                _logger.LogError($"ScormHooksController.RegistrationChangedAsync getting/processing RuntimeInteractions for {instance.DatabaseName} failed {e}", e);
            }

            classScheduleEmployee.CBTStatusId = request.Body.RegistrationCompletion == RegistrationCompletion.COMPLETED ? 3 : request.Body.RegistrationCompletion == RegistrationCompletion.INCOMPLETE ? 2 : 1;

            context.Update(cbtScormUpload);
            context.Update(registration);
            context.Update(classScheduleEmployee);
           
            context.SaveChanges();
            
            try
            {
                var classSchedule = classScheduleEmployee.ClassSchedule;
                var ila = await context.ILAs.FirstAsync(i => i.Id == classSchedule.ILAID.Value);
                var tests = await context.ClassSchedule_Roster.Where(r => r.EmpId == classScheduleEmployee.EmployeeId && r.ClassScheduleId == classScheduleEmployee.ClassScheduleId).ToListAsync();
                var testSettings = classSchedule.ClassSchedule_TestReleaseEMPSettings;
                var cbtRegistration = registration;

                var evaluationResult = _gradeEvaluator.EvaluateClassScheduleEmployee(classScheduleEmployee, classSchedule, ila, tests, cbtRegistration, testSettings);

                if (evaluationResult.CompletionDate.HasValue && !classScheduleEmployee.CompletionDate.HasValue)
                {
                    classScheduleEmployee.CompleteClass(evaluationResult.CompletionDate.Value, evaluationResult.Grade,(int?)evaluationResult.Score);
                    context.Update(classScheduleEmployee);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,$"ScormHooksController.RegistrationChangedAsync failed during evaluation for ClassScheduleEmployeeId {classScheduleEmployee.Id} in database {instance.DatabaseName}");
            }

            return Ok();
        }
    }
}