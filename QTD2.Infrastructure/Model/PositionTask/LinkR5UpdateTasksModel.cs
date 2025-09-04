using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.PositionHistory;

namespace QTD2.Infrastructure.Model.PositionTask
{
    public class LinkR5UpdateTasksModel
    {
        public List<int> Link_TaskIds { get; set; }
        public bool UnlinkAll { get; set; }
        public Position_HistoryCreateOptions Position_HistoryCreateOptions { get; set; }
    }
}