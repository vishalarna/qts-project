using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Evaluation_RosterSpecs
{
    public class ClassSchedule_Evaluation_RosterSpec : ISpecification<ClassSchedule_Evaluation_Roster>
    {
        public bool IsSatisfiedBy(ClassSchedule_Evaluation_Roster entity, params object[] args)
        {
            return true;
        }
    }
}
