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
    public class StudentEvaluation_QuestionService : Common.Service<StudentEvaluation_Question>, IStudentEvaluation_QuestionService
    {
        public StudentEvaluation_QuestionService(IStudentEvaluation_QuestionRepository repository, IStudentEvaluation_QuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}