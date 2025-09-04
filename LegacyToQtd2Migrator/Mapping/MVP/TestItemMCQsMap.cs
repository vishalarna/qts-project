using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestItemMCQsMap : Common.MigrationMap<TblTestitemDistractor, TestItemMCQ>
    {
        List<TblTestitemDistractor> _testItemMcq;

        public TestItemMCQsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestitemDistractor> getSourceRecords()
        {
            _testItemMcq = (_source as EMP_DemoContext).TblTestitemDistractors.ToListAsync().Result;
            return _testItemMcq;
        }

        protected override TestItemMCQ mapRecord(TblTestitemDistractor obj)
        {
            return new TestItemMCQ()
            {
                Active = true,
                TestItemId=obj.TestItemId??-1,
                //ChoiceDescription,
                //IsCorrect,
                //Number
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testItemMcq.Count();
        }

        protected override void updateTarget(TestItemMCQ record)
        {
            (_target as QTD2.Data.QTDContext).TestItemMCQs.Add(record);
        }
    }
}
