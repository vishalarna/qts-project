using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QTD2.Data.Repository.Core
{
    public class Version_TrainingProgram_ILA_LinkRepository : Common.Repository<Version_TrainingProgram_ILA_Link>, IVersion_TrainingProgram_ILA_LinkRepository
    {
        public Version_TrainingProgram_ILA_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}