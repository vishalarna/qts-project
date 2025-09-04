using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class StudentEvaluationQuestionService : Common.Service<StudentEvaluationQuestion>, IStudentEvaluationQuestionService
    {
        public StudentEvaluationQuestionService(IStudentEvaluationQuestionRepository repository, IStudentEvaluationQuestionValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<IEnumerable<StudentEvaluationQuestion>> GetByEvaluationFormAsync(string includeGroups)
        {
            var forms = await AllWithIncludeAsync(new[] { "StudentEvaluationForm" });
            //if(!string.IsNullOrEmpty(includeGroups))
                //forms = forms.Where(x=>x.)
            return await AllAsync();
        }
    }
}