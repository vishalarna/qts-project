import { TrainingProgramReview_OverviewReviewViewModel } from "./TrainingProgramReview_OverviewReviewViewModel";

export class TrainingProgramReview_OverviewViewModel {
    activeInitialTrainingProgramReviews: string;
    inactiveInitialTrainingProgramReviews: string;
    activeContinuingTrainingProgramReviews: string;
    inactiveContinuingTrainingProgramReviews: string;
    activeCycleTrainingProgramReviews: string;
    inactiveCycleTrainingProgramReviews: string;
    noReviewTrainingPrograms: string;
    withReviewInDraftTrainingPrograms: string;
    trainingProgramReviewOverviewReviews: TrainingProgramReview_OverviewReviewViewModel[];
}