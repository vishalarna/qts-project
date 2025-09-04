using DocumentFormat.OpenXml.Office.Word;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Startup;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.Certification_History;
using QTD2.Infrastructure.Model.CertificationSubRequirement;
using QTD2.Infrastructure.Rustici.EngineApi;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Admin
{
    public class ScormController : Controller
    {
        ScormEngineService _scormEngineService;
        IScormHelpersService _scormHelpersService;
        IDbContextBuilder _dbContextBuilder;
        QtdAuthenticationService _authService;

        public ScormController(
            ScormEngineService scormEngineService,
            IScormHelpersService scormHelpersService,
            IDbContextBuilder dbContextBuilder,
            QtdAuthenticationService authService
            )
        {
            _scormEngineService = scormEngineService;
            _scormHelpersService = scormHelpersService;
            _dbContextBuilder = dbContextBuilder;
            _authService = authService;
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("admin/scorm/updatePackages/{instance}")]
        public async Task<IActionResult> UpdateAsync(string instance)
        {
            var instanceSettings = await _authService.Instances.GetInstanceSettingsAsync(instance);
            var context = _dbContextBuilder.BuildQtdContext(instanceSettings.DatabaseName);

            var tenant = instanceSettings.ScormTenant;

            var cbtRegistrations = await context.CBT_ScormRegistration.Include("CBT_ScormRegistration_Responses.CBT_ScormUpload_Question_Choice").ToListAsync();
            var cbtUploads = await context.CBT_ScormUpload.Include("CBT_ScormUpload_Question.CBT_ScormUpload_Question_Choices").ToListAsync();

            for (int i = 0; i < cbtRegistrations.Count(); i++)
            {
                var cbtRegistration = cbtRegistrations[i];
                var cbtScormUpload = cbtUploads.Where(r => r.Id == cbtRegistration.CBTScormUploadId).First();

                string apiRegistrationId = cbtRegistration.CBTScormUploadId + "." + cbtRegistration.ClassScheduleEmployeeId;

                try
                {
                    var registration = (await _scormEngineService.GetRegistrationAsync(apiRegistrationId, tenant));

                    var timeTracked = registration.ActivityDetails?.Children.Select(r => r.timeTracked).ToList();
                    var lastAccessDate = registration?.LastAccessDate;

                    cbtRegistration.UpdateTimeTracked(timeTracked);
                    cbtRegistration.UpdateLastAccessDate(lastAccessDate);

                    var children = registration.ActivityDetails?.Children ?? new System.Collections.Generic.List<ActivityResultSchema>();
                    foreach (var child in children)
                    {
                        var runtimeInteractions = child.Runtime?.RuntimeInteractions ?? new System.Collections.Generic.List<RuntimeInteractionSchema>();

                        cbtRegistration = _scormHelpersService.ProcessRuntimeInteraction(runtimeInteractions, cbtRegistration, cbtScormUpload);

                    }
                }
                catch (System.Exception e)
                {
                    continue;
                }
            }

            context.UpdateRange(cbtUploads);
            context.UpdateRange(cbtRegistrations);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return Ok();
        }
    }
}
