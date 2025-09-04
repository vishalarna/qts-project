using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Persistence;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.ToolCategory;
using QTD2.Infrastructure.Model.ToolGroup;
using ITask_DomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ISubDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.ISubdutyAreaService;
using IDutyAreaDomainService = QTD2.Domain.Interfaces.Service.Core.IDutyAreaService;
using QTD2.Domain.Exceptions;


namespace QTD2.Application.Services.Shared
{
    public class ToolService : Interfaces.Services.Shared.IToolService
    {
        private readonly IToolService _toolService;
        private readonly IToolGroupService _toolGroupService;
        private readonly IToolCategoryService _toolCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnablingObjectiveService _eoDomainService;
        private readonly ITaskService _taskDomainService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ToolService> _locaizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ToolCategory _toolCategory;
        private readonly ISafetyHazard_Tool_LinkService _safetyHazard_Tool_LinkService;
        private readonly ITask_ToolService _task_Tool_LinkService;
        private readonly IEnablingObjective_ToolService _tool_eo_LinkService;
        private readonly Task_Tool _task_tool_link;
        private readonly SafetyHazard_Tool_Link _safetyhazard_tool_link;
        private readonly EnablingObjective_Tool _enablingObjective_tool_link;
        private readonly ITask_DomainService _taskService;
        private readonly ISubDutyAreaDomainService _sdaService;
        private readonly IDutyAreaDomainService _daService;
        private readonly IMainUnitOfWork _mainUow;


