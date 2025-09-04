using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassScheduleSpecs
{
    public class ClassScheduleIdRequiredSpec : ISpecification<ClassSchedule>
    {
        public bool IsSatisfiedBy(ClassSchedule entity, params object[] args)
        {
            return true;
        }
    }
}
