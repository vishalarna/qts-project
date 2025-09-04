export class StudentEvaluation_Question_LinkCreateOptions
{
    studentEvaluationId: number;

    questionIds: number[];

    isReordered?:boolean;
}


export interface EMPSettingStudentEvaluationCreationOption {
  ilaId: number;
  selection:string;
  classScheduleIds:number[]
}

export class EMPSettingStudentEvaluationUpdateOption {
  ilaId: number;
  evalId: number;
  audienceId: number;

}
