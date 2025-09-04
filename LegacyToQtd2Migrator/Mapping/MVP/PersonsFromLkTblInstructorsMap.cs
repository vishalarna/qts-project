using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class PersonsFromLkTblInstructorsMap : Common.MigrationMap<LktblInstructor, Person> 
    {
        List<LktblInstructor> _instructors;

        public PersonsFromLkTblInstructorsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LktblInstructor> getSourceRecords()
        {
            _instructors = (_source as EMP_DemoContext).LktblInstructors.ToListAsync().Result;
            return _instructors;
        }

        protected override Person mapRecord(LktblInstructor obj)
        {
            return new Person()
            {
                Active = true,
                Deleted = false,
                //FirstName
                //LastName 
                //Username 
                //Image,
                //MiddleName

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _instructors.Count();
        }

        protected override void updateTarget(Person record)
        {
            if (!string.IsNullOrEmpty(record.Username))
            {
                (_target as QTD2.Data.QTDContext).Persons.Add(record);
            }
        }
    }
}
