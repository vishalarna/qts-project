using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Authentication
{
    public class AuthenticationSettingService : Common.Service<AuthenticationSetting>, Interfaces.Service.Authentication.IAuthenticationSettingService
    {
        public AuthenticationSettingService(
                Interfaces.Repository.Authentication.IAuthenticationSettingRepository authenticationSettingRepository,
                Interfaces.Validation.Authentication.IAuthenticationSettingValidation validation)
            : base(authenticationSettingRepository, validation)
        {
        }
    }
}
