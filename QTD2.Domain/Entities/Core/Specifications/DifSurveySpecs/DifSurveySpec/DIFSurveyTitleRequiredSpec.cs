using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurvey
{
   public class DIFSurveyTitleRequiredSpec : ISpecification<DIFSurvey>
    {
        public bool IsSatisfiedBy(DIFSurvey entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}