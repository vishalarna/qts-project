using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_RecurrenceSpecs
{
    public class ClassSchedule_RecurrenceClassIdRequiredSpec : ISpecification<ClassSchedule_Recurrence>
    {
        public bool IsSatisfiedBy(ClassSchedule_Recurrence entity, params object[] args)
        {
            return entity.ClassId > 0;
        }
    }
}
