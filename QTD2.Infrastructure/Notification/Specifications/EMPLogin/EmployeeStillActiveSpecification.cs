using QTD2.Infrastructure.Notification.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Specifications.EMPLogin
{
    public class EMPLogin_EmployeeStillActiveSpecification : Domain.Interfaces.Specification.ISpecification<EMPLoginModel>
    {
        public bool IsSatisfiedBy(EMPLoginModel entity, params object[] args)
        {
            return true;
        }
    }
}
