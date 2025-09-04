using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmployeeTest
{
    public class EmployeeAnswerModel
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int TestTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int TestItemTypeId { get; set; }
        public string UserAnswer { get; set; }
        public List<string> MultipleCorrectAnswer { get; set; }

        public List<FillintheBlank> BlankIndexWithAnswer { get; set; }

        public List<MatchColumns> MatchValueWithCorrectValue { get; set; }

        public EmployeeAnswerModel()
        {
                MultipleCorrectAnswer = new List<string>();
                BlankIndexWithAnswer = new List<FillintheBlank>();
                MatchValueWithCorrectValue = new List<MatchColumns>();
        }
       
    }
    public class MatchColumns
    {
        public string MatchValue { get; set; }

        public string UserValue { get; set; }
        public int CorrectIndex { get; set; }
    }
    public class FillintheBlank
    {
        public int CorrectIndex { get; set; }

        public string UserValue { get; set; }
    }

    public class ShortAnswers
    {
        public string UserAnswer { get; set; }

        public List<string> CorrectAnswers { get; set; } = new List<string>();
    }

    public class ReviewTestModel
    {
        public TestItemVM testItem { get; set; }

        public string Status { get; set; }

        public string UserAnswer { get; set; }

        public List<string> MultipleCorrectAnswer { get; set; }
        public List<FillintheBlank> BlankIndexWithAnswer { get; set; }

        public List<MatchColumns> MatchValueWithCorrectValue { get; set; }

        public List<ShortAnswers> ShortAnswerWithCorrects { get; set; }
    }

    public class SubmitTestModel
    {
        public TestItemVM testItem { get; set; }

        public bool Correct { get; set; }
        public string CompletionStatus { get; set; }
        public string UserAnswer { get; set; }
        public string ClearDescription { get; set; }
        public string TrueFalseAnswer { get; set; }
        public List<string> FillInTheBlankkAnswer { get; set; }
        public string MCQAnswer { get; set; }
        public List<string> ShortAnswer { get; set; }
        public List<string> MultiCorrectAnswers { get; set; }
        public List<char?> MatchValueAnswer { get; set; }
        public List<string> MultipleCorrectAnswer { get; set; }
        public List<FillintheBlank> BlankIndexWithAnswer { get; set; }

        public List<MatchColumns> MatchValueWithCorrectValue { get; set; }

        public int MaximumScore { get; set; }
        public int PassingScore { get; set; }
        public int TotalScore { get; set; }
        public string PassFailStatus { get; set; }
        public string ProviderName { get; set; }
        public string ILANumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? showSubmittedAnswers { get; set; }
        public bool? showCorrectIncorrectAnswers { get; set; }
        public TimeSpan TotalTestDuration { get; set; }

    }
    public class TestItemVM
    {
        public int Id { get; set; }
        public int TestItemTypeId { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string TestItemType { get; set; }
        public List<TestItemTrueFalseVM> TestItemTrueFalses { get; set; } = new List<TestItemTrueFalseVM>();
        public List<TestItemFillBlankVM> TestItemFillBlanks { get; set; } = new List<TestItemFillBlankVM>();
        public List<TestItemMatchVM> TestItemMatches { get; set; } = new List<TestItemMatchVM>();
        public  List<TestItemMCQVM> TestItemMCQs { get; set; } = new List<TestItemMCQVM>();
        public List<TestItemShortAnswerVM> TestItemShortAnswers { get; set; } = new List<TestItemShortAnswerVM>();

        public TestItemVM()
        {
        }

        public TestItemVM(int id, int testItemTypeId, string description, string number, string testItemType, List<TestItemTrueFalseVM> testItemTrueFalses, List<TestItemFillBlankVM> testItemFillBlanks, List<TestItemMatchVM> testItemMatches, List<TestItemMCQVM> testItemMCQs, List<TestItemShortAnswerVM> testItemShortAnswers)
        {
            Id = id;
            TestItemTypeId = testItemTypeId;
            Description = description;
            Number = number;
            TestItemType = testItemType;
            TestItemTrueFalses = testItemTrueFalses;
            TestItemFillBlanks = testItemFillBlanks;
            TestItemMatches = testItemMatches;
            TestItemMCQs = testItemMCQs;
            TestItemShortAnswers = testItemShortAnswers;
        }
    }
    public class TestItemTrueFalseVM
    {
        public int Id { get; set; }
        public int TestItemId { get; set; }
        public string Choices { get; set; }
        public bool IsCorrect { get; set; }


        public TestItemTrueFalseVM(int id, int testItemId, string choices, bool isCorrect)
        {
            Id = id;
            TestItemId = testItemId;
            Choices = choices;
            IsCorrect = isCorrect;
        }

        public TestItemTrueFalseVM()
        {
        }
    }
    public class TestItemFillBlankVM
    {
        public int Id { get; set; }
        public int TestItemId { get; set; }
        public int CorrectIndex { get; set; }
        public string Correct { get; set; }

        public TestItemFillBlankVM(int id, int testItemId, int correctIndex, string correct)
        {
            Id = id;
            TestItemId = testItemId;
            CorrectIndex = correctIndex;
            Correct = correct;
        }

        public TestItemFillBlankVM()
        {
        }
    }
    public class TestItemMatchVM
    {
        public int Id { get; set; }
        public int TestItemId { get; set; }
        public string ChoiceDescription { get; set; }
        public string MatchDescription { get; set; }
        public char MatchValue { get; set; }
        public char? CorrectValue { get; set; }
        public char? OriginalCorrectValue { get; set; }
        public char OriginalMatchValue { get; set; }
        public int Number { get; set; }

        public TestItemMatchVM(int id, int testItemId, string choiceDescription, string matchDescription, char matchValue, int number, char? correctValue, char? originalCorrectValue, char originalMatchValue)
        {
            Id = id;
            TestItemId = testItemId;
            ChoiceDescription = choiceDescription;
            MatchDescription = matchDescription;
            MatchValue = matchValue;
            CorrectValue = correctValue;
            Number = number;
            OriginalCorrectValue = originalCorrectValue;
            OriginalMatchValue = originalMatchValue;
        }

        public TestItemMatchVM()
        {
        }
    }
    public class TestItemMCQVM
    {
        public int Id { get; set; }
        public int TestItemId { get; set; }
        public string ChoiceDescription { get; set; }
        public bool IsCorrect { get; set; }
        public int Number { get; set; }

        public TestItemMCQVM(int id,int testItemId, string choiceDescription, bool isCorrect, int number)
        {
            Id = id;
            TestItemId = testItemId;
            ChoiceDescription = choiceDescription;
            IsCorrect = isCorrect;
            Number = number;
        }

        public TestItemMCQVM()
        {
        }
    }
    public class TestItemShortAnswerVM
    {
        public int Id { get; set; }
        public int TestItemId { get; set; }
        public string Responses { get; set; }
        public bool IsCaseSensitive { get; set; }
        public int AcceptableResponses { get; set; }
        public int Number { get; set; }
        public TestItemShortAnswerVM()
        {
        }
        public TestItemShortAnswerVM(int id ,int testItemId, string responses, bool isCaseSensitive, int acceptableResponses, int number)
        {
            Id = id;
            TestItemId = testItemId;
            Responses = responses;
            IsCaseSensitive = isCaseSensitive;
            AcceptableResponses = acceptableResponses;
            Number = number;
        }
    }
}
