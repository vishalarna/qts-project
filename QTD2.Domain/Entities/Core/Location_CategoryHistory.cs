using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Location_CategoryHistory : Entity
    {
        public Location_CategoryHistory (int locCategoryID, DateTime effectivedate, string notes)
        {
            LocCategoryID = locCategoryID;
            EffectiveDate = effectivedate;
            Notes = notes;
        }

        public Location_CategoryHistory()
        {

        }

        public int LocCategoryID { get; set; }
        public DateTime? EffectiveDate { get; set; }

        public string Notes { get; set; }

        public virtual Location_Category Location_Category { get; set; }
    }
}
