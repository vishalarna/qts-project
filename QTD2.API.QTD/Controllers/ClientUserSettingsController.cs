using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ClientUserSettingsController : Controller
    {
        private readonly IClientUserSettingsService _clientUserSettingsService;
        private readonly IStringLocalizer<ClientUserSettingsController> _localizer;

        public ClientUserSettingsController(
                IClientUserSettingsService clientUserSettingsService,
                IStringLocalizer<ClientUserSettingsController> localizer
            )
        {
            _clientUserSettingsService = clientUserSettingsService;
            _localizer = localizer;
        }


    }
}
