using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;


namespace QTD2.Domain.Entities.Core
{
    public class Location_History : Entity
    { 
        public Location_History (int locationID, DateTime effectivedate, string notes)
        {
            LocationId = locationID;
            EffectiveDate = effectivedate;
            Notes = notes;

        }

        public Location_History()
        {

        }

        public int LocationId { get; set; }
        public DateTime? EffectiveDate { get; set; }

        public virtual Location Location { get; set; }

        public  string Notes { get; set; }
    }
}
