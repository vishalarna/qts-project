using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjectiveSpecs
{
    public class Version_EO_SubCatIdRequiredSpec : ISpecification<Version_EnablingObjective>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective entity, params object[] args)
        {
            return entity.SubCategoryId > 0;
        }
    }
}
