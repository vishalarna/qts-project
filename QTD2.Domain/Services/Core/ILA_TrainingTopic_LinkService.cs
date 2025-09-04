using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ILA_TrainingTopic_LinkService : Common.Service<ILA_TrainingTopic_Link>, IILA_TrainingTopic_LinkService
    {
        public ILA_TrainingTopic_LinkService(IILA_TrainingTopic_LinkRepository repository, IILA_TrainingTopic_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILA_TrainingTopic_Link>> GetLinksForActiveTrTopics(int ilaId)
        {
            var trLinks = await FindWithIncludeAsync(x => x.ILAId == ilaId && x.TrainingTopic.Active == true && x.Active == true,new string[] { "TrainingTopic" });
            return trLinks.ToList();
        }
    }
}
