using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgramSpecs
{
    public class TrainingProgram_ProgramTypeRequired : ISpecification<TrainingProgram>
    {
        public bool IsSatisfiedBy(TrainingProgram entity, params object[] args)
        {
            return (entity.TrainingProgramTypeId > 0);
        }
    }
}
