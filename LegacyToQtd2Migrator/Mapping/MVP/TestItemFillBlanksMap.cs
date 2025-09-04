using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestItemFillBlanksMap : Common.MigrationMap<TblTestitemDistractor, TestItemFillBlank>
    {
        List<TblTestitemDistractor> _tblTestItemFillBlankDistractor;

        public TestItemFillBlanksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestitemDistractor> getSourceRecords()
        {
            _tblTestItemFillBlankDistractor = (_source as EMP_DemoContext).TblTestitemDistractors.ToListAsync().Result;
            return _tblTestItemFillBlankDistractor;
        }

        protected override TestItemFillBlank mapRecord(TblTestitemDistractor obj)
        {
            return new TestItemFillBlank()
            {
                Active = true,
                TestItemId=obj.TestItemId??=-1,
                //CorrectIndex,
                //Correct   
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTestItemFillBlankDistractor.Count();
        }

        protected override void updateTarget(TestItemFillBlank record)
        {
            (_target as QTD2.Data.QTDContext).TestItemFillBlanks.Add(record);
        }
    }
}
