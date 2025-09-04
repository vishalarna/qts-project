using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgramSpecs
{
    public class TrainingProgramIdRequiredSpec : ISpecification<TrainingProgramType>
    {
        public bool IsSatisfiedBy(TrainingProgramType entity, params object[] args)
        {
            return entity.Id > 0;
        }
    }
}
