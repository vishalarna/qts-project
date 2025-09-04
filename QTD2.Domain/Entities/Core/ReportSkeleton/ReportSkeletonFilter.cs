using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ReportSkeletonFilter: Entity
    {
        public int ReportSkeletonId { get; set; }
        public string Name { get; set; }
        public FilterPropertyTypeEnum PropertyType { get; set; }
        public FilterValueTypeEnum ValueType { get; set; }
        public DateTime MinOption { get; set; }
        public DateTime MaxOption { get; set; }
        public string FilterOption { get; set; }
        public string DefaultValue { get; set; }
        public int? MaxAllowedSelections { get; set; }
    }
}
