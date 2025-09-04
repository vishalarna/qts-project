using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Domain.Entities.Core
{
    public class Location_Category : Common.Entity
    {

        public string LocCategoryTitle { get; set; }
        public string LocCategoryDesc { get; set; }
        public string LocCategoryWebsite { get; set; }
        public DateTime EffectiveDate { get; set; }

        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

        public virtual ICollection<Location_CategoryHistory> Location_CategoryHistories { get; set; } = new List<Location_CategoryHistory>();

        public Location_Category(string title, string description, string website, DateTime effectiveDate)
        {
            LocCategoryTitle = title;
            LocCategoryDesc = description;
            LocCategoryWebsite = website;
            EffectiveDate = effectiveDate;
        }

        public Location_Category()
        {

        }

    }
}
