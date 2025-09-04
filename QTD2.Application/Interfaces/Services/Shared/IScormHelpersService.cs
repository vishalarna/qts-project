using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Rustici.EngineApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IScormHelpersService
    {
        CBT_ScormRegistration ProcessRuntimeInteraction(List<RuntimeInteractionSchema> runtimeInteractions, CBT_ScormRegistration registration, CBT_ScormUpload cbtScormUpload);
    }
}
