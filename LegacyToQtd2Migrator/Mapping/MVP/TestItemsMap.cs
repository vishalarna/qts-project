using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestItemsMap : Common.MigrationMap<TblTestItem, TestItem>
    {
        List<TblTestItem> _testItem;

        public TestItemsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestItem> getSourceRecords()
        {
            _testItem = (_source as EMP_DemoContext).TblTestItems.ToListAsync().Result;
            return _testItem;
        }

        protected override TestItem mapRecord(TblTestItem obj)
        {
            return new TestItem()
            {
                Active = true,
                TestItemTypeId=obj.TestItemType??-1,
                TaxonomyId=obj.TaxonomyId??-1,
                //EOId,
                //Description,
                //Image,
                //Number
                //TestItemTrueFalses= getTestItemTrueFalses(),
                //TestItemShortAnswers= getTestItemShortAnswers(),
                //TestItemMCQs= getTestItemMCQs(),
                //TestItemMatches= getTestItemMatches(),
                //TestItemFillBlanks= getTestItemFillBlanks(),
                //Test_Item_Links= getTest_Item_Links(),
            };
        }
        private ICollection<TestItemTrueFalse> getTestItemTrueFalses()
        {
            List<TestItemTrueFalse> testItemTrueFalses = new List<TestItemTrueFalse>();

            testItemTrueFalses.Add(new TestItemTrueFalse()
            {
                Active = true,
                Deleted = false,
                //TestItemId,
                //Choices,
                //IsCorrect
            });

            return testItemTrueFalses;
        }

        private ICollection<TestItemShortAnswer> getTestItemShortAnswers()
        {
            List<TestItemShortAnswer> testItemShortAnswers = new List<TestItemShortAnswer>();

            testItemShortAnswers.Add(new TestItemShortAnswer()
            {
                Active = true,
                Deleted = false,
                //TestItemId,
                //Responses,
                //IsCaseSensitive,
                //AcceptableResponses,
                //Number
            });

            return testItemShortAnswers;
        }
        private ICollection<TestItemMatch> getTestItemMatches()
        {
            List<TestItemMatch> testItemMatches = new List<TestItemMatch>();

            testItemMatches.Add(new TestItemMatch()
            {
                Active = true,
                Deleted = false,
                //TestItemId ,
                //ChoiceDescription,
                //MatchDescription,
                //MatchValue,
                //CorrectValue,
                //Number
            });

            return testItemMatches;
        }

        private ICollection<TestItemMCQ> getTestItemMCQs()
        {
            List<TestItemMCQ> testItemMCQs = new List<TestItemMCQ>();

            testItemMCQs.Add(new TestItemMCQ()
            {
                Active = true,
                Deleted = false,
                //TestItemId
                //ChoiceDescription,
                //IsCorrect,
                //Number

            });

            return testItemMCQs;
        }

        private ICollection<TestItemFillBlank> getTestItemFillBlanks()
        {
            List<TestItemFillBlank> testItemFillBlanks = new List<TestItemFillBlank>();

            testItemFillBlanks.Add(new TestItemFillBlank()
            {
                Active = true,
                Deleted = false,
                //TestItemId,
                //CorrectIndex,
                //Correct   
            });

            return testItemFillBlanks;
        }

        private ICollection<Test_Item_Link> getTest_Item_Links()
        {
            List<Test_Item_Link> test_Item_Links = new List<Test_Item_Link>();

            test_Item_Links.Add(new Test_Item_Link()
            {
                Active = true,
                Deleted = false,
               // TestItemId,
                //TestId,
                //Sequenc
            });

            return test_Item_Links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testItem.Count();
        }

        protected override void updateTarget(TestItem record)
        {
            (_target as QTD2.Data.QTDContext).TestItems.Add(record);
        }
    }
}
