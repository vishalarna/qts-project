using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Roster_TimeRecordSpecs
{
    public class ClassSchedule_Roster_TimeRecordEndDateTimeRequiredSpec : ISpecification<ClassSchedule_Roster_TimeRecord>
    {
        public bool IsSatisfiedBy(ClassSchedule_Roster_TimeRecord entity, params object[] args)
        {
            return entity.EndDateTime != DateTime.MaxValue;
        }
    }
}