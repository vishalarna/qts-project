using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenarioSpecs
{
    public class SimulatorScenarioTitleRequired : ISpecification<SimulatorScenario>
    {
        public bool IsSatisfiedBy(SimulatorScenario entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
