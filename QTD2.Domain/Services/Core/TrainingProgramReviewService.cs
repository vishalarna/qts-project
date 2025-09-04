using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TrainingProgramReviewService : Common.Service<TrainingProgramReview>, ITrainingProgramReviewService
    {
        public TrainingProgramReviewService(ITrainingProgramReviewsRepository trainingProgramReviewsRepository, ITrainingProgramReviewsValidation trainingProgramReviewsValidation)
            : base(trainingProgramReviewsRepository, trainingProgramReviewsValidation)
        {
        }

        public async System.Threading.Tasks.Task<TrainingProgramReview> GetAsync(int id)
        {
            var queryable = (await FindWithIncludeAsync(s => s.Id==id,new string[] { "TrainingProgram", "TrainingProgram.Position", "Reviewers.Employee.Person", "TrainingProgram.TrainingProgramType", "TrainingProgramReview_SupportingDocuments" })).FirstOrDefault();
            if (queryable != null)
            {
                queryable.Reviewers = queryable.Reviewers.Where(r => !r.Deleted).ToList();
            }
            return queryable;
        }

        public async System.Threading.Tasks.Task<List<TrainingProgramReview>> GetAllAsync()
        {
            var tprList = await AllWithIncludeAsync(new string[] { "TrainingProgram", "TrainingProgram.Position", "Reviewers.Employee.Person", "TrainingProgram.TrainingProgramType" });
            return tprList.ToList();
        }

        public async System.Threading.Tasks.Task<int> GetActiveInitialTrainingProgramReviewsAsync()
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => x.Active && x.TrainingProgram.TrainingProgramTypeId == 1;
            var count = await GetCount(predicate);
            return count;
        }

        public async System.Threading.Tasks.Task<int> GetInactiveInitialTrainingProgramReviewsAsync()
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => !x.Active && x.TrainingProgram.TrainingProgramTypeId == 1;
            return await GetCount(predicate);
        }

        public async System.Threading.Tasks.Task<int> GetActiveContinuingTrainingProgramReviewsAsync()
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => x.Active && x.TrainingProgram.TrainingProgramTypeId == 2;
            return await GetCount(predicate);
        }

        public async System.Threading.Tasks.Task<int> GetInactiveContinuingTrainingProgramReviewsAsync()
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => !x.Active && x.TrainingProgram.TrainingProgramTypeId == 2;
            return await GetCount(predicate);
        }

        public async System.Threading.Tasks.Task<int> GetActiveCycleTrainingProgramReviewsAsync()
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => x.Active && x.TrainingProgram.TrainingProgramTypeId == 3;
            return await GetCount(predicate);
        }

        public async System.Threading.Tasks.Task<int> GetInactiveCycleTrainingProgramReviewsAsync()
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => !x.Active && x.TrainingProgram.TrainingProgramTypeId == 3;
            return await GetCount(predicate);
        }
        public async System.Threading.Tasks.Task<List<TrainingProgramReview>> GetReviewsForDocumentStorageAsync()
        {
            var tprList = await AllWithIncludeAsync(new string[] { "TrainingProgram.TrainingProgramType"});
            return tprList.ToList();
        }
        public async System.Threading.Tasks.Task<List<TrainingProgramReview>> GetReviewsByTrainingProgramsAsync(List<int> trainingProgramIDs, DateTime startDate, DateTime endDate)
        {
            Expression<Func<TrainingProgramReview, bool>> predicate = x => trainingProgramIDs.Contains(x.TrainingProgramId) && startDate<=x.ReviewDate && endDate>=x.ReviewDate ;
            var tprList = await FindWithIncludeAsync(predicate,new string[] { "TrainingProgram.Position","TrainingProgram.TrainingProgramType", "Reviewers.Employee.Person" });
            return tprList.ToList();
        }


    }
}
