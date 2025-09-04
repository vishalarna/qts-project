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
    public class ILA_UploadRepository : Common.Repository<ILA_Upload>, IILA_UploadRepository
    {
        public ILA_UploadRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
