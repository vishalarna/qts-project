using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.CollaboratorInvitation;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorInvitationController : Controller
    {
        private readonly ICollaboratorInvitationService _collaboratorInvitationService;
        private readonly IStringLocalizer<CollaboratorInvitationController> _localizer;

        public CollaboratorInvitationController(ICollaboratorInvitationService collaboratorInvitationService, IStringLocalizer<CollaboratorInvitationController> localizer)
        {
            _collaboratorInvitationService = collaboratorInvitationService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Collaborator Invitation
        /// </summary>
        /// <returns>Http Response Code with Collaborator Invitations</returns>
        [HttpGet]
        [Route("/collaboratorinvitation")]
        public async Task<IActionResult> GetCollaboratorInvitationAsync()
        {
            var collaboratorInvitation = await _collaboratorInvitationService.GetAsync();
            return Ok( new { collaboratorInvitation });
        }

        /// <summary>
        /// Creates a new Collaborator Invitation
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/collaboratorinvitation")]
        public async Task<IActionResult> CreateCollaboratorInvitationAsync(CollaboratorInvitationCreateOptions options)
        {
            var result = await _collaboratorInvitationService.CreateAsync(options);
            return Ok( new { message = _localizer["collaboratorInvitationCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Collaborator Invitation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Collaborator Invitation</returns>
        [HttpGet]
        [Route("/collaboratorinvitation/{id}")]
        public async Task<IActionResult> GetCollaboratorInvitationTypeAsync(int id)
        {
            var collaboratorInvitation = await _collaboratorInvitationService.GetAsync(id);
            return Ok( new { collaboratorInvitation });
        }

        /// <summary>
        /// Updates  a specific Collaborator Invitation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/collaboratorinvitation/{id}")]
        public async Task<IActionResult> UpdateCollaboratorInvitationAsync(int id, CollaboratorInvitationUpdateOptions options)
        {
            var collaboratorInvitation = await _collaboratorInvitationService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["collaboratorInvitationUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Collaborator Invitation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/collaboratorinvitation/{id}")]
        public async Task<IActionResult> DeleteCollaboratorInvitationAsync(int id, CollaboratorInvitationOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _collaboratorInvitationService.InActiveAsync(id);
                    break;
                case "active":
                    await _collaboratorInvitationService.ActiveAsync(id);
                    break;
                case "delete":
                    await _collaboratorInvitationService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"collaboratorInvitation-{options.ActionType.ToLower()}"].Value });
        }
    }
}
