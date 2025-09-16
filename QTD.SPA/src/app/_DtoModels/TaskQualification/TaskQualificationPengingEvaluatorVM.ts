import { EvaluatorNameWithStatus } from "./EvaluatorNameWithStatus";

export class TaskQualificationPengingEvaluatorVM {
  id: string;
  empId: string;
  taskId: string;
  skillId: string;
  skillQualificationId: string;
  empFName: string;
  empLastName: string;
  empImage: string;
  empEmail: string;
  empNumber: string;
  empPositions: string;
  taskFullNumber: string;
  skillFullNumber: string;
  taskNumber: number;
  taskLetter: string;
  taskDescription: string;
  skillDescription: string;
  dueDate?: Date;
  empReleaseDate?: Date;
  requiredRequals: string;
  evaluatorName: string;
  comments: string;
  status: string;
  createdDate?: Date;
  modifiedDate?: Date;
  releaseToAllSingleSignOff!: boolean;
  signOffOrderEnabled!: boolean;
  evaluatorListWithStatus!: EvaluatorNameWithStatus[];
  canStart!:boolean;
  sqDueDate: Date;
}
