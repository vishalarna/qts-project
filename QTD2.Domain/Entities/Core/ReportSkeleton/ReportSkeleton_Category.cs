using QTD2.Domain.Entities.Common;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class ReportSkeleton_Category : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<ReportSkeleton_Subcategory> ReportSkeleton_Subcategories { get; set; }

        public ReportSkeleton_Category()
        {

        }

        public ReportSkeleton_Category(string name)
        {
            Name = name;
        }
    }
}
