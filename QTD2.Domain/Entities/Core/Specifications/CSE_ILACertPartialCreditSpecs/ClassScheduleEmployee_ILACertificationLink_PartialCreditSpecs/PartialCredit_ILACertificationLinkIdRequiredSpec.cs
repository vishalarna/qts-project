using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.CSE_ILACertPartialCreditSpecs.ClassScheduleEmployee_ILACertificationLink_PartialCreditSpecs
{
    public class PartialCredit_ILACertificationLinkIdRequiredSpec : ISpecification<ClassScheduleEmployee_ILACertificationLink_PartialCredit>
    {
        public bool IsSatisfiedBy(ClassScheduleEmployee_ILACertificationLink_PartialCredit entity, params object[] args)
        {
            return entity.ILACertificationLinkId > 0;
        }
    }
}
