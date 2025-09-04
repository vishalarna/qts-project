using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IStudentEvaluationQuestionService : Common.IService<StudentEvaluationQuestion>
    {
        public System.Threading.Tasks.Task<IEnumerable<StudentEvaluationQuestion>> GetByEvaluationFormAsync(string includeGroups);
    }
}
