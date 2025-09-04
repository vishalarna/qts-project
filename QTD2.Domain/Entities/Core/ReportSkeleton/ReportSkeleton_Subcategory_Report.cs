using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ReportSkeleton_Subcategory_Report : Entity
    {
        public int ReportSkeleton_SubcategoryId { get; set; }
        public virtual ReportSkeleton_Subcategory ReportSkeleton_Subcategory { get; set; }
        public int ReportSkeletonId { get; set; }
        public int Order { get; set; }
        public virtual ReportSkeleton ReportSkeleton { get; set; }

        public ReportSkeleton_Subcategory_Report()
        {

        }

        public ReportSkeleton_Subcategory_Report(int reportSkeleton_SubcategoryId,int reportSkeletonId)
        {
            ReportSkeleton_SubcategoryId = reportSkeleton_SubcategoryId;
            ReportSkeletonId = reportSkeletonId;
        }
    }
}
