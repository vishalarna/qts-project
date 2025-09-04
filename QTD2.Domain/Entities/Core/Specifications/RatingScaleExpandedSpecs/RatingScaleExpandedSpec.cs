using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.RatingScaleExpandedSpecs
{
    public class RatingScaleExpandedSpec : ISpecification<RatingScaleExpanded>
    {
        public bool IsSatisfiedBy(RatingScaleExpanded entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
