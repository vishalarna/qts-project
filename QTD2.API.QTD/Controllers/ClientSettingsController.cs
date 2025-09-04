using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Jobs.Notifications;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.Certification_History;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ClientSettingsController : Controller
    {
        private readonly IClientSettingsService _clientSettingsService;
        private readonly IStringLocalizer<ClientSettingsController> _localizer;

        public ClientSettingsController(
                IClientSettingsService clientSettingsService, 
                IStringLocalizer<ClientSettingsController> localizer
            )
        {
            _clientSettingsService = clientSettingsService;
            _localizer = localizer;
        }
    }
}
