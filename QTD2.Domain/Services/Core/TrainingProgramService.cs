using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System.Linq;
using System.Linq.Expressions;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System;

namespace QTD2.Domain.Services.Core
{
    public class TrainingProgramService : Common.Service<TrainingProgram>, ITrainingProgramService
    {
        public TrainingProgramService(ITrainingProgramRepository trainingProgramRepository, ITrainingProgramValidation trainingProgramValidation)
            : base(trainingProgramRepository, trainingProgramValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetAllTrainingProgramAsync()
        {
            var trainingPrograms = await AllWithIncludeAsync(new[] { "Position" });
            return trainingPrograms.ToList();
        }
        
        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetAllTrainingProgramWithTypesAsync()
        {
            var trainingProgramTypes = await AllWithIncludeAsync(new[] { "TrainingProgramType" });
            return trainingProgramTypes.ToList();
        }
        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetTrainingProgramCompactWithIncludeTypeAndILALinks()
        {
            var trainingPrograms = await AllQueryWithInclude(new string[] { "TrainingProgramType", "TrainingProgram_ILA_Links" }).Select(s => new TrainingProgram
            {
                Active = s.Active,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                Id = s.Id,
                PositionId = s.PositionId,
                TrainingProgramType = new TrainingProgramType
                {
                    TrainingProgramTypeTitle = s.TrainingProgramType.TrainingProgramTypeTitle,
                    Active = s.TrainingProgramType.Active,
                    Id = s.TrainingProgramType.Id
                },
                TrainingProgram_ILA_Links = s.TrainingProgram_ILA_Links.Select(l => new TrainingPrograms_ILA_Link
                {
                    Id = l.Id,
                    Active = l.Active,
                    ILAId = l.ILAId,
                    TrainingProgramId = l.TrainingProgramId,
                }).ToList(),
                Deleted = s.Deleted,
                Description = s.Description,
                EndDate = s.EndDate,
                ProgramTitle = s.ProgramTitle,
                Publish = s.Publish,
                StartDate = s.StartDate,
                TPVersionNo = s.TPVersionNo,
                TrainingProgramTypeId = s.TrainingProgramTypeId,
                Year = s.Year
            }).ToListAsync();

            return trainingPrograms;
        }

        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetActiveVersionTitleWithNoReviewAsync()
        {
            List<Expression<Func<TrainingProgram, bool>>> predicates = new List<Expression<Func<TrainingProgram, bool>>>();
            predicates.Add(x => x.Active && x.TrainingProgramReviews.Count() == 0);
            var trainingPrograms = await FindWithIncludeAsync(predicates,new string[] { "TrainingProgramType", "Position" });
            return trainingPrograms.ToList();
        }

        public async System.Threading.Tasks.Task<int> GetActiveTrainingProgramsWithReviewInDraftCountAsync()
        {
            Expression<Func<TrainingProgram, bool>> predicate = x => x.Active && x.TrainingProgramReviews.Count() > 0 && x.TrainingProgramReviews.Any(review => !review.Published);
            return await GetCount(predicate);
        }

        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(int positionId, int trainingProgramTypeId)
        {
            List<Expression<Func<TrainingProgram, bool>>> predicates = new List<Expression<Func<TrainingProgram, bool>>>();
            predicates.Add(x => x.Active);
            if (positionId > 0)
                predicates.Add(x => x.PositionId == positionId);
            if (trainingProgramTypeId > 0)
                predicates.Add(x => x.TrainingProgramTypeId == trainingProgramTypeId);
            var trainingPrograms = await FindWithIncludeAsync(predicates, new string[] { "Position","TrainingProgramType" });
            return trainingPrograms.ToList();
        }
        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetAllAsync()
        {
            var trainingProgramList = await AllWithIncludeAsync(new string[] { "Position", "TrainingProgramType" });
            return trainingProgramList.ToList();
        }
        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetInitialTrainingProgramsAsync()
        {
            var trainingProgramList = await FindWithIncludeAsync(x=>x.TrainingProgramType.TrainingProgramTypeTitle== "Initial Training Program", new string[] { "Position", "TrainingProgramType" });
            return trainingProgramList.ToList();
        }

        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetTrainingProgramsByIdAndPositionIdsAsync(List<int> positionIds, string trainingProgramId)
        {
            var trainingProgramList = await FindWithIncludeAsync(x => positionIds.Contains(x.PositionId) && x.Id.ToString()==trainingProgramId, new[] { "TrainingProgramType", "TrainingProgram_ILA_Links.ILA" });
            return trainingProgramList.ToList();
        }
        public async System.Threading.Tasks.Task<List<TrainingProgram>> GetAllTrainingProgramLinksWithTrainingProgramReviewAsync()
        {
            var trainingPrograms = await AllWithIncludeAsync(new[] { "TrainingProgram_ILA_Links", "TrainingProgramReviews" });
            return trainingPrograms.ToList();
        }
    }
}
