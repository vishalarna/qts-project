using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.StudentEvaluationAudience;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationAudienceService
    {
        public Task<List<StudentEvaluationAudience>> GetAsync();

        public Task<StudentEvaluationAudience> GetAsync(int id);

        public Task<StudentEvaluationAudience> CreateAsync(StudentEvaluationAudienceCreateOptions options);

        public Task<StudentEvaluationAudience> UpdateAsync(int id, StudentEvaluationAudienceUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
