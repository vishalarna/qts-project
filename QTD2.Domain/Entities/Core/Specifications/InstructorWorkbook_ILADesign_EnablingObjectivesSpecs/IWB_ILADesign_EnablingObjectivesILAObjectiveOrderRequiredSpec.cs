using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_EnablingObjectivesSpecs
{
    public class IWB_ILADesign_EnablingObjectivesILAObjectiveOrderRequiredSpec : ISpecification<InstructorWorkbook_ILADesign_EnablingObjectives>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_EnablingObjectives entity, params object[] args)
        {
            return entity.ILAObjectiveOrder > 0;
        }
    }
}
