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
    public class Version_TestRepository : Common.Repository<Version_Test>, IVersion_TestRepository
    {
        public Version_TestRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
