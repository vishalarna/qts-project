using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgramSpecs
{
    public class TrainingProgramPositionIdRequiredSpec : ISpecification<TrainingProgram>
    {
        public bool IsSatisfiedBy(TrainingProgram entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
