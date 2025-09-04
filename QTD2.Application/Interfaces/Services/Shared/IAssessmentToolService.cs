using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.AssessmentTool;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IAssessmentToolService
    {
        public Task<List<AssessmentTool>> GetAsync();

        public Task<AssessmentTool> GetAsync(int id);

        public Task<AssessmentTool> CreateAsync(AssessmentToolCreateOptions options);

        public Task<AssessmentTool> UpdateAsync(int id, AssessmentToolUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
