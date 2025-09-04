using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class LocationsMap : Common.MigrationMap<LktblLocation, Location>
    {
        List<LktblLocation> _locations;
        public LocationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LktblLocation> getSourceRecords()
        {
            _locations = (_source as EMP_DemoContext).LktblLocations.ToListAsync().Result;
            return _locations;
        }

        protected override Location mapRecord(LktblLocation obj)
        {
            var migrationCategory = (_target as QTD2.Data.QTDContext).Location_Categories.First();

            return new Location()
            {
                LocCategoryID = obj.Lcid,
                LocNumber = obj.Lcid.ToString(),
                LocName = obj.Lcdesc.ToString(),
                LocDescription = obj.Lcdesc,
                LocCity = obj.Lccity,
                LocState = obj.Lcstate,
                LocZipCode = obj.Lczip,
                LocPhone = obj.Lcphone,
                EffectiveDate = DateOnly.FromDateTime(System.DateTime.Now), 
                Deleted = false,
                Active = !obj.Inactive,
                Location_Category = migrationCategory
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _locations.Count();
        }

        protected override void updateTarget(Location record)
        {
            (_target as QTD2.Data.QTDContext).Locations.Add(record);
        }
    }
}
