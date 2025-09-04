using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Admin
{
    public class ILAController : Controller
    {
        IDbContextBuilder _dbContextBuilder;
        QtdAuthenticationService _authService;

        public ILAController(
            IDbContextBuilder dbContextBuilder,
             QtdAuthenticationService authService
            )
        {
            _dbContextBuilder = dbContextBuilder;
            _authService = authService;
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("admin/ila/updateObjectiveOrders/{instance}")]
        public async Task<IActionResult> UpdateObjectiveOrdersAsync(string instance)
        {
            var instanceSettings = await _authService.Instances.GetInstanceSettingsAsync(instance);
            var context = _dbContextBuilder.BuildQtdContext(instanceSettings.DatabaseName);

            var ilas = await context.ILAs.ToListAsync();
            var updatedIlas = new System.Collections.Generic.List<ILA>();

            foreach (var ila2 in ilas)
            {
                var ila =  context.ILAs.Where(r => r.Id == ila2.Id)
                            .Include("ILA_Segment_Links.Segment.SegmentObjective_Links")
                            .Include("ILA_TaskObjective_Links")
                            .Include("ILA_EnablingObjective_Links")
                            .Include("ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Topic")
                            .Include("ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_SubCategory")
                            .Include("ILACustomObjective_Links.CustomEnablingObjective.EnablingObjective_Category")
                            .First();

                var taskids = ila.ILA_TaskObjective_Links.Select(itol => itol.TaskId).Distinct().ToList();
                var tasks = await context.Tasks.Where(t => taskids.Contains(t.Id)).Include("SubdutyArea.DutyArea").ToListAsync();
                foreach (var ila_TaskObjective_Link in ila.ILA_TaskObjective_Links)
                {
                    ila_TaskObjective_Link.Task = tasks.First(t => t.Id == ila_TaskObjective_Link.TaskId);
                }

                var enablingObjectiveIds = ila.ILA_EnablingObjective_Links.Select(ieol => ieol.EnablingObjectiveId).Distinct().ToList();
                var enablingObjectives = await context.EnablingObjectives.Where(eo => enablingObjectiveIds.Contains(eo.Id))
                    .Include("EnablingObjective_Topic")
                    .Include("EnablingObjective_SubCategory")
                    .Include("EnablingObjective_Category")
                    .ToListAsync();
                foreach (var ila_EnablingObjective_Link in ila.ILA_EnablingObjective_Links)
                {
                    ila_EnablingObjective_Link.EnablingObjective = enablingObjectives.First(eo => eo.Id == ila_EnablingObjective_Link.EnablingObjectiveId);
                }

                foreach (var segmentLink in ila.ILA_Segment_Links.OrderBy(r => r.DisplayOrder))
                {
                    var segment = segmentLink.Segment;

                    var objectives = segment.SegmentObjective_Links.OrderBy(r => r.Id).ToList();

                    foreach (var objective in objectives)
                    {
                        objective.Order = objectives.IndexOf(objective) + 1;
                    }
                }

                ila.OrderObjectives();
                updatedIlas.Add(ila);
            }

            context.UpdateRange(updatedIlas);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
