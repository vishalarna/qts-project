using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TestItemTypesMap : Common.MigrationMap<TblTestItemType, TestItemType>
    {
        List<TblTestItemType> _testItemType;
        List<TblTestItem> _testItems;
        List<TblTestitemDistractor> _testItemDistractiors;
        List<TblTaxonomy> _tblTaxonomies;

        List<Image> _images;

        public TestItemTypesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTestItemType> getSourceRecords()
        {
            _testItemType = (_source as EMP_DemoContext).TblTestItemTypes.ToList();
            _testItemDistractiors = (_source as EMP_DemoContext).TblTestitemDistractors.ToList();
            _testItems = (_source as EMP_DemoContext).TblTestItems.ToList();
            _tblTaxonomies = (_source as EMP_DemoContext).TblTaxonomies.ToList();

            _images = (_target as QTD2.Data.QTDContext).Images.ToList();

            return _testItemType;
        }

        protected override TestItemType mapRecord(TblTestItemType obj)
        {
            var testItemType = (_target as QTD2.Data.QTDContext).TestItemTypes.Where(r => r.Description == getQtd2TestItemTypeName(obj.TestItemType)).First();

            testItemType.TestItems = getTestItems(obj);

            return testItemType;
        }

        private ICollection<TestItem> getTestItems(TblTestItemType obj)
        {
            List<TestItem> testItems = new List<TestItem>();
            var sourceItems = obj.TblTestItems;

            foreach (var sourceItem in sourceItems)
            {
                var sourceTaxonomy = _tblTaxonomies.Where(r => r.TaxonomyLevelId == sourceItem.TaxonomyId).First();
                var targetTaxonomy = (_target as QTD2.Data.QTDContext).TaxonomyLevels.Where(r => r.Description == sourceTaxonomy.TaxonomyLevel).First();

                EnablingObjective targetEo = null;

                if (sourceItem.Skid != null)
                {
                    var sourceEo = (_source as EMP_DemoContext).TblSkillsKnowledges.Where(r => r.Skid == sourceItem.Skid).First();
                    var sourceCateogry = (_source as EMP_DemoContext).TblCategories.Where(r => r.Cid == sourceEo.Cid).First();

                    string sksubnum = sourceEo.SksubNum.HasValue ? sourceEo.SksubNum.Value.ToString() : "";
                    if (!String.IsNullOrEmpty(sksubnum) && sksubnum != "0")
                    {
                        int skNum = sourceEo.Sknum.HasValue ? sourceEo.Sknum.Value : 0;
                        int csubnum = sourceCateogry.CsubNum.GetValueOrDefault();

                        var targetCatgory = (_target as QTD2.Data.QTDContext).EnablingObjective_Categories.Where(r => r.Number == sourceCateogry.Cnum).First();
                        var targetSubCategory = targetCatgory.EnablingObjective_SubCategories.Where(r => r.Number == csubnum).FirstOrDefault();
                        var targetopic = targetSubCategory == null ? null : targetSubCategory.EnablingObjective_Topics.Where(r => r.Number == skNum).FirstOrDefault();

                        if (skNum == 0)
                        {
                            if (csubnum == 0)
                            {
                                targetEo = (_target as QTD2.Data.QTDContext).EnablingObjectives.Where(r => r.Number == sksubnum).Where(r => r.CategoryId == targetCatgory.Id).First();
                            }
                            else
                            {
                                targetEo = (_target as QTD2.Data.QTDContext).EnablingObjectives.Where(r => r.Number == sksubnum).Where(r => r.SubCategoryId == targetSubCategory.Id).First();
                            }
                        }
                        else
                        {
                            targetEo = (_target as QTD2.Data.QTDContext).EnablingObjectives.Where(r => r.Number == sksubnum).Where(r => r.TopicId == targetopic.Id).First();
                        }
                    }
                }

                string stem = (sourceItem.Stem ?? "").Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(sourceItem.Stem ?? "") : sourceItem.Stem;

                testItems.Add(new TestItem()
                {
                    Active = sourceItem.Active.GetValueOrDefault(),
                    Deleted = sourceItem.Deleted.GetValueOrDefault(),
                    IsActive = sourceItem.Active.GetValueOrDefault(),
                    Description = sourceItem.TestItemTypeNavigation.TestItemType == "Fill-in-the-blank" ? getFillInTheBlankStem(sourceItem, stem) : stem,
                    EOId = targetEo != null ? targetEo.Id : null,
                    Number = sourceItem.TestItemId.ToString(),
                    TaxonomyId = targetTaxonomy.Id,
                    TestItemMCQs = sourceItem.TestItemTypeNavigation.TestItemType == "Multiple Choice" ? getMcqs(sourceItem) : null,
                    TestItemTrueFalses = sourceItem.TestItemTypeNavigation.TestItemType == "True/False" ? getTrueFalse(sourceItem) : null,
                    TestItemMatches = sourceItem.TestItemTypeNavigation.TestItemType == "Matching" ? getMatching(sourceItem) : null,
                    TestItemShortAnswers = sourceItem.TestItemTypeNavigation.TestItemType == "Short Answer" ? getShortAnswer(sourceItem) : null,
                    TestItemFillBlanks = sourceItem.TestItemTypeNavigation.TestItemType == "Fill-in-the-blank" ? getFillInTheBlank(sourceItem) : null,
                    ImageId = getImageId(sourceItem)
                });
            }

            return testItems;
        }

        private int? getImageId(TblTestItem sourceItem)
        {
            if (sourceItem.ImageId.HasValue)
            {
                var image = _images.Where(r => r.Description == sourceItem.ImageId.ToString()).FirstOrDefault();

                if (image == null) return null;

                return image.Id;
            }

            else return null;
        }

        private string getFillInTheBlankStem(TblTestItem sourceItem, string originalStem)
        {
            var fibs = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId).ToList();
            var fib = fibs.First();

            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            xdoc.LoadXml(fib.DistractorDetails);
            var parts = xdoc.FirstChild.InnerText.Split("&blank;");

            string stem = originalStem;

            if (parts.Length == 3)
                stem = parts[0] + "<u>" + parts[1] + "</u>" + parts[2];

            return "<p>" + stem + "</p>";
        }

        private ICollection<TestItemFillBlank> getFillInTheBlank(TblTestItem sourceItem)
        {
            List<TestItemFillBlank> distractors = new List<TestItemFillBlank>();

            var fibs = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId).ToList();

            foreach (var fib in fibs)
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(fib.DistractorDetails);

                distractors.Add(new TestItemFillBlank()
                {
                    Active = true,
                    Correct = xdoc.FirstChild.InnerText.Split("&blank;").Length > 2 ? xdoc.FirstChild.InnerText.Split("&blank;")[1] : "",
                    CorrectIndex = fibs.IndexOf(fib)
                });
            }

            return distractors;
        }

        private ICollection<TestItemShortAnswer> getShortAnswer(TblTestItem sourceItem)
        {
            List<TestItemShortAnswer> distractors = new List<TestItemShortAnswer>();

            var shortAnswers = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId);

            foreach (var shortAnswer in shortAnswers)
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(shortAnswer.DistractorDetails);

                distractors.Add(new TestItemShortAnswer()
                {
                    Active = true,
                    Responses = "Responses",
                    AcceptableResponses = 1,
                    IsCaseSensitive = false
                });
            }

            return distractors;
        }

        private ICollection<TestItemMatch> getMatching(TblTestItem sourceItem)
        {
            List<TestItemMatch> distractors = new List<TestItemMatch>();

            var matchings = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId).ToList();

            foreach (var matching in matchings)
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(matching.DistractorDetails);

                var choiceDescription = xdoc.FirstChild.SelectNodes("ColumnA")[0].InnerText;
                var matchDescription = xdoc.FirstChild.SelectNodes("ColumnB")[0].InnerText;

                distractors.Add(new TestItemMatch()
                {
                    Active = true,
                    ChoiceDescription = choiceDescription.Substring(0, Math.Min(choiceDescription.Length, 500)),
                    MatchDescription = matchDescription.Substring(0, Math.Min(matchDescription.Length, 500)),
                    CorrectValue = xdoc.FirstChild.Attributes["CorrectMatch"].Value.ToString().Length > 0 ? xdoc.FirstChild.Attributes["CorrectMatch"].Value.ToString()[0] : " "[0],
                    MatchValue = xdoc.FirstChild.Attributes["ColumnCodeAlpha"].Value.ToString().Length > 0 ? xdoc.FirstChild.Attributes["ColumnCodeAlpha"].Value.ToString()[0] : " "[0],
                    Number = matchings.IndexOf(matching) + 1
                });
            }

            return distractors;
        }

        private ICollection<TestItemTrueFalse> getTrueFalse(TblTestItem sourceItem)
        {
            List<TestItemTrueFalse> distractors = new List<TestItemTrueFalse>();

            var tfs = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId);

            foreach (var tf in tfs)
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(tf.DistractorDetails);

                string correctAnswerString = xdoc.FirstChild.Attributes["CorrectAnswer"]?.Value;

                bool correctAnswer = false;
                bool parsed = Boolean.TryParse(correctAnswerString, out correctAnswer);

                distractors.Add(new TestItemTrueFalse()
                {
                    Active = true,
                    Choices = "True",
                    IsCorrect = correctAnswerString == "True"
                });

                distractors.Add(new TestItemTrueFalse()
                {
                    Active = true,
                    Choices = "False",
                    IsCorrect = correctAnswerString == "False"
                });
            }

            return distractors;
        }

        private ICollection<TestItemMCQ> getMcqs(TblTestItem sourceItem)
        {
            List<TestItemMCQ> mcqs = new List<TestItemMCQ>();

            var sourceMcqs = _testItemDistractiors.Where(r => r.TestItemId == sourceItem.TestItemId);

            foreach (var sourceMcq in sourceMcqs)
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(sourceMcq.DistractorDetails.SanitizeXml().Replace("&", "&amp;"));

                string description = xdoc.FirstChild.InnerText.Trim();
                bool isCorrect = Convert.ToBoolean(xdoc.FirstChild.Attributes["Correct"].Value);
                int number = char.ToUpper(xdoc.FirstChild.Attributes["Code"].Value[0]) - 64;

                mcqs.Add(new TestItemMCQ()
                {
                    ChoiceDescription = description.Substring(0, Math.Min(description.Length, 500)),
                    IsCorrect = isCorrect,
                    Number = number,
                    Active = true
                });
            }

            return mcqs;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _testItemType.Count();
        }

        protected override void updateTarget(TestItemType record)
        {
            (_target as QTD2.Data.QTDContext).TestItemTypes.Update(record);
        }
    }
}
