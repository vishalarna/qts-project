using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILACustomObjective_LinkService : Common.IService<ILACustomObjective_Link>
    {
        public System.Threading.Tasks.Task<List<ILACustomObjective_Link>> GetILA_CustomObjective_LinksByILAId(int ilaId);
    }
}
