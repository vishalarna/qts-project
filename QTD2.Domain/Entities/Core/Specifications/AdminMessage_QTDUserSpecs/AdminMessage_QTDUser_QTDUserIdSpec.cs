using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.AdminMessage_QTDUserSpecs
{
    public class AdminMessage_QTDUser_QTDUserIdSpec : ISpecification<AdminMessage_QTDUser>
    {
        public bool IsSatisfiedBy(AdminMessage_QTDUser entity, params object[] args)
        {
            return entity.QTDUserId > 0 ;
        }
    }
}
