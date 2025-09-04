namespace QTD2.Domain.Entities.Core
{
    public class MetaILA_SummaryTest : Common.Entity
    {
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        public string TestInstruction { get; set; }
        public int? TestTimeLimitHours { get; set; }
        public int? TestTimeLimitMinutes { get; set; }
        public int TestTypeId { get; set; }
        public virtual TestType TestType { get; set; }
        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }

        public MetaILA_SummaryTest()
        {

        }

        public MetaILA_SummaryTest(int testId, string testInstruction, int? testTimeLimitHours, int? testTimeLimitMinutes, int testTypeId, int? positionId)
        {
            TestId = testId;
            TestInstruction = testInstruction;
            TestTimeLimitHours = testTimeLimitHours;
            TestTimeLimitMinutes = testTimeLimitMinutes;
            TestTypeId = testTypeId;
            PositionId = positionId;
        }

        public void SetTestInstruction(string testInstruction)
        {
            TestInstruction = testInstruction;
        }

        public void SetTestTimeLimitHours(int? testTimeLimitHours)
        {
            TestTimeLimitHours = testTimeLimitHours;
        }

        public void SetTestTimeLimitMinutes(int? testTimeLimitMinutes)
        {
            TestTimeLimitMinutes = testTimeLimitMinutes;
        }

        public void SetTestTypeId(int testTypeId)
        {
            TestTypeId = testTypeId;
        }

        public void SetPositionId(int? positionId)
        {
            PositionId = positionId;
        }
    }
}