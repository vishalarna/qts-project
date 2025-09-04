using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ReportSkeletonColumn: Entity
    {
        public int ReportSkeletonId { get; set; }
        public string ColumnName { get; set; }
    }
}
