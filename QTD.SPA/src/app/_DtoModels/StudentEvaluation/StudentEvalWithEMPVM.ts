export class StudentEvaluationWithEMPVM {
  empId!: any;
  classId!: any;
  evaluationId!: any;
  empName!: string;
  empImage!: string;
  empEmail!: string;
  releaseDate!: Date | null;
  completedDate!: Date | null;
  isReleased!: boolean;
  hasAggregateData!:boolean;
  rating:RatingList[] = [];
}

export class RatingList{
  questionId!:any;
  rating!:number;
}
