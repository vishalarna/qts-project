using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_SaftyHazardSpecs
{
    public class VSH_PersonalProtectiveEquipmentRequiredSpec : ISpecification<Version_SaftyHazard>
    {
        public bool IsSatisfiedBy(Version_SaftyHazard entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.PersonalProtectiveEquipment);
        }
    }
}
