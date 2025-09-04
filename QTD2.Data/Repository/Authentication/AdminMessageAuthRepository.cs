using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Authentication
{
    public class AdminMessageAuthRepository : Common.Repository<Domain.Entities.Authentication.AdminMessageAuth>,Domain.Interfaces.Repository.Authentication.IAdminMessageAuthRepository
    {
        public AdminMessageAuthRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }    
}
