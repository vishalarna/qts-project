using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.CBT_ScormUpload_Question_ChoiceSpecs
{
    public class CBT_ScormUpload_Question_ChoiceChoiceRequiredSpec : ISpecification<CBT_ScormUpload_Question_Choice>
    {
        public bool IsSatisfiedBy(CBT_ScormUpload_Question_Choice entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Choice);
        }
    }
}