import { Entity } from "../Entity";
import { TraineeEvaluationType } from "../TraineeEvaluationType/TraineeEvaluationType";

export class ILATraineeEvaluation extends Entity {
  testId!: any;
  ilaId!: any;
  testTypeId!: any;
  evaluationTypeId!: any;
  testTitle!: string;
  testInstruction!: string;
  testTimeLimitHours!: number;
  testTimeLimitMinutes!: number;
  trainingEvaluationMethod!: string;
  traineeEvaluationType!:TraineeEvaluationType;
}

export class IlaTraineeEvaluationList {
  evaluationMethodType!: string;
  evaluationType!: string;
  evaluationDescription!: string;
  evaluationId!: string;
  isActive!: boolean;
  data!: any;
}


export class IlaTraineeEvaluationStatusOption {
  status!: boolean;
  type!: string;
  evaluationId!: string;
}
