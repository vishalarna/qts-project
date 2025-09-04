using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_StudentEvaluations_LinkSpecs
{
    public class ClassSchedule_StudentEvaluationStuduentEvaluationIdRequiredSpec : ISpecification<ClassSchedule_StudentEvaluations_Link>
    {
        public bool IsSatisfiedBy(ClassSchedule_StudentEvaluations_Link entity, params object[] args)
        {
            return entity.StudentEvaluationId > 0;
        }
    }
}
