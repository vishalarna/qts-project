using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.CSE_ILACertPartialCreditSpecs.CSE_ILACertLink_SubReq_PartialCreditSpecs
{
    public class CSE_ILACertLink_SubReq_PartialCredit_CSE_ILACert_PCIdRequiredIdSpec : ISpecification<ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit>
    {
        public bool IsSatisfiedBy(ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredit entity, params object[] args)
        {
            return entity.ClassScheduleEmployee_ILACertificationLink_PartialCreditId > 0;
        }
    }
}
