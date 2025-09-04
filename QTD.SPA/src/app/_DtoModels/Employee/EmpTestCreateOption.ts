export interface EmpTestCreateOption {
  testId: number
  questionId: number
  testTypeId: number
  employeeId: number
  testItemTypeId: number
  classScheduleId: number
  userAnswer: string[]
  matchValue: string
  correctIndex: number
  rosterId:any;
  blankIndexWithAnwer: BlankIndexWithAnwer[]
  matchValueWithCorrectValue: MatchValueWithCorrectValue[];
  shortAnswerDescription:string;
  }

  export interface BlankIndexWithAnwer {
    correctIndex: number
    userValue: string
  }

  export interface MatchValueWithCorrectValue {
    matchValue: string
    userValue: string,
    id:string,
    correctIndex:number
  }

