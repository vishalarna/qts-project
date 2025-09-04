using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.TrainingProgramReview
{
    public class TrainingProgramReview_OverviewViewModel
    {
        public int ActiveInitialTrainingProgramReviews { get; set; }
        public int InactiveInitialTrainingProgramReviews { get; set; }
        public int ActiveContinuingTrainingProgramReviews { get; set; }
        public int InactiveContinuingTrainingProgramReviews { get; set; }
        public int ActiveCycleTrainingProgramReviews { get; set; }
        public int InactiveCycleTrainingProgramReviews { get; set; }
        public int NoReviewTrainingPrograms { get; set; }
        public int WithReviewInDraftTrainingPrograms { get; set; }
        public List<TrainingProgramReview_OverviewReviewViewModel> TrainingProgramReviewOverviewReviews { get; set; }
    }
}
