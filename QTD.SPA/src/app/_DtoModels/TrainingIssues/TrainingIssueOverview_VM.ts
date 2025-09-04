import { TrainingIssueOverview_TrainingIssue_VM } from "./TrainingIssueOverview_TrainingIssue_VM";

export class TrainingIssueOverview_VM {
  trainingIssuesOpen: number;
  trainingIssuesClosed: number;
  trainingIssuesWithPendingActionItems: number;
  trainingIssuesWithNoActionItems: number;
  trainingIssues: TrainingIssueOverview_TrainingIssue_VM[];
}
