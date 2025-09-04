using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.StudentEvaluationForm;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationFormService
    {
        public Task<List<StudentEvaluationForm>> GetAsync();

        public Task<StudentEvaluationForm> GetAsync(int id);

        public Task<StudentEvaluationForm> CreateAsync(StudentEvaluationFormCreateOptions options);

        public Task<StudentEvaluationForm> UpdateAsync(int id, StudentEvaluationFormUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
