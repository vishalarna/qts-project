using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.QTDUserSpecs
{
    public class QTDUser_PersonIdRequiredSpecs : ISpecification<QTDUser>
    {
        public bool IsSatisfiedBy(QTDUser entity, params object[] args)
        {
            return entity.PersonId > 0;
        }
    }
}
