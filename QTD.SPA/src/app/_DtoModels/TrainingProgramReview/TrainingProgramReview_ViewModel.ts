import { TrainingProgramReview_Employee_Link_ViewModel } from "./TrainingProgramReview_Employee_Link_ViewModel";

export class TrainingProgramReview_ViewModel {
    id: string;
    positionId: string;
    positionName: string;
    trainingProgramTypeId: string;
    trainingProgramType: string;
    trainingProgramId: string;
    trainingProgram_ProgramTitle: string;
    trainingProgram_Version: string;
    reviewers: TrainingProgramReview_Employee_Link_ViewModel[];
    reviewDate: string; 
    startDate: string; 
    endDate: string;   
    purpose: string;
    method: string;
    historicalBackground: string;
    programDesign: string;
    programMaterials: string;
    programImplementation: string;
    evaluationOfTraineeLearning: string;
    studentEvaluationResults: string;
    conclusion: string;
    summary: string;
    trainerName: string;
    title: string;
    trainerSignOff: boolean;
    published: boolean;
}
