namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_TopicSpecs
{
    public class EO_TopicSubCategoryIdRequiredSpec : Interfaces.Specification.ISpecification<EnablingObjective_Topic>
    {
        public bool IsSatisfiedBy(EnablingObjective_Topic entity, params object[] args)
        {
            return entity.SubCategoryId > 0;
        }
    }
}
