using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.StudentEvaluationQuestion;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationQuestionService
    {
        public Task<List<StudentEvaluationQuestion>> GetAsync();

        public Task<StudentEvaluationQuestion> GetAsync(int id);

        public Task<List<QuestionBank>> GetStudentEvalQuestionsForEvalAsync(int id);

        public Task<StudentEvaluationQuestion> CreateAsync(StudentEvaluationQuestionCreateOptions options);

        public Task<StudentEvaluationQuestion> UpdateAsync(int id, StudentEvaluationQuestionUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
