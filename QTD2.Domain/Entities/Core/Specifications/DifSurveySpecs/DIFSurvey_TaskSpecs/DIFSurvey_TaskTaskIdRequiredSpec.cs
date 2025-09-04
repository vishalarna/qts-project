using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_TaskSpecs
{
    public class DIFSurvey_TaskTaskIdRequiredSpec : ISpecification<DIFSurvey_Task>
    {
        public bool IsSatisfiedBy(DIFSurvey_Task entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
