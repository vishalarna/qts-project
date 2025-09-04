using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IAuthenticationSettingService
    {
        public Task<AuthenticationSetting> GetAsync();
    }
}
