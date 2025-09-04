using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Task_Position_LinkRepository : Common.Repository<Version_Task_Position_Link>, IVersion_Task_Position_LinkRepository
    {
        public Version_Task_Position_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
