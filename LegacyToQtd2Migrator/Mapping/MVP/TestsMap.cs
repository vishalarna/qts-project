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
    public class TestsMap : Common.MigrationMap<TblTest, Test>
    {
        List<TblTest> _test;

        public TestsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTest> getSourceRecords()
        {
            _test = (_source as EMP_DemoContext).TblTests.ToListAsync().Result;
            return _test;
        }

        protected override Test mapRecord(TblTest obj)
        {
            return new Test()
            {
                Active = true,
                TestStatusId=obj.TestStatusId??-1,
                TestTitle=obj.TestTitle,
                //IsPublished,
                //RandomizeDistractors,
                //EffectiveDate

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _test.Count();
        }

        protected override void updateTarget(Test record)
        {
            (_target as QTD2.Data.QTDContext).Tests.Add(record);
        }
    }
}
