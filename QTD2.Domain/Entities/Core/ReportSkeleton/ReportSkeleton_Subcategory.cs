using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ReportSkeleton_Subcategory : Entity
    {
        public string Name { get; set; }
        public int ReportSkeleton_CategoryId { get; set; }
        public int Order { get; set; }
        public virtual ReportSkeleton_Category ReportSkeleton_Category  { get; set; }
        public virtual List<ReportSkeleton_Subcategory_Report> ReportSkeleton_Subcategory_Reports { get; set; }

        public ReportSkeleton_Subcategory()
        {

        }

        public ReportSkeleton_Subcategory(string name,int reportSkeleton_CategoryId)
        {
            Name = name;
            ReportSkeleton_CategoryId = reportSkeleton_CategoryId;
        }
    }
}
