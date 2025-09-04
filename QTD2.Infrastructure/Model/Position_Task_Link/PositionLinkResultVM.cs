using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position_Task_Link
{
    public class PositionLinkResultVM
    {
        public List<int> LinkedIds { get; set; } = new();
        public List<int> UnlinkedIds { get; set; } = new();

        public bool HasChanges => LinkedIds.Any() || UnlinkedIds.Any();
    }
}
