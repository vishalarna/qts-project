using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RatingScaleSpecs
{
    public class Position1TextRequiredSpec : ISpecification<RatingScale>
    {
        public bool IsSatisfiedBy(RatingScale entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Position1Text);
        }
    }
}
