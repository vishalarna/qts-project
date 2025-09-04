using QTD2.Domain.Interfaces.Specification;


namespace QTD2.Domain.Entities.Core.Specifications.EmpTestSpecs
{
    public class EmpTest_TestTypeIdRequiredSpec : ISpecification<ClassSchedule_Roster_Response_Selection>
    {
        public bool IsSatisfiedBy(ClassSchedule_Roster_Response_Selection entity, params object[] args)
        {
            return true;
        }
    }
}
