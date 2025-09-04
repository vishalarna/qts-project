using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Version_EnablingObjective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVersion_EnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjectiveService;
using IVersion_EO_Position_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjective_Position_LinkService;
using IVersion_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_PositionService;
using IPosition_SQLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_SQService;
using IEO_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_Employee_LinkService;
using IVersion_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EmployeeService;
using IVersion_EO_EmpLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjective_Employee_LinkService;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IVersion_TaskDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TaskService;
using IVersion_RRDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_RegulatoryRequirementService;
using IVersion_SHDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_SaftyHazardService;
using IVersion_ProcedureDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ProcedureService;
using IVersion_ILADomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_ILAService;
using IVersion_MetaEOLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjective_MetaEOLinkService;
using IVersion_TestItemLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TestItemsService;
using IVersion_EnablingObjective_StepDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_EnablingObjective_StepService;
using IEO_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveHistoryService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IEnablingObjective_TopicDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_TopicService;
using IEnablingObjective_SubCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryService;
using IEnablingObjective_CategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_CategoryService;
using DocumentFormat.OpenXml.Spreadsheet;

namespace QTD2.Application.Services.Shared
{
    public class Version_EnablingObjectiveService : IVersion_EnablingObjectiveService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_EnablingObjectiveService> _localizer;
        private readonly IVersion_EnablingObjectiveDomainService _versionEOService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEnablingObjectiveDomainService _eoService;
        private readonly IVersion_EO_Position_LinkDomainService _ver_eo_posLinkService;
        private readonly IVersion_PositionDomainService _ver_posService;
        private readonly IPosition_SQLinkDomainService _eo_posLinkService;
        private readonly IVersion_EO_EmpLinkDomainService _ver_eo_empLinkService;
        private readonly IVersion_EmployeeDomainService _ver_empService;
        private readonly IEO_EmployeeDomainService _eo_emp_linkService;
        private readonly Version_EnablingObjective _ver_eo;
        private readonly EnablingObjective _eo;
        private readonly IVersion_TaskDomainService _ver_taskService;
        private readonly IVersion_RRDomainService _ver_RRService;
        private readonly IVersion_SHDomainService _ver_shService;
        private readonly IVersion_ProcedureDomainService _ver_procService;
        private readonly IVersion_ILADomainService _ver_ilaService;
        private readonly IVersion_MetaEOLinkDomainService _ver_metaeo_linkService;
        private readonly IVersion_TestItemLinkDomainService _ver_testItemService;
        private readonly IVersion_EnablingObjective_StepDomainService _ver_eoStepService;
        private readonly IEO_HistoryDomainService _eo_histService;
        private readonly IPersonDomainService _person_Serivce;
        private readonly IEnablingObjective_TopicDomainService _eo_topicService;
        private readonly IEnablingObjective_SubCategoryDomainService _eo_subCategoryService;
        private readonly IEnablingObjective_CategoryDomainService _eo_categoryService;


