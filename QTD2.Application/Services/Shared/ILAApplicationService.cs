using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QTD2.Infrastructure.HttpClients;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_Cbt_ScormUpload;
using QTD2.Infrastructure.Rustici.EngineApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class ILAApplicationService: IILAApplicationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly ScormEngineService _scormHttpClient;
        private readonly IILAService _ilaService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IScormUploadService _scormUploadService;

        public ILAApplicationService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService,
            ScormEngineService scormHttpClient, IILAService ilaService,
            QTD2.Domain.Interfaces.Service.Core.IScormUploadService scormUploadService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _scormHttpClient = scormHttpClient;
            _ilaService = ilaService;
            _scormUploadService = scormUploadService;
        }

        

    }
}
