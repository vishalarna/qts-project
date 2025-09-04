using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_RosterSpecs
{
    public class ClassSchedule_RosterEmpIdRequiredSpec : ISpecification<ClassSchedule_Roster>
    {
        public bool IsSatisfiedBy(ClassSchedule_Roster entity, params object[] args)
        {
            return entity.EmpId > 0;
        }
    }
}
