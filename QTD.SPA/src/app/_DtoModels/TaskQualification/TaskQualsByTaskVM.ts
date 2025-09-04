import { EvaluatorNameWithStatus } from "./EvaluatorNameWithStatus";

export class TaskQualsByTaskVM{
  empId!: string;
  empName!: string;
  empImage!: string;
  positions!: string;
  tqId!: string;
  dueDate?: Date;
  releaseDate?: Date;
  requiredRequals!: string;
  releaseToAllSingleSignOff!: boolean;
  signOffOrderEnabled!: boolean;
  evaluatorListWithStatus!: EvaluatorNameWithStatus[];
  tqStatus!: string;
  canStart!:boolean;
}