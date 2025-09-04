using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.ILA_TestRelease_EMPSettings;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPost]
        [Route("/ila/setting/test")]
        public async Task<IActionResult> CreateTestReleaseSettings(ILATestReleaseEmpSettingsCreateOptions option)
        {
            try
            {
                var ila = await _ilaService.GetAsync(option.ILAId);
                await _versioningService.VersionILAAsync(ila, "Test Release Settings Created", DateTime.Now, 1);
                var result = await _ilatestReleaseEmpSettingsService.CreateAsync(option);
                return Ok( new { result });
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        [HttpPut]
        [Route("/ila/setting/test/{id}")]
        public async Task<IActionResult> UpdateTestReleaseSettings(int id, ILATestReleaseEmpSettingsCreateOptions option)
        {
            var ila = await _ilaService.GetAsync(option.ILAId);
            await _versioningService.VersionILAAsync(ila, "Test Release Settings Updated Created", DateTime.Now, 2);
            var result = await _ilatestReleaseEmpSettingsService.UpdateAsync(id, option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/testSettings")]
        public async Task<IActionResult> GetTestReleaseSettings()
        {
            var result = await _ilatestReleaseEmpSettingsService.GetAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/testSettings/{id}")]
        public async Task<IActionResult> GetTestReleaseSetting(int id)
        {
            var result = await _ilatestReleaseEmpSettingsService.GetAsync(id);
            return Ok( new { result });

        }

        [HttpDelete]
        [Route("/ila/testSettings/{id}")]
        public async Task<IActionResult> DeleteTestReleaseSettings(int id)
        {
          
            await _ilatestReleaseEmpSettingsService.DeleteAsync(id);
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Test Release Settings Deleted", DateTime.Now, 0);
            return Ok( new { message = _localizer[$"Settings Deleted"].Value });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/setting/test")]
        public async Task<IActionResult> GetTestReleaseSettingsForILA(int ilaId)
        {
            var result = await _ilatestReleaseEmpSettingsService.GetTTestReleaseEMPSettingsForILAAsync(ilaId);
            return Ok( new { result });
        }
    }
}