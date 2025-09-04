export class TaskQualificationCreateOptions {
  taskId!: any;
  empId!: any;
  evaluationId!: any;
  taskQualificationDate?: Date;
  taskQualificationEvaluator: string;
  dueDate: Date;
  criteriaMet: boolean;
  comments: string;
  evaluatorIds:any[] = [];
}
