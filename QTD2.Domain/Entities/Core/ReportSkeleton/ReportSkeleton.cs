using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ReportSkeleton: Entity
    {
        public string DefaultTitle { get; set; }
        public virtual List<ReportSkeletonFilter> AvailableFilters { get; set; }
        public virtual List<ReportSkeletonColumn> DisplayColumns { get; set; }

        public ReportSkeletonFilter GetFilter(string name)
        {
            if (AvailableFilters == null) AvailableFilters = new List<ReportSkeletonFilter>();

            return AvailableFilters.Where(r => r.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        }
    }
}
