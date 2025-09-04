using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Version_TrainingProgramSpecs
{
    public class Version_TrainingProgram_TrainingProgramIdRequiredSpec : ISpecification<Version_TrainingProgram>
    {
        public bool IsSatisfiedBy(Version_TrainingProgram entity, params object[] args)
        {
            return entity.TrainingProgramId > 0;
        }
    }
}
