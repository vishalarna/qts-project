using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.QtdUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QTDUserController : ControllerBase
    {
        private readonly IQTDService _qTDService;
        public QTDUserController(IQTDService qTDService)
        {
            _qTDService = qTDService;
        }

        [HttpGet]
        [Route("/qtdUsers")]
        public async Task<IActionResult> GetAllActiveAsync()
        {
            var result = await _qTDService.GetAllActiveAsync();
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/qtdUsers")]
        public async Task<IActionResult> CreateAsync(QtdUserVM qtdUserOption)
        {
            var qtdUser = await _qTDService.CreateAsync(qtdUserOption);
            return Ok( new { qtdUser });
        }

    }
}
