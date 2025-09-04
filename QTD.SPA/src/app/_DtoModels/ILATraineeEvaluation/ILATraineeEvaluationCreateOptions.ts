export class ILATraineeEvaluationCreateOptions {
  testId!: any;
  ilaId!: any;
  testTypeId!: any;
  evaluationTypeId!: any;
  testTitle!: string;
  testInstruction!: string;
  testTimeLimitHours!: number;
  testTimeLimitMinutes!: number;
  trainingEvaluationMethod!: string;
  editId:any|null = null;
}
