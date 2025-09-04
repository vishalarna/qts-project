using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TainingProgram_ILA_LinkSpecs
{
    public class TrainingProgram_ILA_LinkILAIdRequiredSpec : ISpecification<TrainingPrograms_ILA_Link>
    {
        public bool IsSatisfiedBy(TrainingPrograms_ILA_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
