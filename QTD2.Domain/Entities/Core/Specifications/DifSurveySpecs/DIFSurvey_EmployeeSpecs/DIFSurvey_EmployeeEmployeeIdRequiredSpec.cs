using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.DifSurveySpecs.DIFSurvey_EmployeeSpecs
{
    public class DIFSurvey_EmployeeEmployeeIdRequiredSpec : ISpecification<DIFSurvey_Employee>
    {
        public bool IsSatisfiedBy(DIFSurvey_Employee entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
