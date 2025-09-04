using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SegmentSpecs
{
    public class SegmentTitleRequiredSpec : ISpecification<Segment>
    {
        public bool IsSatisfiedBy(Segment entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
