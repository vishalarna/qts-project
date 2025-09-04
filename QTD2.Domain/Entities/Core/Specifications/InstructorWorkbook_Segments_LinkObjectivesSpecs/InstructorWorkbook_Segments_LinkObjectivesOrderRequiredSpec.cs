using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_Segments_LinkObjectivesSpecs
{
    public class InstructorWorkbook_Segments_LinkObjectivesOrderRequiredSpec : ISpecification<InstructorWorkbook_Segments_LinkObjectives>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_Segments_LinkObjectives entity, params object[] args)
        {
            return entity.Order > 0;
        }
    }
}
