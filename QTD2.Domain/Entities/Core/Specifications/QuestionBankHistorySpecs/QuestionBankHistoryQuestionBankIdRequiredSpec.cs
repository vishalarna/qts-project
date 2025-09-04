using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.QuestionBankHistorySpecs
{
    public class QuestionBankHistoryQuestionBankIdRequiredSpec : ISpecification<QuestionBankHistory>
    {
        public bool IsSatisfiedBy(QuestionBankHistory entity, params object[] args)
        {
            return entity.QuestionBankId > 0;
        }
    }
}