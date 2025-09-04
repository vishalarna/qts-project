using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_Employee_ResponseSpecs
{
    public class DIFSurvey_Employee_ResponseDIFSurvey_TaskIdRequiredSpec : ISpecification<DIFSurvey_Employee_Response>
    {
        public bool IsSatisfiedBy(DIFSurvey_Employee_Response entity, params object[] args)
        {
            return entity.DIFSurvey_TaskId > 0;
        }
    }
}