        public Version_EnablingObjectiveService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<Version_EnablingObjectiveService> localizer,
            IVersion_EnablingObjectiveDomainService versionEOService,
            UserManager<AppUser> userManager,
            IEnablingObjectiveDomainService eoService,
            IVersion_EO_Position_LinkDomainService ver_eo_posLinkService,
            IVersion_PositionDomainService ver_posService,
            IPosition_SQLinkDomainService eo_posLinkService,
            IVersion_EO_EmpLinkDomainService ver_eo_empLinkService,
            IVersion_EmployeeDomainService ver_empService,
            IEO_EmployeeDomainService eo_emp_linkService,
            IVersion_TaskDomainService ver_taskService,
            IVersion_RRDomainService ver_RRService,
            IVersion_SHDomainService ver_shService,
            IVersion_ProcedureDomainService ver_procService,
            IVersion_ILADomainService ver_ilaService,
            IVersion_MetaEOLinkDomainService ver_metaeo_linkService,
            IVersion_TestItemLinkDomainService ver_testItemService,
            IVersion_EnablingObjective_StepDomainService ver_eoStepService,
            IEO_HistoryDomainService eo_histService,
            IPersonDomainService person_Serivce,
            IEnablingObjective_TopicDomainService eo_topicService,
            IEnablingObjective_SubCategoryDomainService eo_subCategoryService,
            IEnablingObjective_CategoryDomainService eo_categoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _versionEOService = versionEOService;
            _userManager = userManager;
            _eoService = eoService;
            _ver_eo_posLinkService = ver_eo_posLinkService;
            _ver_posService = ver_posService;
            _eo_posLinkService = eo_posLinkService;
            _ver_eo_empLinkService = ver_eo_empLinkService;
            _ver_empService = ver_empService;
            _eo_emp_linkService = eo_emp_linkService;
            _ver_eo = new Version_EnablingObjective();
            _eo = new EnablingObjective();
            _ver_taskService = ver_taskService;
            _ver_RRService = ver_RRService;
            _ver_shService = ver_shService;
            _ver_procService = ver_procService;
            _ver_ilaService = ver_ilaService;
            _ver_metaeo_linkService = ver_metaeo_linkService;
            _ver_testItemService = ver_testItemService;
            _ver_eoStepService = ver_eoStepService;
            _eo_histService = eo_histService;
            _person_Serivce = person_Serivce;
            _eo_topicService = eo_topicService;
            _eo_subCategoryService = eo_subCategoryService;
            _eo_categoryService = eo_categoryService;
        }

        public async Task<Version_EnablingObjective> CreateAsync(EnablingObjective options, int state)
        {
            var obj = (await _versionEOService.FindAsync(x => x.EnablingObjectiveId == options.Id)).OrderBy(o => o.Id).LastOrDefault();
            var eo = await _eoService.FindQuery(x => x.Id == options.Id).FirstOrDefaultAsync();
            string versionNumber = "";
            if (obj == null)
            {
                if (eo != null)
                {
                    versionNumber = "1.0";
                    obj = new Version_EnablingObjective(eo, versionNumber, state);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["EONotFound"].Value);
                }
            }
            else
            {
                if (eo != null)
                {
                    var oldInUse = await _versionEOService.FindQuery(x => x.IsInUse && x.EnablingObjectiveId == eo.Id).FirstOrDefaultAsync();
                    if (oldInUse == null)
                    {
                        var totalVersions = await _versionEOService.FindQuery(x => x.EnablingObjectiveId == eo.Id).OrderBy(o => o.VersionNumber).ToListAsync();
                        var greaterVersion = new Version_EnablingObjective();
                        foreach (var totalVersion in totalVersions)
                        {
                            if (greaterVersion == null || greaterVersion?.Id == null || greaterVersion?.Id == 0)
                            {
                                greaterVersion = totalVersion;
                            }
                            else
                            {
                                if (greaterVersion.VersionNumber == null || totalVersion.VersionNumber == null)
                                {
                                    if (greaterVersion.Id < totalVersion.Id)
                                    {
                                        greaterVersion = totalVersion;
                                    }
                                }
                                else
                                {
                                    if (int.Parse(greaterVersion.VersionNumber.ToString().Split(".")[0]) < int.Parse(totalVersion.VersionNumber.ToString().Split(".")[0]))
                                    {
                                        greaterVersion = totalVersion;
                                    }
                                }
                            }
                        }
                        if (greaterVersion != null && greaterVersion.Id != 0)
                        {
                            greaterVersion.IsInUse = true;
                            await _versionEOService.UpdateAsync(greaterVersion);
                            oldInUse = greaterVersion;
                        }
                    }
                    oldInUse.IsInUse = false;
                    await _versionEOService.UpdateAsync(oldInUse);
                    var verNumber = (double)(await _versionEOService.FindQuery(x => x.EnablingObjectiveId == eo.Id).CountAsync());
                    if (obj.VersionNumber != null)
                    {
                        verNumber = Convert.ToDouble(obj.VersionNumber);
                    }
                    verNumber += 1;
                    versionNumber = verNumber.ToString() + ".0";
                    obj = new Version_EnablingObjective(eo, versionNumber,state);
                    obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    obj.ModifiedDate = DateTime.Now;
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["EONotFound"].Value);
                }
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                obj.IsInUse = true;
                if(eo.SubCategoryId == 0 || eo.CategoryId == 0)
                {
                    if(eo.TopicId == 0)
                    {
                        throw new BadHttpRequestException(message: _localizer["CorruptedEOData"]);
                    }
                    else
                    {
                        var topic = await _eo_topicService.FindQuery(x => x.Id == eo.TopicId).Select(s => new EnablingObjective_Topic { Id = s.Id, SubCategoryId = s.SubCategoryId}).FirstOrDefaultAsync();
                        if(topic == null)
                        {
                            throw new BadHttpRequestException(message: _localizer["TopicNotFoundException"]);
                        }
                        else
                        {
                            var subCategory = await _eo_subCategoryService.FindQuery(x => x.Id == topic.SubCategoryId).Select(s => new EnablingObjective_SubCategory { Id = s.Id, CategoryId = s.CategoryId }).FirstOrDefaultAsync();
                            if(subCategory == null)
                            {
                                throw new BadHttpRequestException(message: _localizer["SubCategoryNotFound"]);
                            }
                            else
                            {
                                var category = await _eo_categoryService.FindQuery(x => x.Id == subCategory.CategoryId).Select(s => new EnablingObjective_Category { Id = s.Id }).FirstOrDefaultAsync();
                                if (category == null)
                                {
                                    throw new BadHttpRequestException(message: _localizer["CategoryNotFound"]);
                                }
                                else
                                {
                                    eo.SubCategoryId = subCategory.Id;
                                    eo.CategoryId = category.Id;
                                    await _eoService.UpdateAsync(eo);
                                }
                            }
                        }
                    }
                }
                obj.CategoryId = eo.CategoryId;
                obj.SubCategoryId = eo.SubCategoryId;
                var validationResult = await _versionEOService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        //public async System.Threading.Tasks.Task DeleteAsync(int id)
        //{
        //    var obj = await GetAsync(id);
        //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Delete);

        //    if (result.Succeeded)
        //    {
        //        obj.Delete();

        //        var validationResult = await _versionEOService.UpdateAsync(obj);
        //        if (!validationResult.IsValid)
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
        //        }
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
        //    }
        //}

        //public async Task<List<Version_EnablingObjective>> GetAsync()
        //{
        //    var obj_list = await _versionEOService.AllAsync();
        //    obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read).Result.Succeeded);
        //    return obj_list?.ToList();
        //}

        //public async Task<Version_EnablingObjective> GetAsync(int id)
        //{
        //    var obj = await _versionEOService.GetAsync(id);
        //    if (obj != null)
        //    {
        //        var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read);
        //        if (result.Succeeded)
        //        {
        //            return obj;
        //        }
        //        else
        //        {
        //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception(message: _localizer["RecordNotFound"].Value);
        //    }
        //}

        //public async Task<Version_EnablingObjective> UpdateAsync(int id, Version_EnablingObjectiveUpdateOptions options)
        //{
        //    var obj = await GetAsync(id);
        //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Update);

        //    if (result.Succeeded)
        //    {
        //        obj.EnablingObjectiveId = options.EnablingObjectiveId;
        //        obj.VersionNumber = options.VersionNumber;
        //        obj.CategoryId = options.CategoryId;
        //        obj.SubCategoryId = options.SubCategoryId;
        //        obj.TopicId = options.TopicId;
        //        obj.isMetaEO = options.isMetaEO;
        //        obj.Description = options.Description;
        //        obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
        //        obj.ModifiedDate = DateTime.Now;
        //        var validationresult = await _versionEOService.UpdateAsync(obj);
        //        if (!validationresult.IsValid)
        //        {
        //            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationresult.Errors));
        //        }
        //        else
        //        {
        //            return obj;
        //        }
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
        //    }
        //}

        public async System.Threading.Tasks.Task CreateEOPositionLinkVersion(int eoId, int posId)
        {
            var eo = await _versionEOService.FindQuery(x => x.EnablingObjectiveId == eoId).FirstOrDefaultAsync();
            var pos = await _ver_posService.FindQuery(x => x.PositionId == posId).FirstOrDefaultAsync();
            if (eo == null || pos == null)
            {
                throw new BadHttpRequestException(message: _localizer["DataNotFound"]);
            }
            else
            {
                var link = await _ver_eo_posLinkService.FindQuery(x => x.Version_EnablingObjectiveId == eo.Id && x.Version_PositionId == pos.Id).FirstOrDefaultAsync();
                if (link == null)
                {
                    var myLink = await _eo_posLinkService.FindQuery(x => x.EOId == eoId && x.PositionId == posId).FirstOrDefaultAsync();
                    var verPosLink = new Version_EnablingObjective_Position_Link(eo, pos);
                    await _ver_eo_posLinkService.AddAsync(verPosLink);
                }
                else
                {
                    var num = Double.Parse(link.Version_Number);
                    link.Version_Number = (num + 1).ToString();
                    await _ver_eo_posLinkService.UpdateAsync(link);
                }
            }
        }

        public async System.Threading.Tasks.Task CreateEMPEmployeeLinkVersion(int eoId, int empId)
        {
            var eo = await _versionEOService.FindQuery(x => x.EnablingObjectiveId == eoId).FirstOrDefaultAsync();
            var emp = await _ver_empService.FindQuery(x => x.EmployeeId == empId).FirstOrDefaultAsync();
            if (eo == null || emp == null)
            {
                throw new BadHttpRequestException(message: _localizer["DataNotFound"]);
            }
            else
            {
                var link = await _ver_eo_empLinkService.FindQuery(x => x.Version_EnablingObjectiveId == eo.Id && x.Version_EmployeeId == emp.Id).FirstOrDefaultAsync();
                if (link == null)
                {
                    var myLink = await _eo_emp_linkService.FindQuery(x => x.EOID == eoId && x.EmployeeId == empId).FirstOrDefaultAsync();
                    var verEmpLink = new Version_EnablingObjective_Employee_Link(eo,emp,myLink.StartDate);
                    await _ver_eo_empLinkService.AddAsync(verEmpLink);
                }
                else
                {
                    var num = Double.Parse(link.Version_Number);
                    link.Version_Number = (num + 1).ToString();
                    await _ver_eo_empLinkService.UpdateAsync(link);
                }
            }
        }

        public async System.Threading.Tasks.Task CreateLinkVersioning(EnablingObjective eo, int state)
        {
            var copy = await _eoService.FindQueryWithIncludeAsync(x => x.Id == eo.Id,
                new string[] {
                            nameof(_eo.TestItems),
                            nameof(_eo.Task_EnablingObjective_Links),
                            nameof(_eo.RegRequirement_EO_Links),
                            nameof(_eo.SafetyHazard_EO_Links),
                            nameof(_eo.Procedure_EnablingObjective_Links),
                            nameof(_eo.ILA_EnablingObjective_Links),
                            nameof(_eo.EnablingObjective_MetaEO_Links),
                            nameof(_eo.EnablingObjective_Employee_Links),
                            nameof(_eo.Position_SQs),
                            nameof(_eo.EnablingObjective_Steps),
                            nameof(_eo.EnablingObjective_Questions)
                        }).FirstOrDefaultAsync();

            if (copy == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOToVersionNotFound"]);
            }
            else
            {

                var version = await CreateAsync(eo, state);
                // Copy Task Links
                foreach(var copyeo in copy.Task_EnablingObjective_Links)
                {
                    var verTask = await _ver_taskService.FindQuery(x => x.TaskId == copyeo.TaskId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verTask != null)
                    {
                        version.Version_EnablingObjective_Tasks.Add(new Version_EnablingObjective_Task(version, verTask));
                    }
                }

                // Copy RR Links
                foreach(var copyrr in copy.RegRequirement_EO_Links)
                {
                    var verRR = await _ver_RRService.FindQuery(x => x.RegulatoryRequirementId == copyrr.RegulatoryRequirementId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verRR != null)
                    {
                        version.Version_EnablingObjective_RRLinks.Add(new Version_EnablingObjective_RRLink(version, verRR));
                    }
                    
                }

                // Copy SH Links
                foreach(var copysh in copy.SafetyHazard_EO_Links)
                {
                    var versh = await _ver_shService.FindQuery(x => x.SaftyHazardId == copysh.SafetyHazardId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(versh != null)
                    {
                        version.Version_EnablingObjective_SaftyHazard_Links.Add(new Version_EnablingObjective_SaftyHazard_Link(version.Id, versh.SaftyHazardId, "1.0"));
                    }
                }

                // Copy Proc Links
                foreach (var copyproc in copy.Procedure_EnablingObjective_Links)
                {
                    var verProc = await _ver_procService.FindQuery(x => x.ProcedureId == copyproc.ProcedureId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verProc != null)
                    {
                        version.Version_Procedure_EnablingObjective_Links.Add(new Version_Procedure_EnablingObjective_Link(version.Id, verProc.Id, "1.0"));
                    }
                }

                // Copy ILA Links
                foreach (var copyila in copy.ILA_EnablingObjective_Links)
                {
                    var verila = await _ver_ilaService.FindQuery(x => x.ILAId == copyila.ILAId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verila != null)
                    {
                        version.Version_EnablingObjective_ILALinks.Add(new Version_EnablingObjective_ILALink(version, verila));
                    }
                    
                }

                // Copy Meta Links
                foreach(var copymeta in copy.EnablingObjective_MetaEO_Links)
                {
                    var vermeta = await _versionEOService.FindQuery(x => x.EnablingObjectiveId == copymeta.EOID && x.EnablingObjectiveId != eo.Id).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(vermeta != null)
                    {
                        version.Version_EnablingObjective_MetaEOLinks.Add(new Version_EnablingObjective_MetaEOLink(version, vermeta, "1.0"));
                    }
                }

                // Copy Emp Links
                foreach(var copyemp in copy.EnablingObjective_Employee_Links)
                {
                    var veremp = await _ver_empService.FindQuery(x => x.EmployeeId == copyemp.EmployeeId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(veremp != null)
                    {
                        version.Version_EnablingObjective_Employee_Links.Add(new Version_EnablingObjective_Employee_Link(version, veremp, copyemp.StartDate));
                    }
                }

                // Copy Position Links
                foreach(var copyPos in copy.Position_SQs)
                {
                    var verpos = await _ver_posService.FindQuery(x => x.PositionId == copyPos.PositionId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verpos != null)
                    {
                        version.Version_EnablingObjective_Position_Links.Add(new Version_EnablingObjective_Position_Link(version, verpos));
                    }
                }

                // Copy Test Item Links
                foreach(var copyTest in copy.TestItems)
                {
                    version.Version_TestItems.Add(new Version_TestItems(version, copyTest));
                }

                // Copy Step Links
                foreach (var copySteps in copy.EnablingObjective_Steps)
                {
                    //var verstep = await _ver_eoStepService.FindQuery(x => x.EOStepId == copySteps.ParentStepId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    //version.Version_EnablingObjective_Steps.Add(new Version_EnablingObjective_Step(version, verstep));
                }

                version.ModifiedDate = DateTime.Now;
                version.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

                await _versionEOService.UpdateAsync(version);

            }
        }

        public async Task<Version_EnablingObjective> VersionAndCreateCopy(Domain.Entities.Core.EnablingObjective eo, int state)
        {
            var copy = await _eoService.FindQuery(x => x.Id == eo.Id, true).FirstOrDefaultAsync();
            //var copy = await _eoService.FindQueryWithIncludeAsync(x => x.Id == eo.Id,
            //    new string[] {
            //                nameof(_eo.TestItems),
            //                nameof(_eo.Task_EnablingObjective_Links),
            //                nameof(_eo.RegRequirement_EO_Links),
            //                nameof(_eo.SafetyHazard_EO_Links),
            //                nameof(_eo.Procedure_EnablingObjective_Links),
            //                nameof(_eo.ILA_EnablingObjective_Links),
            //                nameof(_eo.EnablingObjective_MetaEO_Links),
            //                nameof(_eo.EnablingObjective_Employee_Links),
            //                nameof(_eo.Position_SQs),
            //                nameof(_eo.EnablingObjective_Steps),
            //                nameof(_eo.EnablingObjective_Questions)
            //            },true).FirstOrDefaultAsync();

            if (copy == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOToVersionNotFound"]);
            }
            else
            {
                var version = await CreateAsync(eo, state);
                // Copy Task Links
                foreach (var copyeo in copy.Task_EnablingObjective_Links)
                {
                    var verTask = await _ver_taskService.FindQuery(x => x.TaskId == copyeo.TaskId,true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    
                    if(verTask != null)
                    {
                        version.Version_EnablingObjective_Tasks.Add(new Version_EnablingObjective_Task(version, verTask));
                    }
                }

                // Copy RR Links
                foreach (var copyrr in copy.RegRequirement_EO_Links)
                {
                    var verRR = await _ver_RRService.FindQuery(x => x.RegulatoryRequirementId == copyrr.RegulatoryRequirementId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                if(verRR != null)
                    {
                        version.Version_EnablingObjective_RRLinks.Add(new Version_EnablingObjective_RRLink(version, verRR));

                    }
                }

                // Copy SH Links
                foreach (var copysh in copy.SafetyHazard_EO_Links)
                {
                    var versh = await _ver_shService.FindQuery(x => x.SaftyHazardId == copysh.SafetyHazardId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(versh != null)
                    {
                        version.Version_EnablingObjective_SaftyHazard_Links.Add(new Version_EnablingObjective_SaftyHazard_Link(version.Id, versh.SaftyHazardId, "1.0"));

                    }
                }

                // Copy Proc Links
                foreach (var copyproc in copy.Procedure_EnablingObjective_Links)
                {
                    var verProc = await _ver_procService.FindQuery(x => x.ProcedureId == copyproc.ProcedureId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verProc != null)
                    {
                        version.Version_Procedure_EnablingObjective_Links.Add(new Version_Procedure_EnablingObjective_Link(version.Id, verProc.Id, "1.0"));

                    }
                }

                // Copy ILA Links
                foreach (var copyila in copy.ILA_EnablingObjective_Links)
                {
                    var verila = await _ver_ilaService.FindQuery(x => x.ILAId == copyila.ILAId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verila != null)
                    {
                        version.Version_EnablingObjective_ILALinks.Add(new Version_EnablingObjective_ILALink(version, verila));

                    }
                }

                // Copy Meta Links
                foreach (var copymeta in copy.EnablingObjective_MetaEO_Links)
                {
                    var vermeta = await _versionEOService.FindQuery(x => x.EnablingObjectiveId == copymeta.EOID && x.EnablingObjectiveId != eo.Id, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(vermeta != null)
                    {
                        version.Version_EnablingObjective_MetaEOLinks.Add(new Version_EnablingObjective_MetaEOLink(version, vermeta, "1.0"));

                    }
                }

                // Copy Emp Links
                foreach (var copyemp in copy.EnablingObjective_Employee_Links)
                {
                    var veremp = await _ver_empService.FindQuery(x => x.EmployeeId == copyemp.EmployeeId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(veremp != null)
                    {
                        version.Version_EnablingObjective_Employee_Links.Add(new Version_EnablingObjective_Employee_Link(version, veremp, copyemp.StartDate));

                    }

                }

                // Copy Position Links
                foreach (var copyPos in copy.Position_SQs)
                {
                    var verpos = await _ver_posService.FindQuery(x => x.PositionId == copyPos.PositionId, true).OrderBy(o => o.Id).LastOrDefaultAsync();
                    if(verpos != null)
                    {
                        version.Version_EnablingObjective_Position_Links.Add(new Version_EnablingObjective_Position_Link(version, verpos));

                    }

                }

                // Copy Test Item Links
                foreach (var copyTest in copy.TestItems)
                {
                    version.Version_TestItems.Add(new Version_TestItems(version, copyTest));
                }

                // Copy Step Links
                foreach (var copySteps in copy.EnablingObjective_Steps)
                {
                    //var verstep = await _ver_eoStepService.FindQuery(x => x.EOStepId == copySteps.ParentStepId).OrderBy(o => o.Id).LastOrDefaultAsync();
                    version.Version_EnablingObjective_Steps.Add(new Version_EnablingObjective_Step(version, copySteps));
                }

                // Copy Questions
                foreach(var copyQues in copy.EnablingObjective_Questions)
                {
                    version.Version_EnablingObjective_Questions.Add(new Version_EnablingObjective_Question(version,copyQues));
                }

                // Copy Suggestions
                foreach(var copySugg in copy.EnablingObjective_Suggestions)
                {
                    version.Version_EnablingObjective_Suggestions.Add(new Version_EnablingObjective_Suggestions(version,copySugg));
                }


                version.ModifiedDate = DateTime.Now;
                version.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

                await _versionEOService.UpdateAsync(version);

                return version;

            }
        }

        public async Task<List<Version_EnablingObjective>> GetAllVersionsForEOAsync(int eoId)
        {
            var history = await _eo_histService.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            var persons = await _person_Serivce.AllQuery().ToListAsync();
            var versions = await _versionEOService.FindQueryWithIncludeAsync(x => x.EnablingObjectiveId == eoId, new string[] { nameof(_ver_eo.EnablingObjectiveHistories) }).ToListAsync();
            versions = versions.Where(version => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, version, Version_TaskOperations.Read).Result.Succeeded).ToList();
            //versions = (from v in versions
            //           join h in history on v.Id equals h.Version_TaskId
            //           join u in _userManager.Users on h.CreatedBy equals u.Id
            //           join p in persons on u.UserName equals p.Username
            //           select v).ToList();
            foreach (var version in versions)
            {
                if (version.ModifiedDate == null)
                {
                    var user = users.Where(x => x.Email == version.CreatedBy).Select(s => s.UserName).FirstOrDefault();
                    //version.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                    version.CreatedBy = user;
                }
                else
                {
                    var user = users.Where(x => x.Email == version.ModifiedBy).Select(s => s.UserName).FirstOrDefault();
                    //version.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                    version.CreatedBy = user;
                }
                foreach (var hist in version.EnablingObjectiveHistories)
                {
                    if (hist.ModifiedBy == null)
                    {
                        var user = users.Where(x => x.Email == hist.CreatedBy).Select(s => s.UserName).FirstOrDefault();
                        //hist.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                        version.CreatedBy = user;
                    }
                    else
                    {
                        var user = users.Where(x => x.Email == hist.ModifiedBy).Select(s => s.UserName).FirstOrDefault();
                        //hist.CreatedBy = persons.Where(x => x.Username == user).Select(s => s.FirstName).FirstOrDefault();
                        version.CreatedBy = user;
                    }
                }
            }
            return versions.OrderByDescending(o => o.VersionNumber).ToList();
        }

    }
}
