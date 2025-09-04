using Azure;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class TestSpecifications: IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }

        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public List<Test> Tests { get; set; }
        public bool IsShowCorrectAnswer { get; set; }
        public bool IsIncludeTestStatistics { get; set; }
        public List<ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }

        public List<TestSpecifications_TestStatistics> TestStatistics { get; set; } = new List<TestSpecifications_TestStatistics>();
        public TestSpecifications(string title, string templatePath, List<string> displayColumns, string companyLogo, List<Test> tests, bool isShowCorrectAnswer, bool isIncludeTestStatistics, List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements, string defaultTimeZone)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Tests = tests;
            Title = title;
            CompanyLogo = companyLogo;
            IsShowCorrectAnswer = isShowCorrectAnswer;
            IsIncludeTestStatistics = isIncludeTestStatistics;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;

            foreach (var test in tests)
            {
                TestStatistics.Add(new TestSpecifications_TestStatistics(test));
            }
        }
    }

    public class TestSpecifications_TestStatistics
    {
        public Test Test { get; set; }
        public List<TestSpecifications_TestItemStatistics> TestItemStatistics { get; set; } = new List<TestSpecifications_TestItemStatistics>();

        public TestSpecifications_TestStatistics(Test test)
        {
            Test = test;

            foreach (var testItem in test.Test_Item_Links.Select(til => til.TestItem).ToList())
            {
                var responses = test.ClassSchedule_Rosters.SelectMany(csr => csr.Responses).Where(r => r.TestItemId == testItem.Id).ToList();
                TestItemStatistics.Add(new TestSpecifications_TestItemStatistics(testItem, responses));
            }
        }
    }

    public class TestSpecifications_TestItemStatistics
    {
        public TestItem TestItem { get; set; }
        public int AnsweredCorrectly { get; set; }
        public int TotalAnswers { get; set; }
        public List<TestSpecifications_ResponseStatistics> ResponseStatistics { get; set; } = new List<TestSpecifications_ResponseStatistics>();

        public TestSpecifications_TestItemStatistics(TestItem testItem, List<ClassSchedule_Roster_Response> responses)
        {
            TestItem = testItem;

            foreach (var response in responses)
            {
                //conditionally increment correct count
                if (response.IsCorrect ?? false)
                {
                    AnsweredCorrectly += 1;
                }

                //increment answered count
                TotalAnswers += 1;

                // Construct the string version of the response based on testitem type
                // This creates the strings in a standard way so like-responses should inherently be grouped for counting purposes
                string responseString = "";
                switch (testItem.TestItemTypeId)
                {
                    case 1:
                        //Multiple Choice Questions
                        responseString = response.Selections.FirstOrDefault()?.UserAnswer;
                        break;
                    case 2:
                        //Fill in the Blank
                        responseString = string.Join(",", response.Selections.OrderBy(s => s.CorrectIndex).Select(s => s.UserAnswer));
                        break;
                    case 3:
                        //True / False
                        responseString = response.Selections.FirstOrDefault()?.UserAnswer;
                        break;
                    case 4:
                        //Short Answers
                        responseString = response.Selections.FirstOrDefault()?.UserAnswer;
                        break;
                    case 5:
                        //Match the Column
                        responseString =
                            string.Join(
                                ',',
                                testItem.TestItemMatches
                                .Where(tim => tim.CorrectValue != null)
                                .OrderBy(tim => tim.Number)
                                .Select(tim => response.Selections
                                    .FirstOrDefault(r => r.MatchValue == tim.MatchValue.ToString())?.UserAnswer
                                )
                            );

                        break;
                    case 6:
                        //Multiple Correct Answers
                        var unorderedUserAnswers = response.Selections.Select(s => s.UserAnswer);

                        //Loop the MCQ options, in order, constructing a list of string responses to then join by ','. This guarantees regardless of the order which the UserAnswers were persisted, they always list the same way here
                        var orderedMCQUserAnswers = new List<string>();
                        foreach (var testItemMCQ in testItem.TestItemMCQs.OrderBy(timcq => timcq.Number))
                        {
                            if (unorderedUserAnswers.Contains(testItemMCQ.ChoiceDescription))
                            {
                                orderedMCQUserAnswers.Add(testItemMCQ.ChoiceDescription);
                            }
                        }

                        responseString = string.Join(",", orderedMCQUserAnswers);
                        break;
                }

                // Add / update ResponseStatistics for the record where == the constructed string (adds if doesn't yet exist)
                var responseRecord = ResponseStatistics.FirstOrDefault(rs => rs.ResponseString == responseString);
                if (responseRecord != null)
                {
                    responseRecord.ResponseCount += 1;
                }
                else
                {
                    ResponseStatistics.Add(new TestSpecifications_ResponseStatistics(responseString, 1));
                }

            }
            
        }
    }

    public class TestSpecifications_ResponseStatistics
    {
        public string ResponseString { get; set; }
        public int ResponseCount { get; set; }
        public TestSpecifications_ResponseStatistics(string responseString, int responseCount)
        {
            ResponseString = responseString;
            ResponseCount = responseCount;
        }
    }
}
