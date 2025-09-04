using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class OrganizationsMap : Common.MigrationMap<LktblOrganization, Organization>
    {
        List<LktblOrganization> _organizations;
        public OrganizationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LktblOrganization> getSourceRecords()
        {
            _organizations = (_source as EMP_DemoContext).LktblOrganizations.ToListAsync().Result;
            return _organizations;
        }

        protected override Organization mapRecord(LktblOrganization obj)
        {
            return new Organization()
            {
                Name = obj.Oname,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _organizations.Count();
        }

        protected override void updateTarget(Organization record)
        {
            (_target as QTD2.Data.QTDContext).Organizations.Add(record);
        }
    }
}
