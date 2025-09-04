import { TrainingProgramReview_Employee_Link_ViewModel } from "./TrainingProgramReview_Employee_Link_ViewModel";

export class TrainingProgramReview_OverviewReviewViewModel {
    trainingProgramReviewId: string;
    trainingProgramId: string;
    trainingProgramTypeId: string;
    trainingProgramType: string;
    positionId: string;
    positionAbbreviation: string;
    positionName: string;
    startDate: string; 
    endDate: string;   
    reviewers: TrainingProgramReview_Employee_Link_ViewModel[];
    reviewDate: string; 
    published: boolean;
    active: boolean;
}