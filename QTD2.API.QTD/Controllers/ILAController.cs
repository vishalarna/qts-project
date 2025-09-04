using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Test;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ILAController : ControllerBase
    {
        private readonly IILAService _ilaService;
        private readonly IStringLocalizer<ILAController> _localizer;
        private readonly IILAHistoryService _iLAHistoryService;
        private readonly IVersioningService _versioningService;
        private readonly ITestReleaseEmpSettingsService _ilatestReleaseEmpSettingsService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly ITask_HistoryService _task_histService;
        private readonly ITaskService _taskService;
        private readonly IILAResourceService _iLAResourceService;
        private readonly ICertificationService _certificationService;
        private readonly IHasher _hasher;
        private readonly IReportsService _reportsService;
        private readonly IReportGeneratorService _reportGeneratorService;

        public ILAController(IILAService ilaService, IStringLocalizer<ILAController> localizer,
            IILAHistoryService iLAHistoryService, IVersioningService versioningService,
            ITestReleaseEmpSettingsService ilatestReleaseEmpSettingsService, IVersion_TaskService ver_taskService, ITask_HistoryService task_histService, ITaskService taskService,
            IILAResourceService iLAResourceService, ICertificationService certificationService, IHasher hasher, IReportsService reportsService, IReportGeneratorService reportGeneratorService)
        {
            _ilaService = ilaService;
            _localizer = localizer;
            _iLAHistoryService = iLAHistoryService;
            _versioningService = versioningService;
            _ilatestReleaseEmpSettingsService = ilatestReleaseEmpSettingsService;
            _ver_taskService = ver_taskService;
            _task_histService = task_histService;
            _taskService = taskService;
            _iLAResourceService = iLAResourceService;
            _certificationService = certificationService;
            _hasher = hasher;
            _reportsService = reportsService;
            _reportGeneratorService = reportGeneratorService;
        }

        /// <summary>
        /// Gets a list of ILAs
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpGet]
        [Route("/ila")]
        public async Task<IActionResult> GetILAsAsync()
        {
            var result = await _ilaService.GetAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/number")]
        public async Task<IActionResult> GetILANumber()
        {
            var result = await _ilaService.GetILANumberAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/versions/{all}")]
        public async Task<IActionResult> GetAllVersionsForILAAsync(int id,bool all)
        {
            var result = await _ilaService.GetAllVersionsForILAAsync(id, all);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of ILAs
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpGet]
        [Route("/ila/provider/{id}")]
        public async Task<IActionResult> GetILAsByProviderIdAsync(int id, bool activeOnly = false)
        {
            var result = await _ilaService.GetByProviderId(id, activeOnly);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of ILAs
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpGet]
        [Route("/ila/topic/{id}")]
        public async Task<IActionResult> GetILAsByTopicIdAsync(int id)
        {
            var result = await _ilaService.GetByTopicId(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets count of ILAs
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpGet]
        [Route("/ila/stats")]
        public async Task<IActionResult> GetILACountsAsync()
        {
            var result = await _ilaService.GetILAStatCounts();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets count of ILAs
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpGet]
        [Route("/ila/draft")]
        public async Task<IActionResult> GetDraftILAAsync()
        {
            var result = await _ilaService.GetDraftILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/active")]
        public async Task<IActionResult> GetActiveILAAsync()
        {
            var result = await _ilaService.GetActiveILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/active/details")]
        public async Task<IActionResult> GetActiveILADetailsAsync()
        {
            var result = await _ilaService.GetActiveILADetailsAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets count of ILAs
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpGet]
        [Route("/ila/published")]
        public async Task<IActionResult> GetPublishedILAAsync()
        {
            var result = await _ilaService.GetPublishedILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/provider/isNerc")]
        public async Task<IActionResult> CheckIsProviderNerc(int id)
        {
            var result = await _ilaService.CheckIsProviderNercAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Change Provider of ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/provider/{newId}")]
        public async Task<IActionResult> ChangeProviderAsync(int id, int newId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Provider Changed", DateTime.Now, 2);
            await _ilaService.ChangeProviderAsync(id, newId);
            return Ok( new { message = _localizer["ProviderChanged"].Value });
        }
        [HttpPost]
        [Route("/ila/bulkEdit")]
        public async Task<IActionResult> BulkEditIlasAsync(BulkEditOptions options)
        {
            foreach (var id in options.iLaIds)
            {
                var ila = await _ilaService.GetAsync(id);
                ila.EffectiveDate = options.EffectiveDate;
                await _ilaService.UpdateDateAsync(ila);
                int state;
                switch (options.actionType.ToLower())
                {
                    case "inactive":
                    default:
                        await _ilaService.InActiveAsync(id);
                        state = 2;
                        break;
                    case "active":
                        await _ilaService.ActiveAsync(id);
                        state = 2;
                        break;
                    case "delete":
                        await _ilaService.DeleteAsync(id);
                        state = 0;
                        break;
                }
                await _versioningService.VersionILAAsync(ila, options.changeNotes, DateTime.Now, state);

            }


            return Ok( new { message = "Task Completed successfully." });
        }

        /// <summary>
        /// Creates a new ILA
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/ila")]
        public async Task<IActionResult> CreateILAAsync(ILACreateOptions options)
        {
            var result = await _ilaService.CreateAsync(options);
            await _versioningService.VersionILAAsync(result, "New ILA Created", DateTime.Now, 1);
            return Ok( new { message = _localizer["ILACreated"].Value, ilaId = result.Id });
        }

        [HttpPost]
        [Route("/ila/cbt/{cbtId}/employee/{employeeId}")]
        public async Task<IActionResult> EnrollStudentInCbtAsync(int cbtId, int employeeId) //not adding Task and async as we've to just return 200. needs to be added in future.
        {
            //await _ilaService.EnrollStudentAsync(cbtId,employeeId); //currenty this will not return anything. needs to be implementd in future.
            return Ok();
        }

        /// <summary>
        /// Gets a specific ILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with ILA</returns>
        [HttpGet]
        [Route("/ila/{id}")]
        public async Task<IActionResult> GetILAAsync(int id)
        {

            var result = await _ilaService.GetILAByIdAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/copy/{id}")]
        public async Task<IActionResult> CopyILAAsync(int id)
        {
            await _ilaService.CopyAsync(id);
            return Ok( new { message = "ILA Copied Successfully" });
        }

        /// <summary>
        /// Updates  a specific ILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/ila/{id}")]
        public async Task<IActionResult> UpdateILAAsync(int id, ILAUpdateOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            var ilaHasChanged = _ilaService.HasILAChanges(ila, options);
            
            if (ilaHasChanged)
            {
                await _versioningService.VersionILAAsync(ila, "ILA Data Updated", DateTime.Now, 2);
            }
            var result = await _ilaService.UpdateAsync(id, options);

            return Ok( new { result, message = _localizer["ILAUpdated"].Value });
        }

        /// <summary>
        /// Upload The given files data to the ILA
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        [Route("/ila/upload/{id}")]
        public async Task<IActionResult> UploadFile(int id, ILAUploadOptions file)
        {
            await _ilaService.UploadFile(id, file);
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Files Uploaded For ILA", DateTime.Now, 2);
            return Ok( new { message = _localizer["Files Uploaded"].Value });
        }

        /// <summary>
        /// Get All files upload for this ILA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/upload/{id}")]
        public async Task<IActionResult> getUploadedFiles(int id)
        {
            var result = await _ilaService.getUploadedFiles(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/objectives")]
        public async Task<IActionResult> GetAllLinkedObjectives(int id)
        {
            var result = await _ilaService.GetAllLinkedObjectivesAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Download the file.
        /// </summary>
        /// <param name="ilaId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{ilaId}/download/{id}")]
        public async Task<IActionResult> downloadFile(int ilaId, int id)
        {
            var result = await _ilaService.DownloadFile(ilaId, id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/ScheduleClasses/{empid}/idp/{idpId}")]
        public async Task<IActionResult> GEtlinkedSchedules(int ilaId, int empid, int idpId)
        {
            var result = await _ilaService.GetLinkedSchedulingClasses(ilaId, empid, idpId);
            return Ok( new { result });
        }

        /// <summary>
        /// Save the training plan for given ILA
        /// </summary>
        /// <param name="ilaId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/ila/{ilaId}/trainingPlan")]
        public async Task<IActionResult> saveTrainingPlan(int ilaId, ILAUpdateOptions options)
        {
            var ila = await _ilaService.GetAsync(ilaId);
            if(ila.TrainingPlan?.ToLower() != options.TrainingPlan?.ToLower())
            {
                await _versioningService.VersionILAAsync(ila, "Training Plan Saved", DateTime.Now, 2);
                await _ilaService.AddTrainingPlan(ilaId, options);
            }
            return Ok( new { message = _localizer["Training Plan Saved"].Value });
        }

        /// <summary>
        /// Delete the given file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/ila/{id}/upload/{uploadId}")]
        public async Task<IActionResult> deleteUploadedFiles(int id, int uploadId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "An Uploaded Filed for Training Plan Deleted", DateTime.Now, 0);
            await _ilaService.DeleteUploadedFiles(id, uploadId);
            await _ilaService.DeleteUploadedFileFromIwbResources(uploadId);
            return Ok( new { message = _localizer["Files Deleted"].Value });
        }

        /// <summary>
        /// Mark the ILA as Published
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/ila/{id}/publish")]
        public async Task<IActionResult> PublishILA(int id, ILAPublshOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            if(!string.IsNullOrWhiteSpace(options.Reason))
            {
                await _versioningService.VersionILAAsync(ila, options.Reason, options.EffectiveDate, 4);
            }
            await _ilaService.PublishILAAsync(id, options);
            return Ok( new { message = _localizer["ILAPublished"] });
        }

        [HttpPut]
        [Route("/ila/{id}/evalMethod")]
        public async Task<IActionResult> UpdateEvalMethodAndEMP(int id, ILAEvalMethodVM options)
        {
            await _ilaService.UpdateUseForEMPAsync(id, options);
            return Ok( new { message = _localizer["TraineeEvalMethodUpdated"] });
        }
        [HttpPut]
        [Route("/ila/{id}/evalMethodString")]
        public async Task<IActionResult> UpdateEvalMethod(int id, ILAEvalMethodVM options)
        {
            await _ilaService.UpdateEvalMethodAsync(id, options);
            return Ok( new { message = _localizer["TraineeEvalMethodUpdated"] });
        }

        [HttpGet]
        [Route("/ila/{id}/evalMethod")]
        public async Task<IActionResult> GetEvalMethodAndEMP(int id)
        {
            var result = await _ilaService.GetEvalMethodAndEMPAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/tqobjectives")]
        public async Task<IActionResult> GetTQForILA(int id)
        {
            var result = await _ilaService.GetTQForILAAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific ILA by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/ila/{id}")]
        public async Task<IActionResult> DeleteILAAsync(int id, ILAOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            int state;
            if (options.ActionType.Trim().ToLower().Equals("delete"))
            {
                state = 0;
            }
            else
            {
                state = 2;
            }
            await _versioningService.VersionILAAsync(ila, "ILA " + (options.ActionType.ToLower() == "inactive" ? "made Inactive" : options.ActionType.ToLower() == "active" ? "made Active" : "was Deleted"), DateTime.Now, state);
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _ilaService.InActiveAsync(id);
                    break;
                case "active":
                    await _ilaService.ActiveAsync(id);
                    break;
                case "delete":
                    await _ilaService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"ILA-{options.ActionType.ToLower()}"].Value });
        }

        [HttpPost]
        [Route("/ila/traineeEval/filter")]
        public async Task<IActionResult> GetWithTraineeEvalLinks(TestFilterOptions filterOptions)
        {
            var result = await _ilaService.GetWithTraineeEvalLinks(filterOptions);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/{id}/credHours")]
        public async Task<IActionResult> UpdateTotalTrainingHours(int id, ILACreditHourVM hours)
        {
            await _ilaService.UpdateTotalTrainingHoursAsync(id, hours);
            return Ok( new { message = _localizer["ILACreditHoursUpdated"] });
        }
        [HttpGet]
        [Route("/ila/{id}/credHours")]
        public async Task<IActionResult> GetTotalTrainingHours(int id)
        {
            var result = await _ilaService.GetTotalCredHoursAsync(id);
            return Ok( new { result });

        }

        //[HttpPost]
        //[Route("/startCourse/{id}")]
        //public async Task<IActionResult> StartCourse(int id)
        //{
        //    var result = await _ilaService.StartCourseAsync(id);
        //    return StatusCode(StatusCodes.Status200OK);
        //}
        [HttpGet]
        [Route("/ila/notLinked/topic")]
        public async Task<IActionResult> GetILANotLinkedWithTopicAsync()
        {
            var result = await _ilaService.GetILAsNotLinkedToTopic();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/export")]
        public async Task<IActionResult> ExportILAAsync(int ilaId)
        {
            var file = await _ilaService.ExportILAAsCSV(ilaId);
            var ila = await _ilaService.GetAsync(ilaId);

            string fileName = String.Format("{0} - {1}.csv", ila.Number, ila.Name);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            var encodedFileName = Uri.EscapeDataString(fileName);
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"; filename*=UTF-8''{encodedFileName}");
            Response.ContentType = "text/csv;charset=utf-8";

            // Return the CSV data as a file
            return File(file, "text/csv;charset=utf-8", fileName);
        }

        [HttpGet]
        [Route("/ila/{ilaId}/requirements/details")]
        public async Task<IActionResult> GetILARequirementsDetailsByILAId(int ilaId)
        {
            var result = await _ilaService.GetILARequirementsDetailsByILAId(ilaId);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/ila/{ilaId}/certifyingBodies/{certifyingBodyId}/certifications")]
        public async Task<IActionResult> SaveILACertificationByCertifyingBodyAsync(int ilaId, int certifyingBodyId, CertifyingBodyCEHUpdateOptions options)
        {
            await _ilaService.SaveILACertLinksByCertifyingBodyAsync(ilaId, certifyingBodyId, options);
            return Ok();
        }


        [HttpDelete]
        [Route("/ila/{ilaId}/certifyingBodies/{certifyingBodyId}")]
        public async Task<IActionResult> DeleteILACertLinksByILAId(int ilaId, int certifyingBodyId)
        {
            await _ilaService.DeleteILACertLinksAsync(ilaId, certifyingBodyId);
            return Ok();
        }

        [HttpGet]
        [Route("/ila/{ilaId}/trainingplan")]
        public async Task<IActionResult> GetTrainingPlanByILAId(int ilaId)
        {
           var result= await _ilaService.GetTrainingPlanByILAIdAsync(ilaId);
           return Ok(new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/candeactivate")]
        public async Task<IActionResult> CanILABeDeactivateAsync(int ilaId)
        {
            var result = await _ilaService.CanILABeDeactivateAsync(ilaId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/deactivateojtpopulate")]
        public async Task<IActionResult> CanPopulateOJTBeDeactivateAsync(int ilaId)
        {
            var result = await _ilaService.CanPopulateOJTBeDeactivateAsync(ilaId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/nerccertsubreqname")]
        public async Task<IActionResult> GetILANERCCertificationSubRequirementNamesAsync(int ilaId)
        {
            var result = await _ilaService.GetILANERCCertificationSubRequirementNamesForPartialCreditAsync(ilaId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/iscreatedfromiwb")]
        public async Task<IActionResult> IsILACreatedFromInstructorWorkbook(int ilaId)
        {
            var result = await _ilaService.IsILACreatedFromInstructorWorkbook(ilaId);
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/ila/{id}/isPubliclyAvailable")]
        public async Task<IActionResult> UpdateIspubliclyAvailableIla(int id, ILAUpdateOptions options)
        {
            var result = await _ilaService.UpdateIspubliclyAvailableIla(id, options);
            return Ok(new {result });
        }
    }
}
