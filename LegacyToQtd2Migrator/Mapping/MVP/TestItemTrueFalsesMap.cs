using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestItemTrueFalsesMap : Common.MigrationMap<TblTestitemDistractor, TestItemTrueFalse>
    {
        List<TblTestitemDistractor> _testItemTrueFalse;

        public TestItemTrueFalsesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestitemDistractor> getSourceRecords()
        {
            _testItemTrueFalse = (_source as EMP_DemoContext).TblTestitemDistractors.ToListAsync().Result;
            return _testItemTrueFalse;
        }

        protected override TestItemTrueFalse mapRecord(TblTestitemDistractor obj)
        {
            return new TestItemTrueFalse()
            {
                Active = true,
                TestItemId = obj.TestItemId ?? -1,
                //Choices,
                //IsCorrect

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testItemTrueFalse.Count();
        }

        protected override void updateTarget(TestItemTrueFalse record)
        {
            (_target as QTD2.Data.QTDContext).TestItemTrueFalses.Add(record);
        }
    }
}