        public ToolService(
            IEnablingObjectiveService eoDomainService,
            ITaskService taskDomainService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IToolService toolService,
            ITask_ToolService task_Tool_LinkService,
            IToolGroupService toolGroupService,
            IStringLocalizer<ToolService> locaizer,
            UserManager<AppUser> userManager,
            ISafetyHazard_Tool_LinkService safetyHazard_Tool_LinkService,
            IEnablingObjective_ToolService task_eo_LinkService,
            IToolCategoryService toolCategoryService,
            ITask_DomainService taskService,
            ISubDutyAreaDomainService sdaService,
            IDutyAreaDomainService daService,
            IMainUnitOfWork mainUow)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _toolService = toolService;
            _toolGroupService = toolGroupService;
            _locaizer = locaizer;
            _userManager = userManager;
            _toolCategoryService = toolCategoryService;
            _toolCategory = new ToolCategory();
            _eoDomainService = eoDomainService;
            _taskDomainService = taskDomainService;
            _safetyHazard_Tool_LinkService = safetyHazard_Tool_LinkService;
            _task_Tool_LinkService = task_Tool_LinkService;
            _tool_eo_LinkService = task_eo_LinkService;
            _task_tool_link = new Task_Tool();
            _safetyhazard_tool_link = new SafetyHazard_Tool_Link();
            _enablingObjective_tool_link = new EnablingObjective_Tool();
            _taskService = taskService;
            _sdaService = sdaService;
            _daService = daService;
            _mainUow = mainUow;
        }
        public async System.Threading.Tasks.Task ActiveToolAsync(int id)
        {
            var obj = await _toolService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _toolService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<Tool> UpdateToolAsync(int id, ToolCreateOptions options)
        {
            var obj = await _toolService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.ToolCategoryId = options.ToolCategoryId;
                obj.Description = options.Description;
                obj.Number = options.Number;
                obj.Name = options.Name;
                obj.Hyperlink = options.Hyperlink;
                obj.EffectiveDate = options.EffectiveDate;
                byte[] uploadarray = null;
                if (options.Upload != null && options.Upload.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        options.Upload.CopyTo(ms);
                        uploadarray = ms.ToArray();
                    }
                }
                obj.Upload = uploadarray;


                var validationResult = await _toolService.UpdateAsync(obj);
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
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }
        public async System.Threading.Tasks.Task DeleteToolAsync(int id)
        {
            var obj = await _toolService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _toolService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveToolAsync(int id)
        {
            var obj = await _toolService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _toolService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }
        public async System.Threading.Tasks.Task<List<ToolNestedData>> GetToolsWithCategoriesAsync()
        {
            var toolCategories = (await _toolCategoryService.AllWithIncludeAsync(new string[] { "Tools" })).ToList();
            if (toolCategories != null && toolCategories.Count>0)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, toolCategories[0], AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    var CategoryToolsNestedResults = toolCategories.Select(x=>new ToolNestedData
                    {
                       Category=new ToolCategoryData
                       {
                           Id=x.Id,
                           Title=x.Title,
                           Description=x.Description,
                           Website=x.Website,
                           EffectiveDate=x.EffectiveDate,
                           Notes=x.Notes,
                           Active=x.Active,
                           Deleted=x.Deleted
                       },
                       Tools=x.Tools.Select(y=>new ToolOptions
                       {
                           Id=y.Id,
                           ToolCategoryId=y.ToolCategoryId,
                           Number=y.Number,
                           Name=y.Name,
                           Hyperlink=y.Hyperlink,
                           EffectiveDate=y.EffectiveDate,
                           Description=y.Description,
                           Active=y.Active
                       }).OrderBy(x=>x.Number).ToList()
                    }).ToList();
                    
                  
                    
                    return CategoryToolsNestedResults;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task<List<TaskWithCountOptions>> GetLinkedTasksByToolId(int id)
        {
            //var toolTasks = await _toolService.GetWithIncludeAsync(id, new string[] { "Task_Tools.Task.Task_Tools" });
            //if (toolTasks != null && toolTasks.Task_Tools.Count > 0)
            //{
            //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, toolTasks, AuthorizationOperations.Read);
            //    if (result.Succeeded)
            //    {
            //        var resultSet = toolTasks.Task_Tools.Select(x => x.Task).ToList();
            //        return resultSet;
            //    }
            //    else
            //    {
            //        throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            //    }
            //}
            //else
            //{
            //    throw new Exception(message: _locaizer["RecordNotFound"].Value);
            //}



            //New implementation
            var links = await _task_Tool_LinkService.FindWithIncludeAsync(x => x.ToolId == id, new string[] { nameof(_task_tool_link.Task) });
            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _task_Tool_LinkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.FindQueryWithIncludeAsync(x => x.Id == task.Id, new string[] { "SubdutyArea.DutyArea" }).FirstOrDefaultAsync();

                var num = taskNumber.SubdutyArea.DutyArea.Letter + taskNumber.SubdutyArea.DutyArea.Number.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();
                //var taskGroups = await _taskTrainingGroupService.GetCount(x => x.TaskId == task.Id);
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }
        
        public async System.Threading.Tasks.Task<List<EnablingObjectiveWithCountOptions>> GetLinkedSkillsByToolId(int id)
        {
            //var toolEos = await _toolService.GetWithIncludeAsync(id,new string[] { "EnablingObjective_Tools.EnablingObjective.EnablingObjective_Tools" });
            //if (toolEos != null && toolEos.EnablingObjective_Tools.Count > 0)
            //{
            //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, toolEos, AuthorizationOperations.Read);
            //    if (result.Succeeded)
            //    {
            //        var resultSet = toolEos.EnablingObjective_Tools.Select(x => x.EnablingObjective).ToList();
            //        return resultSet;
            //    }
            //    else
            //    {
            //        throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            //    }
            //}
            //else
            //{
            //    throw new Exception(message: _locaizer["RecordNotFound"].Value);
            //}


            var links = await _tool_eo_LinkService.FindWithIncludeAsync(x => x.ToolId == id, new string[] { nameof(_enablingObjective_tool_link.EnablingObjective) });
            List<Domain.Entities.Core.EnablingObjective> eoList = new List<Domain.Entities.Core.EnablingObjective>();
            eoList.AddRange(links.Select(x => x.EnablingObjective));

            List<EnablingObjectiveWithCountOptions> eoWithCount = new List<EnablingObjectiveWithCountOptions>();
            foreach (var eo in eoList)
            {
                var data = await _tool_eo_LinkService.GetCount(x => x.EOId == eo.Id);
                eoWithCount.Add(new EnablingObjectiveWithCountOptions(eo.Number, eo.Description, eo.Id, data, eo.Active));
            }

            return eoWithCount;
        }
        public async System.Threading.Tasks.Task<List<SafetyHazardWithLinkCount>> GetLinkedsafetyHazardsByToolId(int id)
        {
            //var toolSafetyHazards = await _toolService.GetWithIncludeAsync(id,new string[] { "SafetyHazard_Tool_Links.SaftyHazard.SafetyHazard_Tool_Links" });
            //if (toolSafetyHazards != null && toolSafetyHazards.SafetyHazard_Tool_Links.Count > 0)
            //{
            //    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, toolSafetyHazards, AuthorizationOperations.Read);
            //    if (result.Succeeded)
            //    {
            //        var resultSet = toolSafetyHazards.SafetyHazard_Tool_Links.Select(x => x.SaftyHazard).ToList();
            //        return resultSet;
            //    }
            //    else
            //    {
            //        throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            //    }
            //}
            //else
            //{
            //    throw new Exception(message: _locaizer["RecordNotFound"].Value);
            //}

            var data = await _safetyHazard_Tool_LinkService.AllQueryWithInclude(new string[] { nameof(_safetyhazard_tool_link.SaftyHazard) }).Where(x => x.ToolId == id).Select(x => x.SaftyHazard).ToListAsync();
            List<SafetyHazardWithLinkCount> list = new List<SafetyHazardWithLinkCount>();
            foreach (var item in data)
            {
                var count = await _safetyHazard_Tool_LinkService.GetCount(x => x.SafetyHazardId == item.Id);
                var sh = new SafetyHazardWithLinkCount(item.Id, item.Title, item.Number, count, item.Active);
                list.Add(sh);
            }

            return list;
        }
        public async System.Threading.Tasks.Task LinkTasksByTools(LinkToolsOptions options)
        {
            
            
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new Task_Tool(), AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                foreach (var toolid in options.toolIds)
                {
                    foreach (var taskid in options.LinkedIds)
                    {
                        await _task_Tool_LinkService.AddAsync(new Task_Tool
                        {
                            TaskId = taskid,
                            ToolId = toolid
                        });
                    }
                }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            
        }
        public async System.Threading.Tasks.Task unLinkTasksByTools(LinkToolsOptions options)
        {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new Task_Tool(), AuthorizationOperations.Delete);
                if (result.Succeeded)
                {
                foreach (var toolid in options.toolIds)
                {
                    foreach (var taskid in options.LinkedIds)
                    {
                        var linkTObeRemoved = (await _task_Tool_LinkService.FindAsync(x=>x.TaskId==taskid && x.ToolId==toolid)).FirstOrDefault();
                        if (linkTObeRemoved!=null)
                        {
                            linkTObeRemoved.Delete();
                            await _task_Tool_LinkService.UpdateAsync(linkTObeRemoved);
                        }
                    }
                }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            
        }
        public async System.Threading.Tasks.Task LinkEOsByTools(LinkToolsOptions options)
        {
            
            
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new EnablingObjective_Tool(), AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                foreach (var toolid in options.toolIds)
                {
                    foreach (var eoId in options.LinkedIds)
                    {
                        await _tool_eo_LinkService.AddAsync(new EnablingObjective_Tool
                        {
                            EOId = eoId,
                            ToolId = toolid
                        });
                    }
                }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            
        }
        public async System.Threading.Tasks.Task unLinkEOsByTools(LinkToolsOptions options)
        {


            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new EnablingObjective_Tool(), AuthorizationOperations.Delete);
            if (result.Succeeded)
            {
                foreach (var toolid in options.toolIds)
                {
                    foreach (var eoId in options.LinkedIds)
                    {
                        var linkTObeRemoved = (await _tool_eo_LinkService.FindAsync(x => x.EOId == eoId && x.ToolId == toolid)).FirstOrDefault();
                        if (linkTObeRemoved != null)
                        {
                            linkTObeRemoved.Delete();
                            await _tool_eo_LinkService.UpdateAsync(linkTObeRemoved);
                        }
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }

        }
        public async System.Threading.Tasks.Task LinksafetyHazardsByTools(LinkToolsOptions options)
        {
            
            
                //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new SafetyHazard_Tool_Link(), AuthorizationOperations.Create);
                //if (result.Succeeded)
                //{
                foreach (var toolid in options.toolIds)
                {
                    foreach (var saftyHazardid in options.LinkedIds)
                    {
                        await _safetyHazard_Tool_LinkService.AddAsync(new SafetyHazard_Tool_Link
                        {
                            SafetyHazardId = saftyHazardid,
                            ToolId = toolid
                        });
                    }
                }
                //}
                //else
                //{
                //    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                //}
            
        }
        public async System.Threading.Tasks.Task unLinksafetyHazardsByTools(LinkToolsOptions options)
        {
            //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new SafetyHazard_Tool_Link(), AuthorizationOperations.Delete);
            //if (result.Succeeded)
            //{
                foreach (var toolid in options.toolIds)
                {
                    foreach (var sId in options.LinkedIds)
                    {
                        var linkTObeRemoved = (await _safetyHazard_Tool_LinkService.FindAsync(x => x.SafetyHazardId == sId && x.ToolId == toolid)).FirstOrDefault();
                        if (linkTObeRemoved != null)
                        {
                            linkTObeRemoved.Delete();
                            await _safetyHazard_Tool_LinkService.UpdateAsync(linkTObeRemoved);
                        }
                    }
                }
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            //}
        }

        public async Task<ToolDashboardModel> GetToolsStatistics()
        {
            var toolCategories = (await _toolCategoryService.AllWithIncludeAsync(new[] { "Tools" })).ToList();
            
            if (!toolCategories.Any())
            {
                throw new QTDServerException(_locaizer["RecordNotFound"].Value);
            }
            
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, toolCategories[0], AuthorizationOperations.Read);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
            
            var model = new ToolDashboardModel();
            var categoryToolsNestedResults = toolCategories.Select(x => new ToolNestedData
            {
                Category = new ToolCategoryData
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Website = x.Website,
                    EffectiveDate = x.EffectiveDate,
                    Notes = x.Notes,
                    Active = x.Active,
                    Deleted = x.Deleted
                },
                Tools = x.Tools.Select(y => new ToolOptions
                {
                    Id = y.Id,
                    ToolCategoryId = y.ToolCategoryId,
                    Number = y.Number,
                    Name = y.Name,
                    Hyperlink = y.Hyperlink,
                    EffectiveDate = y.EffectiveDate,
                    Description = y.Description,
                    Active = y.Active
                }).OrderBy(i => i.Number)
                    .ToList()
            }).ToList();
            
            var notLinkedTasksCount = await _mainUow.Repository<Tool>()
                .CountAsync(i => i.Active && !i.Task_Tools.Any());
            
            var notLinkedSkillAssessmentsCount = await _mainUow.Repository<Tool>()
                .CountAsync(i => i.Active && !i.EnablingObjective_Tools.Any());
            
            model.ActiveCaategories = categoryToolsNestedResults.Count(x => x.Category.Active);
            model.InActiveCaategories = categoryToolsNestedResults.Count(x => !x.Category.Active);
            model.ActiveToolsCount = categoryToolsNestedResults.Sum(item => item.Tools.Count(i => i.Active));
            model.InActiveToolsCount = categoryToolsNestedResults.Sum(item => item.Tools.Count(i => !i.Active));
            model.ToolsNotLinkedToSkillAssesmentCount = notLinkedSkillAssessmentsCount;
            model.ToolsNotLinkedToTaskCount = notLinkedTasksCount;
            return model;
        }

        public async Task<List<Tool>> GetToolList(string notLinkedWith)
        {
            var procList = new List<Tool>();

            switch (notLinkedWith.ToLower().Trim())
            {
                case "active":
                    procList = await _toolService.FindQuery(x => x.Active == true).ToListAsync();
                    break;
                case "inactive":
                    procList = await _toolService.FindQuery(x => x.Active == false).ToListAsync();
                    break;
            }

            return procList;
        }

        public async Task<List<ToolCategory>> GetCategoryList(string notLinkedWith)
        {
            var procList = new List<ToolCategory>();

            switch (notLinkedWith.ToLower().Trim())
            {
                case "catinactive":
                    procList = await _toolCategoryService.FindQuery(x => x.Active == false).ToListAsync();
                    break;
                case "catactive":
                    procList = await _toolCategoryService.FindQuery(x => x.Active == true).ToListAsync();
                    break;
            }

            return procList;
        }

        public async Task<Result<IEnumerable<UnlinkedToolDto>>> GetToolsNotLinkedToTaskAsync()
        {
            var toolsNotRelatedToTask = await _mainUow.Repository<Tool>()
                .GetListAsync(i => i.Active && !i.Task_Tools.Any(), "ToolCategory");

            var unlinkedToolsDto = toolsNotRelatedToTask.Select(tool => new UnlinkedToolDto
            {
                Id = tool.Id,
                Active = tool.Active,
                Number = tool.Number,
                Name = tool.Name,
                CategoryName = tool.ToolCategory.Title,
            });

            return Result<IEnumerable<UnlinkedToolDto>>.CreateSuccess(unlinkedToolsDto);
        }

        public async Task<Result<IEnumerable<UnlinkedToolDto>>> GetToolsNotLinkedToEoAsync()
        {
            var toolsNotRelatedToEo = await _mainUow.Repository<Tool>()
                .GetListAsync(i => i.Active && !i.EnablingObjective_Tools.Any(), "ToolCategory");
            
            var unlinkedToolsDto = toolsNotRelatedToEo.Select(tool => new UnlinkedToolDto
            {
                Id = tool.Id,
                Active = tool.Active,
                Number = tool.Number,
                Name = tool.Name,
                CategoryName = tool.ToolCategory.Title,
            });
            
            return Result<IEnumerable<UnlinkedToolDto>>.CreateSuccess(unlinkedToolsDto);
        }

        public async System.Threading.Tasks.Task ActiveToolCategoryAsync(int id)
        {
            var obj = await GetToolCategoryAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _toolCategoryService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<Tool>> AddTooltoToolGroupAsync(int toolGroupId, ToolAddOptions options)
        {
            var tg = await GetToolGroupAsync(toolGroupId);
            var resultTG = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tg, ToolGroupOperations.Update);
            List<Tool> toolList = new List<Tool>();
            foreach (var id in options.ToolIds)
            {
                var tool = await GetAsync(id);
                var resultT = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);
                if (resultT.Succeeded && resultTG.Succeeded)
                {
                    var tg_tool = tg.AddTool(tool);
                    tg_tool.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    tg_tool.CreatedDate = DateTime.Now;
                    var validationResult = await _toolGroupService.UpdateAsync(tg);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        toolList.Add(tool);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
                }
            }

            return toolList;
        }

