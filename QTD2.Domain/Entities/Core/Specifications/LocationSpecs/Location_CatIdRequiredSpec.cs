using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.LocationSpecs
{
    public class Location_CatIdRequiredSpec : ISpecification<Location>
    {
        public bool IsSatisfiedBy (Location entity, params object[] args)
        {
            return entity.LocCategoryID > 0;
        }
    }
}
