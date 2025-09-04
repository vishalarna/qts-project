using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.QTD2ScormContext;

namespace LegacyToQtd2Migrator.Mapping.Scorm
{
    public class ILAPopulateCbtMap : Common.MigrationMap<Cbt, Ila>
    {
        List<Cbt> _cbts;

        public ILAPopulateCbtMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<Cbt> getSourceRecords()
        {
            _cbts = (_target as MigrationTestContext).Cbts.Include("Ila").Where(r => r.Active).ToList();

            return _cbts;
        }

        protected override Ila mapRecord(Cbt obj)
        {
            var ila = obj.Ila;

            ila.CbtrequiredForCourse = true;
            ila.UseForEmp = true;

            return ila;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _cbts.Count();
        }

        protected override void updateTarget(Ila record)
        {
            (_target as MigrationTestContext).Ilas.Update(record);
        }
    }
}
