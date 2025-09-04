using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILATraineeEvaluation : Entity
    {
        public int TestId { get; set; }

        public int ILAId { get; set; }

        public int? TestTypeId { get; set; }

        public int EvaluationTypeId { get; set; }

        public string TestTitle { get; set; }

        public string TestInstruction { get; set; }

        public int TestTimeLimitHours { get; set; }

        public int TestTimeLimitMinutes { get; set; }

        public string TrainingEvaluationMethod { get; set; }

        public virtual Test Test { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual TestType TestType { get; set; }

        public virtual TraineeEvaluationType TraineeEvaluationType { get; set; }

        public virtual ICollection<DiscussionQuestion> DiscussionQuestions { get; set; } = new List<DiscussionQuestion>();

        public ILATraineeEvaluation()
        {
        }

        public ILATraineeEvaluation(int testId, int iLAId, int evaluationTypeId, string testTitle, string testInstruction, int testTimeLimitHours, int testTimeLimitMinutes, string trainingEvaluationMethod, int? testTypeId)
        {
            TestId = testId;
            ILAId = iLAId;
            EvaluationTypeId = evaluationTypeId;
            TestTitle = testTitle;
            TestInstruction = testInstruction;
            TestTimeLimitHours = testTimeLimitHours;
            TestTimeLimitMinutes = testTimeLimitMinutes;
            TrainingEvaluationMethod = trainingEvaluationMethod;
            TestTypeId = testTypeId;
        }

        public override void Modify(string username)
        {
            base.Modify(username);
            AddDomainEvent(new Domain.Events.Core.OnILATraineeEvaluation_Updated(this));
        }

            //public override T Copy<T>(string createdBy)
            //{
            //    var ilaTrainingEvaluation =  base.Copy<T>(createdBy) as ILATraineeEvaluation;
            //    ilaTrainingEvaluation.TestId = 0;
            //    var ilaTrainingEvaluationTestCopy = this.Test.Copy<Test>(createdBy);
            //    ilaTrainingEvaluationTestCopy.Id = 0;
            //    ilaTrainingEvaluation.Test = ilaTrainingEvaluationTestCopy;          
            //    return (T)(object)ilaTrainingEvaluation;
            //}
        }
}
