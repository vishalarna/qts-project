using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvery_DevStatusSpecs
{
    public class DIFSurvey_DevStatusRequiredSpec : ISpecification<DIFSurvey_DevStatus>
    {
        public bool IsSatisfiedBy(DIFSurvey_DevStatus entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}
