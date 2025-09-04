using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Location : Common.Entity
    {
        public int LocCategoryID { get; set; }
        public string LocNumber { get; set; }
        public string LocName { get; set; }
        public string LocDescription { get; set; }
        public string LocAddress { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string LocZipCode { get; set; }
        public string LocPhone { get; set; }
        public DateOnly EffectiveDate { get; set; }

        public virtual Location_Category Location_Category { get; set; }

        public virtual ICollection<Location_History> Location_Histories { get; set; } = new List<Location_History>();

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
        public Location (int loccatid, string num, string name, string description, string address, string city, string state, string zipcode, string phone, DateOnly effectiveDate)
        {
            LocCategoryID = loccatid;
            LocNumber = num;
            LocName = name;
            LocDescription = description;
            LocAddress = address;
            LocCity = city;
            LocState = state;
            LocZipCode = zipcode;
            LocPhone = phone;
            EffectiveDate = effectiveDate;
        }

        public Location()
        {

        }
    }
}
