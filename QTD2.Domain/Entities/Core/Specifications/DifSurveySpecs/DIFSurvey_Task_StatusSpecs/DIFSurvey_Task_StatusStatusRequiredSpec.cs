using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvery_Task_StatusSpecs
{
    public class DIFSurvey_Task_StatusStatusRequiredSpec : ISpecification<DIFSurvey_Task_Status>
    {
        public bool IsSatisfiedBy(DIFSurvey_Task_Status entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}