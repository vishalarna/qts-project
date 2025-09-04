using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.StudentEvaluationAvailability;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationAvailabilityService
    {
        public Task<List<StudentEvaluationAvailability>> GetAsync();

        public Task<StudentEvaluationAvailability> GetAsync(int id);

        public Task<StudentEvaluationAvailability> CreateAsync(StudentEvaluationAvailabilityCreateOptions options);

        public Task<StudentEvaluationAvailability> UpdateAsync(int id, StudentEvaluationAvailabilityUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
