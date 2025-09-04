using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILASelfRegistrationOptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        [HttpPost]
        [Route("/ila/setting/selfReg")]
        public async Task<IActionResult> CreateSelfRegistrationSetting(ILA_SelfRegistrationCreateOptions options)
        {
            var ila = (await _ilaService.GetAsync(x=>x.Id==options.ILAId)).FirstOrDefault();
            var hasSelfRegChanged = await _ilaService.GetSelfRegistrationOptionsSettingForILAAsync(options.ILAId) == null ||
                           _ilaService.HasILASelfRegistrationOptionChanges(ila.ILA_SelfRegistrationOption, options);
             await _ilaService.UpdateClassSizeAsync(options.ClassSize, options.ILAId);
            if (hasSelfRegChanged)
            {
                await _versioningService.VersionILAAsync(ila, "ILA Self Registration Setting Created", DateTime.Now, 1);
                var result = await _ilaService.CreateSelfRegistrationOptionsSettingAsync(options);
                return Ok(new { result });
            }

            return Ok();
        }

        [HttpPut]
        [Route("/ila/setting/selfReg/{id}")]
        public async Task<IActionResult> UpdateSelfRegistrationSetting(int id, ILA_SelfRegistrationCreateOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Self Registration Setting Created", DateTime.Now, 2);
            var result = await _ilaService.UpdateSelfRegistrationOptionsSettingAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Get a specific self registration option setting;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/setting/selfReg/{id}")]
        public async Task<IActionResult> GetSelfRegistrationSetting(int id)
        {
            var result = await _ilaService.GetSelfRegistrationOptionsSettingAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get specific self registration option settings for an ila
        /// </summary>
        /// <param name="ilaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ila/{ilaId}/selfReg/eval")]
        public async Task<IActionResult> GetSelfRegistrationSettingForILA(int ilaId)
        {
            var result = await _ilaService.GetSelfRegistrationOptionsSettingForILAAsync(ilaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{ilaId}/selfReg")]
        public async Task<IActionResult> GetSelfRegistrationSettingByILAId(int ilaId)
        {
            var result = await _ilaService.GetSelfRegistrationOptionsSettingByILAIdAsync(ilaId);
            return Ok(new { result });
        }
    }
}
