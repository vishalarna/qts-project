using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
//using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.TrainingProgram;
using QTD2.Infrastructure.Model.TrainingProgram_ILA_Link;
using QTD2.Infrastructure.Model.TrainingProgramReview;
using ITrainingProgram_ILA_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingPrograms_ILA_LinkService;
namespace QTD2.Application.Services.Shared
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramService _trainingProgramService;
        private readonly Domain.Interfaces.Service.Core.ITrainingProgramTypeService _trainingProgramtypeService;
        private readonly Domain.Interfaces.Service.Core.IILAService _ilaDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingProgram> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITrainingProgram_ILA_LinkDomainService _trainingProgramILAService;
        private readonly TrainingPrograms_ILA_Link _trainingProgram_ILA_Link;
        private readonly IILAService _ilaService;
        private readonly TrainingProgram _trainingProgram;

        public TrainingProgramService(
            Domain.Interfaces.Service.Core.ITrainingProgramService trainingProgramService,
            Domain.Interfaces.Service.Core.ITrainingProgramTypeService trainingProgramtypeService,
             IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TrainingProgram> localizer,
            ITrainingProgram_ILA_LinkDomainService trainingProgramILAService,
          IILAService ilaService,
          Domain.Interfaces.Service.Core.IILAService ilaDomainService,
        UserManager<AppUser> userManager)
        {
            _trainingProgramService = trainingProgramService;
            _trainingProgramtypeService = trainingProgramtypeService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _trainingProgramILAService = trainingProgramILAService;
            _trainingProgram_ILA_Link = new TrainingPrograms_ILA_Link();
            _trainingProgram = new TrainingProgram();
            _ilaService = ilaService;
            _localizer = localizer;
            _userManager = userManager;
            _ilaDomainService = ilaDomainService;
        }

        public async Task<TrainingProgram> CreateAsync(TrainingProgramCreateOptions options)
        {
            var trainingProgramtype =(await _trainingProgramtypeService.GetAsync(options.TrainingProgramTypeId))?.TrainingProgramTypeTitle;
            var db_trainingProgram = new TrainingProgram();
            if (trainingProgramtype == "Initial Training Program")
            {
                options.ProgramTitle = null;
                db_trainingProgram = (await _trainingProgramService.FindAsync(x => x.PositionId == options.PositionId && x.TPVersionNo == options.TPVersionNo)).FirstOrDefault();
                if (db_trainingProgram != null && options.copiedTrainingId != null)
                {
                    options.TPVersionNo = db_trainingProgram.TPVersionNo + "-copy";
                    db_trainingProgram = null;
                }
            }
            else if (trainingProgramtype == "Cycle Training Program")
            {
                options.TPVersionNo = null;
                db_trainingProgram = (await _trainingProgramService.FindAsync(x => x.PositionId == options.PositionId && x.ProgramTitle == options.ProgramTitle)).FirstOrDefault();
                if (db_trainingProgram != null && options.copiedTrainingId != null)
                {
                    options.ProgramTitle = db_trainingProgram.ProgramTitle + "-copy";
                    db_trainingProgram = null;
                }
            }
            else
            {
                options.ProgramTitle = null;
                options.TPVersionNo = null;
                db_trainingProgram = null;
            }

            //var db_trainingProgram = (await _trainingProgramService.FindAsync(x => x.PositionId == options.PositionId && x.ProgramTitle == options.ProgramTitle)).FirstOrDefault();
            var tpExist = db_trainingProgram != null;

            if (tpExist)
            {
                throw new BadHttpRequestException(message: _localizer["TrainingProgramExists"]);
            }

            var trainingProgram = new TrainingProgram(options.PositionId, options.TPVersionNo, options.ProgramTitle, options.TrainingProgramTypeId, options.StartDate.ToUniversalTime(), options.EndDate?.ToUniversalTime(), options.Year, options.Description, options.Publish);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Create);

            if (result.Succeeded)
            {
                trainingProgram.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                trainingProgram.CreatedDate = DateTime.Now;
                var validationResult = await _trainingProgramService.AddAsync(trainingProgram);
                if (validationResult.IsValid)
                {
                    //Now link ILA from existing trainingProgram
                    if (options.copiedTrainingId != null)
                    {
                        var trainingProg = await _trainingProgramService.GetWithIncludeAsync(options.copiedTrainingId ?? 0, new string[] { nameof(_trainingProgram.TrainingProgram_ILA_Links) });
                        if (trainingProg.TrainingProgram_ILA_Links != null && trainingProg.TrainingProgram_ILA_Links.Count > 0)
                        {
                            var opt = new TrainingProgram_ILA_LinkCreateOptions();
                            var list = new List<int>();
                            foreach (var link in trainingProg.TrainingProgram_ILA_Links)
                            {
                                list.Add(link.ILAId);
                            }
                            if (list != null && list.Count() > 0)
                            {
                                opt.ILAIds = list.ToArray();
                                await LinkILA(trainingProgram.Id, opt);
                            }
                        }
                        //

                    }
                    return trainingProgram;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var trainingProgram = await GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgram.Delete();
                var validationResult = await _trainingProgramService.UpdateAsync(trainingProgram);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }
        public async System.Threading.Tasks.Task DeactivateAsync(int id)
        {
            var trainingProgram = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgram.Deactivate();

                var validationResult = await _trainingProgramService.UpdateAsync(trainingProgram);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async System.Threading.Tasks.Task ActivateAsync(int id)
        {
            var trainingProgram = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Delete);
            if (result.Succeeded)
            {
                trainingProgram.Activate();

                var validationResult = await _trainingProgramService.UpdateAsync(trainingProgram);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        public async Task<List<TrainingProgram>> GetAsync()
        {
            var trainingPrograms = await _trainingProgramService.AllQueryWithInclude(new string[] { "Position.EmployeePositions", nameof(_trainingProgram.TrainingProgramType)}, true).ToListAsync();
            trainingPrograms = trainingPrograms.Where(tp => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tp, TrainingProgramOperations.Read).Result.Succeeded).ToList();
            return trainingPrograms.OrderBy(x => x.TrainingProgramType.TrainingProgramTypeTitle).ToList();
        }

        public async Task<List<TrainingProgram>> GetTPByPositionIdAsync(int posId, string trainingProgramType = null)
        {
            var tps = await _trainingProgramService.FindQuery(x => x.PositionId == posId
                                                                    && (String.IsNullOrEmpty(trainingProgramType) || x.TrainingProgramType.TrainingProgramTypeTitle.ToUpper() == trainingProgramType.ToUpper())
                                                                    && ((x.ProgramTitle != null && x.ProgramTitle != "") || (x.TPVersionNo != null && x.TPVersionNo != "")), true).ToListAsync();
            tps = tps.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, TrainingProgramOperations.Read).Result.Succeeded).ToList();
            return tps;
        }

        public async Task<TrainingProgram> GetAsync(int id)
        {
            var trainingProgram = await _trainingProgramService.GetWithIncludeAsync(id, new string[] { nameof(_trainingProgram.Position), nameof(_trainingProgram.TrainingProgramType) });
            //var trainingProgram = await _trainingProgramService.GetAsync(id);
            if (trainingProgram != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Read);
                if (result.Succeeded)
                {
                    return trainingProgram;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            throw new QTDServerException(_localizer["TrainingProgramNotFound"]);
        }

        public async Task<List<TrainingProgram>> GetByPositionIdAsync(int positionId)
        {
            var trainingPrograms = await _trainingProgramService.FindAsync(x => x.PositionId == positionId);
            return trainingPrograms.ToList();
        }

        public async Task<TrainingProgram> UpdateAsync(int id, TrainingProgramUpdateOptions options)
        {
            var trainingProgram = await GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Update);
            if (result.Succeeded)
            {
                // Todo Update Logic
                trainingProgram.PositionId = options.PositionId;
                trainingProgram.TPVersionNo = options.TPVersionNo;
                trainingProgram.ProgramTitle = options.ProgramTitle;
                trainingProgram.TrainingProgramTypeId = options.TrainingProgramTypeId;
                trainingProgram.StartDate = options.StartDate.ToUniversalTime();
                trainingProgram.EndDate = options.EndDate?.ToUniversalTime();
                trainingProgram.Year = options.Year;
                trainingProgram.Description = options.Description;
                trainingProgram.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                trainingProgram.ModifiedDate = DateTime.Now;
                var validationResult = await _trainingProgramService.UpdateAsync(trainingProgram);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return trainingProgram;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<ILAWithCountOptions>> GetLinkedILAs(int id)
        {
            var links = await _trainingProgramILAService.FindWithIncludeAsync(x => x.TrainingProgramId == id, new string[] { nameof(_trainingProgram_ILA_Link.ILA) });
            List<Domain.Entities.Core.ILA> ilaList = new List<Domain.Entities.Core.ILA>();
            ilaList.AddRange(links.Select(x => x.ILA));
            List<ILAWithCountOptions> iLAWithCount = new List<ILAWithCountOptions>();
            foreach (var ila in ilaList)
            {
                var data = await _trainingProgramILAService.GetCount(x => x.ILAId == ila.Id);
                iLAWithCount.Add(new ILAWithCountOptions(ila.Number, ila.Name, ila.Id, data, ila.Active));
            }

            return iLAWithCount;
        }

        public async Task<TrainingProgram> LinkILA(int trainingProgramId, TrainingProgram_ILA_LinkCreateOptions options)
        {
            var trainingProg = await _trainingProgramService.GetWithIncludeAsync(trainingProgramId, new string[] { nameof(_trainingProgram.TrainingProgram_ILA_Links) });

            var ilas = await _ilaDomainService.GetByListOfIdsAsync(options.ILAIds);

            var trainingProgramResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProg, TrainingProgramOperations.Update);
            if (!trainingProgramResult.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

            foreach (var ila in ilas)
            {
                trainingProg.UnlinkILA(ila);
                trainingProg.LinkILA(ila);
            }

            var validationResult = await _trainingProgramService.UpdateAsync(trainingProg);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            return trainingProg;
        }

        public async System.Threading.Tasks.Task UnlinkILA(int trainingProgramId, int[] ilaId)
        {
            var trainingProg = await _trainingProgramService.GetWithIncludeAsync(trainingProgramId, new string[] { nameof(_trainingProgram.TrainingProgram_ILA_Links) });
            foreach (var id in ilaId)
            {
                var ila = await _ilaService.GetAsync(id);

                var procResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProg, TrainingProgramOperations.Update);
                var ilaResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ila, ILAOperations.Read);
                if (ilaResult.Succeeded && procResult.Succeeded)
                {
                    trainingProg.UnlinkILA(ila);
                    var validationResult = await _trainingProgramService.UpdateAsync(trainingProg);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }
        public async Task<List<TrainingProgram>> GetTrainingProgramILAIsLinkedTo(int id)
        {
            var data = await _trainingProgramILAService.AllQueryWithInclude(new string[] { nameof(_trainingProgram_ILA_Link.TrainingProgram) }).Where(x => x.ILAId == id).Select(x => x.TrainingProgram).ToListAsync();
            return data;
        }

        public async Task<TrainingProgramVM> GetStatsCount()
        {
            var inittalTrainingProgramType = _trainingProgramtypeService.AllQuery().Where(x => x.TrainingProgramTypeTitle == "Initial Training Program").Select(x => x.Id).FirstOrDefault();
            var continuingTrainingProgramType = _trainingProgramtypeService.AllQuery().Where(x => x.TrainingProgramTypeTitle == "Continuing Training Program").Select(x => x.Id).FirstOrDefault();
            var cycleTrainingProgramType = _trainingProgramtypeService.AllQuery().Where(x => x.TrainingProgramTypeTitle == "Cycle Training Program").Select(x => x.Id).FirstOrDefault();

            var stats = new TrainingProgramVM()
            {
                InitialTrainingProgramActive = await _trainingProgramService.GetCount(x => x.Active == true && x.TrainingProgramTypeId == inittalTrainingProgramType),
                InitialTrainingProgramInActive = await _trainingProgramService.GetCount(x => x.Active == false && x.TrainingProgramTypeId == inittalTrainingProgramType),
                ContinuingTrainingProgramActive = await _trainingProgramService.GetCount(x => x.Active == true && x.TrainingProgramTypeId == continuingTrainingProgramType),
                ContinuingTrainingProgramInActive = await _trainingProgramService.GetCount(x => x.Active == false && x.TrainingProgramTypeId == continuingTrainingProgramType),
                CycleTrainingProgramActive = await _trainingProgramService.GetCount(x => x.Active == true && x.TrainingProgramTypeId == cycleTrainingProgramType),
                CyclelTrainingProgramInActive = await _trainingProgramService.GetCount(x => x.Active == false && x.TrainingProgramTypeId == cycleTrainingProgramType)
            };

            return stats;
        }
        public async Task<List<TrainingProgram>> GetTrainingProgamsByFilter(string filter, TrainingProgramFilterOptions options)
        {
            var trainingPrograms = await _trainingProgramService.AllQueryWithInclude(new string[] { nameof(_trainingProgram.Position), nameof(_trainingProgram.TrainingProgramType) }, true).ToListAsync();
            switch (filter)

            {
                case "position":
                    trainingPrograms = trainingPrograms.Where(x => options.positionIds.Contains(x.PositionId)).ToList();
                    break;
                case "year":
                    trainingPrograms = trainingPrograms.Where(x => x.StartDate.Year.ToString() == options.StartYear || x.EndDate?.Year.ToString() == options.EndYear).ToList();
                    break;
                case "trainingProogramType":
                    var trainingProgramType = _trainingProgramtypeService.AllQuery().Where(x => x.TrainingProgramTypeTitle == options.trainingProgramTitle).Select(x => x.Id).FirstOrDefault();
                    trainingPrograms = trainingPrograms.Where(x => x.TrainingProgramTypeId == trainingProgramType).ToList();
                    break;
            }

            return trainingPrograms.ToList();

        }

        public async Task<TrainingProgram> PublishTrainingProgramAsync(int id, TrainingProgram_HistoryCreateOption options)
        {
            var trainingProgram = await GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingProgram, TrainingProgramOperations.Update);
            if (result.Succeeded)
            {

                trainingProgram.Publish = true;
                trainingProgram.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                trainingProgram.ModifiedDate = DateTime.Now;
                var validationResult = await _trainingProgramService.UpdateAsync(trainingProgram);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return trainingProgram;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

            return trainingProgram;
        }


        public async Task<List<TrainingProgram>> GetActiveInactiveList(string name, bool status)
        {
            var trainingProgramList = new List<TrainingProgram>();
            var trainingProgramTypeId = await _trainingProgramtypeService.FindQuery(x => x.TrainingProgramTypeTitle == name).Select(x => x.Id).FirstOrDefaultAsync();

            switch (name.ToLower().Trim())
            {
                //active
                case "initial training program":
                    {
                        trainingProgramList = await _trainingProgramService.FindQueryWithIncludeAsync(x => x.TrainingProgramTypeId == trainingProgramTypeId && x.Active == status, new string[] { "Position.EmployeePositions", "Position", "TrainingProgramType" }).ToListAsync();
                        break;
                    }
                case "continuing training program":
                    {
                        trainingProgramList = await _trainingProgramService.FindQueryWithIncludeAsync(x => x.TrainingProgramTypeId == trainingProgramTypeId && x.Active == status, new string[] { "Position.EmployeePositions", "Position", "TrainingProgramType" }).ToListAsync();
                        break;
                    }

                case "cycle training program":
                    {
                        trainingProgramList = await _trainingProgramService.FindQueryWithIncludeAsync(x => x.TrainingProgramTypeId == trainingProgramTypeId && x.Active == status, new string[] { "Position.EmployeePositions", "Position", "TrainingProgramType" }).ToListAsync();
                        break;
                    }
            }

            return trainingProgramList;
        }

        public async Task<List<TrainingProgram_VersionTitleViewModel>> GetActiveVersionTitleWithNoReviewAsync()
        {
            var trainingPrograms = await _trainingProgramService.GetActiveVersionTitleWithNoReviewAsync();
            List<TrainingProgram_VersionTitleViewModel> trainingProgram_VersionTitleViewModel = trainingPrograms.Select(tp => new
            TrainingProgram_VersionTitleViewModel(
                tp.Id, tp.PositionId, tp.Position.PositionTitle, tp.TrainingProgramTypeId, tp.TrainingProgramType.TrainingProgramTypeTitle, tp.ProgramTitle, tp.Version)).ToList();
            return trainingProgram_VersionTitleViewModel;
        }

        public async Task<List<TrainingProgram_VersionTitleViewModel>> GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(int positionId, int trainingProgramTypeId)
        {
            var trainingPrograms = await _trainingProgramService.GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(positionId, trainingProgramTypeId);
            List<TrainingProgram_VersionTitleViewModel> trainingProgram_VersionTitleViewModel = trainingPrograms
                .OrderByDescending(tp => tp.StartDate)
                .Select(tp => new
                    TrainingProgram_VersionTitleViewModel(
                        tp.Id, tp.PositionId, tp.Position.PositionTitle, tp.TrainingProgramTypeId, tp.TrainingProgramType.TrainingProgramTypeTitle, tp.ProgramTitle, tp.Version))
                .ToList();
            return trainingProgram_VersionTitleViewModel;
        }

        public async Task<List<TrainingProgram>> GetTrainingProgramLinksAndTrainingProgramReviewAsync()
        {
            var trainingPrograms = await _trainingProgramService.GetAllTrainingProgramLinksWithTrainingProgramReviewAsync();
            return trainingPrograms.ToList();
        }
    }
}
