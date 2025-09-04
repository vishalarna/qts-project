using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;


namespace QTD2.Domain.Entities.Core.Specifications.LocationSpecs
{
    public class  Location_NumberRequiredSpec : ISpecification<Location>
    {
        public bool IsSatisfiedBy(Location entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.LocNumber);
        }
    }
}
