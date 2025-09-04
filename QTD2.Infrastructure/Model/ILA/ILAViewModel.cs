using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILAEntity = QTD2.Domain.Entities.Core.ILA;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAViewModel
    {
        public ILAEntity ila { get; set; }

        public List<Domain.Entities.Core.Position> positions { get; set; }
    }
}
