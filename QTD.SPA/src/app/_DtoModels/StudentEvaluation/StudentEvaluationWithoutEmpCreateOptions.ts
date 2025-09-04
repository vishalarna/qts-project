export class StudentEvaluationWithoutEmpCreateOptions{
  studentEvaluationId!:any;
  classScheduleId!:any;
  dataMode!:'Aggregate' | 'Individual';
  additionalComments!:string;
  studentEvalData!:StudentEval[];
  empId!:any;
}

export class StudentEval{
  questionId!:any;
  ratingScale!:number | null;
  high!:number;
  average!:number;
  low!:number;
  notes!:string;
}
