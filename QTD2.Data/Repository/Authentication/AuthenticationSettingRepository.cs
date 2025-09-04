using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Authentication
{
    public class AuthenticationSettingRepository : Common.Repository<Domain.Entities.Authentication.AuthenticationSetting>, Domain.Interfaces.Repository.Authentication.IAuthenticationSettingRepository
    {
        public AuthenticationSettingRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
