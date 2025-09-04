using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TrainingProgramReviewsRepository : Common.Repository<TrainingProgramReview>, ITrainingProgramReviewsRepository
    {
        public TrainingProgramReviewsRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
