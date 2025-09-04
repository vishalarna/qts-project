
using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_DesignSpecs
{
   public  class InstructorWorkbook_ILA_DesignILAIdRequiredSpecs : ISpecification<InstructorWorkbook_ILA_Design>
    {
        public bool IsSatisfiedBy(InstructorWorkbook_ILA_Design entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
