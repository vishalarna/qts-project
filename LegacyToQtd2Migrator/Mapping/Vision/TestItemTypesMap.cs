using LegacyToQtd2Migrator.Helpers;
using LegacyToQtd2Migrator.Legacy.Data;
using LegacyToQtd2Migrator.Vision.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class SourceTestItemType
    {
        public SourceTestItemType(int testItemType)
        {
            TestItemTypeId = testItemType;
        }

        public int TestItemTypeId { get; set; }
    }

    public class TestItemTypesMap : Common.MigrationMap<SourceTestItemType, TestItemType>
    {
        List<SourceTestItemType> _testItemTypes;
        List<QuestionImpl> _sourceTestItems;
        List<QuestionImpl> _sourceTestItemHtmls;
        int _projectId;


        public TestItemTypesMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }

        protected override List<SourceTestItemType> getSourceRecords()
        {
            _testItemTypes = new List<SourceTestItemType>()
            {
                new SourceTestItemType(0),
                 new SourceTestItemType(1),
                new SourceTestItemType(2),
                new SourceTestItemType(3),
                new SourceTestItemType(4),
                new SourceTestItemType(5),
                new SourceTestItemType(6),
                new SourceTestItemType(7)
            };

            _sourceTestItems = (_source as VisionContext).QuestionImpls
                    .Include("FkQuestionNavigation.QuestionStems")
                    .Include("FkQuestionNavigation.QuestionFis")
                    .Include("FkQuestionNavigation.QuestionEs")
                    .Include("FkQuestionNavigation.QuestionSas")
                    .Include("FkQuestionNavigation.QuestionMaChoices")
                    .Include("FkQuestionNavigation.QuestionMaItems")
                    .Include("FkQuestionNavigation.QuestionTfs")
                    .Include("FkQuestionNavigation.QuestionMcChoices")
                .Where(r => r.FkExpiredBy == null && r.FkProject == _projectId).ToList();

            return _testItemTypes;
        }

        protected override TestItemType mapRecord(SourceTestItemType obj)
        {
            var testItemType = (_target as QTD2.Data.QTDContext).TestItemTypes.Where(r => r.Description == getQtd2TestItemTypeName(obj.TestItemTypeId)).FirstOrDefault();

            if (testItemType == null) return null;

            testItemType.TestItems = getTestItems(obj);

            return testItemType;
        }

        private ICollection<TestItem> getTestItems(SourceTestItemType testItemType)
        {
            List<TestItem> testItems = new List<TestItem>();
            var sourceItems = _sourceTestItems.Where(r => r.Type == testItemType.TestItemTypeId);

            foreach (var sourceItem in sourceItems)
            {
                var targetTaxonomy = (_target as QTD2.Data.QTDContext).TaxonomyLevels.Where(r => r.Description == "Recall").First();

                var sourceStemItem = sourceItem.FkQuestionNavigation.QuestionStems.Where(r => r.FkExpiredBy == null).FirstOrDefault();

                if (sourceStemItem == null) continue;

                var stem = sourceStemItem.Stem.RtfToHtml();

                testItems.Add(new TestItem()
                {
                    Active = true,
                    Deleted = false,
                    IsActive = true,
                    Description = getQtd2TestItemTypeName(testItemType.TestItemTypeId) == "Fill in the Blank" ? getFillInTheBlankStem(sourceItem, stem) : stem,
                    EOId = null,
                    Number = sourceItem.FkQuestion.ToString(),
                    TaxonomyId = targetTaxonomy.Id,
                    TestItemMCQs = getQtd2TestItemTypeName(testItemType.TestItemTypeId) == "Multiple Choice Questions" ? getMcqs(sourceItem) : null,
                    TestItemTrueFalses = getQtd2TestItemTypeName(testItemType.TestItemTypeId) == "True / False" ? getTrueFalse(sourceItem) : null,
                    TestItemMatches = getQtd2TestItemTypeName(testItemType.TestItemTypeId) == "Match the Column" ? getMatching(sourceItem) : null,
                    TestItemShortAnswers = getQtd2TestItemTypeName(testItemType.TestItemTypeId) == "Short Answers" ? getShortAnswer(sourceItem) : null,
                    TestItemFillBlanks = getQtd2TestItemTypeName(testItemType.TestItemTypeId) == "Fill in the Blank" ? getFillInTheBlank(sourceItem) : null
                });
            }

            return testItems;
        }

        private string getFillInTheBlankStem(QuestionImpl sourceItem, string originalStem)
        {
            string sourceStem = sourceItem.FkQuestionNavigation.QuestionStems.Where(r => r.FkExpiredBy == null).FirstOrDefault()?.Stem;
            string stem = (sourceStem ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceStem ?? "") : sourceStem;

            var parts = stem.Split("_______");
            var correctAnswers = sourceItem.FkQuestionNavigation.QuestionFis.Where(r => r.FkExpiredBy != null).OrderBy(r => r.Sequence);


            string stem_new = parts[0];

            for (int i = 0; i < correctAnswers.Count(); i++)
            {
                stem_new += "<u>" + RtfPipe.Rtf.ToHtml(correctAnswers.ElementAt(i).Answer ?? "") + "</u>" + parts[i + 1];
            }

            return stem_new;
        }

        private ICollection<TestItemFillBlank> getFillInTheBlank(QuestionImpl sourceItem)
        {
            List<TestItemFillBlank> distractors = new List<TestItemFillBlank>();

            var fibs = sourceItem.FkQuestionNavigation.QuestionFis.Where(r => r.FkExpiredBy != null);

            foreach (var fib in fibs)
            {
                distractors.Add(new TestItemFillBlank()
                {
                    Active = true,
                    Correct = RtfPipe.Rtf.ToHtml(fib.Answer ?? ""),
                    CorrectIndex = Convert.ToInt32(fib.Sequence)
                });
            }

            return distractors;
        }

        private ICollection<TestItemShortAnswer> getShortAnswer(QuestionImpl sourceItem)
        {
            List<TestItemShortAnswer> distractors = new List<TestItemShortAnswer>();

            var essayAnswers = sourceItem.Type == 3 ? sourceItem.FkQuestionNavigation.QuestionEs : null;
            var shortAnswers = sourceItem.Type == 4 ? sourceItem.FkQuestionNavigation.QuestionSas : null;


            if (shortAnswers != null)
            {
                foreach (var shortAnswer in shortAnswers)
                {
                    distractors.Add(new TestItemShortAnswer()
                    {
                        Active = true,
                        Responses = RtfPipe.Rtf.ToHtml(shortAnswer.Answer ?? ""),
                        AcceptableResponses = 1,
                        IsCaseSensitive = false
                    });
                }
            }

            if (essayAnswers != null)
            {
                foreach (var essayAnswer in essayAnswers)
                {
                    distractors.Add(new TestItemShortAnswer()
                    {
                        Active = true,
                        Responses = RtfPipe.Rtf.ToHtml(essayAnswer.Answer ?? ""),
                        AcceptableResponses = 1,
                        IsCaseSensitive = false
                    });
                }
            }


            return distractors;
        }

        private ICollection<TestItemMatch> getMatching(QuestionImpl sourceItem)
        {
            List<TestItemMatch> distractors = new List<TestItemMatch>();

            var choices = sourceItem.FkQuestionNavigation.QuestionMaChoices.Where(r => r.FkExpiredBy == null);
            var items = sourceItem.FkQuestionNavigation.QuestionMaItems.Where(r => r.FkExpiredBy == null);

            for (int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAt(i);
                var choice = choices.Where(r => r.Sequence == item.Sequence).FirstOrDefault();
                var correctMatch = choices.Where(r => r.Sequence == item.Choice1).FirstOrDefault();

                distractors.Add(new TestItemMatch()
                {
                    Active = true,
                    Deleted = choice.FkExpiredBy == null,
                    ChoiceDescription = RtfPipe.Rtf.ToHtml(choice.Text ?? ""),
                    MatchDescription = RtfPipe.Rtf.ToHtml(correctMatch?.Text ?? ""),
                    CorrectValue = correctMatch?.Sequence == null ? null : (char)(correctMatch?.Sequence),
                    MatchValue = (char)(choice?.Sequence),
                });
            }

            return distractors;
        }

        private ICollection<TestItemTrueFalse> getTrueFalse(QuestionImpl sourceItem)
        {
            List<TestItemTrueFalse> distractors = new List<TestItemTrueFalse>();

            var tfs = sourceItem.FkQuestionNavigation.QuestionTfs.Where(r => r.FkExpiredBy == null).First();

            distractors.Add(new TestItemTrueFalse()
            {
                Active = true,
                Choices = "True",
                IsCorrect = tfs.IsTrue.GetValueOrDefault() > 0
            });

            distractors.Add(new TestItemTrueFalse()
            {
                Active = true,
                Choices = "False",
                IsCorrect = tfs.IsTrue.GetValueOrDefault() == 0
            });

            return distractors;
        }

        private ICollection<TestItemMCQ> getMcqs(QuestionImpl sourceItem)
        {
            List<TestItemMCQ> mcqs = new List<TestItemMCQ>();

            var sourceChoices = sourceItem.FkQuestionNavigation.QuestionMcChoices.Where(r => r.FkExpiredBy == null);

            foreach (var sourceChoice in sourceChoices)
            {
                mcqs.Add(new TestItemMCQ()
                {
                    ChoiceDescription = RtfPipe.Rtf.ToHtml(sourceChoice.Choice ?? ""),
                    IsCorrect = sourceChoice.IsCorrect > 0,
                    Number = Convert.ToInt32(sourceChoice.Sequence),
                    Active = true
                });
            }

            return mcqs;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testItemTypes.Count();
        }

        protected override void updateTarget(TestItemType record)
        {
            if (record != null)
                (_target as QTD2.Data.QTDContext).TestItemTypes.Update(record);
        }
    }
}