        public async Task<Tool> CreateAsync(ToolCreateOptions options)
        {
            var tool = await _toolService.FindQuery(x => x.Number.ToLower().Trim() == options.Number.ToLower().Trim() && x.ToolCategoryId == options.ToolCategoryId).FirstOrDefaultAsync();
            if (tool == null)
            {
                byte[] uploadarray = null;
                if (options.Upload != null && options.Upload.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        options.Upload.CopyTo(ms);
                        uploadarray = ms.ToArray();
                    }
                }
                var number = (await _toolService.FindQueryWithDeleted(x => x.ToolCategoryId == options.ToolCategoryId).ToListAsync()).Count;
                tool = new Tool(options.ToolCategoryId, (number + 1).ToString(), options.Name, options.Hyperlink, options.EffectiveDate, uploadarray, options.Description);
            }
            else
            {
                throw new BadHttpRequestException(message: _locaizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Create);
            if (result.Succeeded)
            {
                tool.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                tool.CreatedDate = DateTime.Now;
                var validationResult = await _toolService.AddAsync(tool);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return tool;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async Task<int> GetToolNumber(int catId)
        {
            var number = (await _toolService.FindQueryWithDeleted(x => x.ToolCategoryId == catId).ToListAsync()).Count;
            return number + 1;
        }

        public async Task<ToolCategory> CreateToolCategoryAsync(ToolCategoryCreateOptions options)
        {
            var obj = await _toolCategoryService.FindQuery(x => x.Title.ToLower().Trim() == options.Title.ToLower().Trim()).FirstOrDefaultAsync();
            if (obj == null)
            {
                obj = new ToolCategory(options.Title, options.Description, options.Website, options.EffectiveDate, options.Notes);
            }
            else
            {
                throw new BadHttpRequestException(message: _locaizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _toolCategoryService.AddAsync(obj);
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
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async Task<ToolGroup> CreateToolGroupAsync(ToolGroupCreateOptions options)
        {
            var tg = new ToolGroup(options.Description, options.Active);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tg, ToolGroupOperations.Create);
            if (result.Succeeded)
            {
                tg.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                tg.CreatedDate = DateTime.Now;
                var validationResult = await _toolGroupService.AddAsync(tg);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return tg;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int toolId)
        {
            var tool = await GetAsync(toolId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Delete);

            if (result.Succeeded)
            {
                tool.Deactivate();

                var validationResult = await _toolService.UpdateAsync(tool);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteToolCategoryAsync(int id)
        {
            var obj = await GetToolCategoryAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _toolCategoryService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteToolGroupAsync(int toolGroup)
        {
            var tg = await GetToolGroupAsync(toolGroup);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tg, ToolGroupOperations.Delete);

            if (result.Succeeded)
            {
                tg.Deactivate();
                var validationResult = await _toolGroupService.UpdateAsync(tg);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<Tool>> GetAsync()
        {
            var tools = await _toolService.AllAsync();
            tools = tools.Where(tool => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read).Result.Succeeded);
            return tools?.ToList();
        }

        public async Task<Tool> GetAsync(int toolId)
        {
            var tool = await _toolService.FindQueryWithIncludeAsync(x => x.Id == toolId, new string[] { "ToolCategory", "Task_Tools", "SafetyHazard_Tool_Links", "EnablingObjective_Tools" }).FirstOrDefaultAsync();
            if (tool != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);
                if (result.Succeeded)
                {
                    return tool;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["ToolNotFound"]);
            }
        }

        public async Task<List<ToolCategory>> GetToolCategoriesAsync(bool includeTools = false)
        {
            var obj_list = includeTools ? await _toolCategoryService.AllQueryWithInclude(new string[] { nameof(_toolCategory.Tools) }).ToListAsync() : await _toolCategoryService.AllQuery().ToListAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return obj_list;
        }

        public async Task<ToolCategory> GetToolCategoryAsync(int id)
        {
            var obj =  (await _toolCategoryService.FindWithIncludeAsync(x => x.Id == id, new string[] { "Tools" })).FirstOrDefault();
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["RecordNotFound"].Value);
            }
        }

        public async Task<ToolGroup> GetToolGroupAsync(int toolGroupId)
        {
            var toolGroup = await _toolGroupService.GetAsync(toolGroupId);
            if (toolGroup != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, toolGroup, ToolGroupOperations.Read);
                if (result.Succeeded)
                {
                    return toolGroup;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["ToolGroupNotFound"]);
            }
        }

        public async Task<List<ToolGroup>> GetToolGroupsAsync()
        {
            var toolGroups = await _toolGroupService.AllAsync();
            toolGroups = toolGroups.Where(tg => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tg, ToolGroupOperations.Read).Result.Succeeded);
            return toolGroups?.ToList();
        }

        public async System.Threading.Tasks.Task InActiveToolCategoryAsync(int id)
        {
            var obj = await GetToolCategoryAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _toolCategoryService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

  

        public async System.Threading.Tasks.Task RemoveToolFromToolGroupAsync(int toolGroupId, int toolId)
        {
            var tg = await GetToolGroupAsync(toolGroupId);
            var tool = await GetAsync(toolId);
            var resultTG = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tg, ToolGroupOperations.Update);
            var resultT = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);
            if (resultT.Succeeded && resultTG.Succeeded)
            {
                tg.RemoveTool(tool);
                var validationResult = await _toolGroupService.UpdateAsync(tg);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async Task<Tool> UpdateAsync(int toolId, ToolUpdateOptions options)
        {
            var tool = await GetAsync(toolId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update Logic
                tool.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                tool.ModifiedDate = DateTime.Now;
                var validationResult = await _toolService.UpdateAsync(tool);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return tool;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async Task<ToolCategory> UpdateToolCategoryAsync(int id, ToolCategoryUpdateOptions options)
        {
            var obj = await GetToolCategoryAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.Title = options.Title;
                obj.Description = options.Description;
                obj.EffectiveDate = options.EffectiveDate;
                obj.Website = options.Website;
                obj.Notes = options.Notes;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _toolCategoryService.UpdateAsync(obj);
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
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ToolGroup> UpdateToolGroupAsync(int toolGroupId, ToolGroupUpdateOptions options)
        {
            var tg = await GetToolGroupAsync(toolGroupId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tg, ToolGroupOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update Logic
                tg.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                tg.ModifiedDate = DateTime.Now;
                var validationResult = await _toolGroupService.UpdateAsync(tg);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return tg;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<Domain.Entities.Core.Tool>> GetToolTaskIsLinkedTo(int id)
        {
            var data = await _task_Tool_LinkService.AllQueryWithInclude(new string[] { "Tool" }).Where(x => x.TaskId == id).Select(x => x.Tool).ToListAsync();
            return data;
        }

        public async Task<List<Tool>> GetToolsEoIsLinkedTo(int id)
        {
            var data = await _tool_eo_LinkService.AllQueryWithInclude(new string[] { "Tool" }).Where(x => x.EOId == id).Select(x => x.Tool).ToListAsync();
            return data;
        }


        public async Task<List<Tool>> GetToolsSHIsLinkedTo(int id)
        {
            var data = await _safetyHazard_Tool_LinkService.AllQueryWithInclude(new string[] { "Tool" }).Where(x => x.SafetyHazardId == id).Select(x => x.Tool).ToListAsync();
            return data;
        }

    }
}
