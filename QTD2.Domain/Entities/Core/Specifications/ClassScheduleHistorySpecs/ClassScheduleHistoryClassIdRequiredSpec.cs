using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassScheduleHistorySpecs
{
    public class ClassScheduleHistoryClassIdRequiredSpec : ISpecification<ClassScheduleHistory>
    {
        public bool IsSatisfiedBy(ClassScheduleHistory entity, params object[] args)
        {
            return entity.ClassScheduleID > 0;
        }
    }
}
