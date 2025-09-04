export class DIFSurveyEmployeeResponseModel  {
    dIFSurveyTaskId: string;
    difficulty: number;
    importance: number;
    frequency: number;
    nA: boolean;
    comments: string;

    constructor(taskId: string, diff: number, imp: number, freq: number, na: boolean, comment: string){
        this.dIFSurveyTaskId = taskId;
        this.difficulty = diff;
        this.importance = imp;
        this.frequency = freq;
        this.nA = na;
        this.comments = comment;
    }
  }