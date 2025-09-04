using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_TasksSpecs
{
    public class InstructorWorkbook_ILADesign_TasksValidationILAobjectiveorderRequiredSpecs : ISpecification<InstructorWorkbook_ILADesign_Tasks>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILADesign_Tasks entity, params object[] args)
        {
            return entity.ILAObjectiveOrder > 0;
        }
    }
}
