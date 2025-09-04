using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_Employee_StatusSpecs
{
   public class DIFSurvey_Employee_StatusRequiredSpec : ISpecification<DIFSurvey_Employee_Status>
    {
        public bool IsSatisfiedBy(DIFSurvey_Employee_Status entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}
