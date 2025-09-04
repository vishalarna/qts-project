using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using QTD2.Infrastructure.Model.Nerc;
using Microsoft.AspNetCore.Http;

namespace QTD2.API.QTD.Controllers
{
    public partial class NercController
    {
        [HttpPost]
        [Route("/nerc/cehupload")]
        public async Task<IActionResult> GetCehUploadAsync(CehUploadGetOptions options)
        {
            var result  = await _nercService.GetCehUploadAsync(options);
            return Ok( new { result});
        }
    }
}
