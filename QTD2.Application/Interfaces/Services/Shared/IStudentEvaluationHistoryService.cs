using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.StudentEvaluationHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationHistoryService
    {
        public List<StudentEvaluationHistory> GetAsync();

        public Task<StudentEvaluationHistory> GetAsync(int id);

        public Task<StudentEvaluationHistory> CreateAsync(StudentEvaluationHistoryCreateOptions options);

        public Task<StudentEvaluationHistory> UpdateAsync(int id, StudentEvaluationHistoryCreateOptions options);

        public Task<StudentEvaluationHistory> DeleteAsync(int id);

        public Task<StudentEvaluationHistory> ActiveAsync(int id);

        public Task<StudentEvaluationHistory> DeactivateAsync(int id);
    }
}
