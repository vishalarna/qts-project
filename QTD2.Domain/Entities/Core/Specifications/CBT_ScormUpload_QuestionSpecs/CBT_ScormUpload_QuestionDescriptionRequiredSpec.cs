using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.CBT_ScormUpload_QuestionSpecs
{
    public class CBT_ScormUpload_QuestionDescriptionRequiredSpec : ISpecification<CBT_ScormUpload_Question>
    {
        public bool IsSatisfiedBy(CBT_ScormUpload_Question entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
