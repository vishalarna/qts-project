using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ClientUsersFromLkInstrutorsMap : Common.MigrationMap<LktblInstructor, ClientUser>
    {
        List<LktblInstructor> _instructors;
        public ClientUsersFromLkInstrutorsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<LktblInstructor> getSourceRecords()
        {
            _instructors = (_source as EMP_DemoContext).LktblInstructors.ToListAsync().Result;
            return _instructors;
        }

        protected override ClientUser mapRecord(LktblInstructor obj)
        {
            return new ClientUser()
            {
                //PersonId ,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _instructors.Count();
        }

        protected override void updateTarget(ClientUser record)
        {
            (_target as QTD2.Data.QTDContext).ClientUsers.Add(record);
        }

    }
}
