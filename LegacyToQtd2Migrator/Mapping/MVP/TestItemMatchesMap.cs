using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestItemMatchesMap : Common.MigrationMap<TblTestitemDistractor, TestItemMatch>
    {
        List<TblTestitemDistractor> _tblTestItemMatchesDistractor;

        public TestItemMatchesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestitemDistractor> getSourceRecords()
        {
            _tblTestItemMatchesDistractor = (_source as EMP_DemoContext).TblTestitemDistractors.ToListAsync().Result;
            return _tblTestItemMatchesDistractor;
        }

        protected override TestItemMatch mapRecord(TblTestitemDistractor obj)
        {
            return new TestItemMatch()
            {
                Active = true,
                TestItemId = obj.TestItemId ??= -1,
                //ChoiceDescription,
                //MatchDescription,
                //MatchValue,
                //CorrectValue,
                //Number
               
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTestItemMatchesDistractor.Count();
        }

        protected override void updateTarget(TestItemMatch record)
        {
            (_target as QTD2.Data.QTDContext).TestItemMatches.Add(record);
        }
    }
}
