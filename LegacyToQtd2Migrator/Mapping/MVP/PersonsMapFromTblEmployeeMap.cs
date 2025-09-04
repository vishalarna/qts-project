using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class PersonsMapFromTblEmployeeMap : Common.MigrationMap<TblEmployee, Person>
    {
        List<TblEmployee> _persons;

        public PersonsMapFromTblEmployeeMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblEmployee> getSourceRecords()
        {
            _persons = (_source as EMP_DemoContext).TblEmployees.ToListAsync().Result;
            return _persons;
        }

        protected override Person mapRecord(TblEmployee obj)
        {
            return new Person()
            {
                Active = true,
                FirstName = obj.EfirstName,
                LastName = obj.ElastName,
                Username = obj.UserName,
                Deleted = false,
                //Image,
                //MiddleName
        
            };
        }


        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _persons.Count();
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
