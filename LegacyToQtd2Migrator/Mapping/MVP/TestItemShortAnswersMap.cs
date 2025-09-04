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
    public class TestItemShortAnswersMap : Common.MigrationMap<TblTestitemDistractor, TestItemShortAnswer>
    {
        List<TblTestitemDistractor> _testItemShortAnswers;

        public TestItemShortAnswersMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestitemDistractor> getSourceRecords()
        {
            _testItemShortAnswers = (_source as EMP_DemoContext).TblTestitemDistractors.ToListAsync().Result;
            return _testItemShortAnswers;
        }

        protected override TestItemShortAnswer mapRecord(TblTestitemDistractor obj)
        {
            return new TestItemShortAnswer()
            {
                Active = true,
                TestItemId=obj.TestItemId??-1,
                //Responses,
                //IsCaseSensitive,
                //AcceptableResponses,
                //Number
                

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testItemShortAnswers.Count();
        }

        protected override void updateTarget(TestItemShortAnswer record)
        {
            (_target as QTD2.Data.QTDContext).TestItemShortAnswers.Add(record);
        }
    }
}
