namespace QTD2.Infrastructure.Model.MetaILA_SummaryTest
{
    public class MetaILA_SummaryTest_ViewModel
    {
        public int Id { get; set; }
        public virtual QTD2.Domain.Entities.Core.Test Test { get; set; }
        public string? TestInstruction { get; set; }
        public int? TestTimeLimitHours { get; set; }
        public int? TestTimeLimitMinutes { get; set; }
        public int TestTypeId { get; set; }
        public int? PositionId { get; set; }
        public string TestType { get; set; }
        
        public MetaILA_SummaryTest_ViewModel()
        {

        }

        public MetaILA_SummaryTest_ViewModel(int id, string? testInstruction, int? testTimeLimitHours, int? testTimeLimitMinutes, int testTypeId, int? positionId, QTD2.Domain.Entities.Core.Test  test, string testType)
        {
            Id = id;
            TestInstruction = testInstruction;
            TestTimeLimitHours = testTimeLimitHours;
            TestTimeLimitMinutes = testTimeLimitMinutes;
            TestTypeId = testTypeId;
            PositionId = positionId;
            Test = test;
            TestType = testType;
        }
    }
}
