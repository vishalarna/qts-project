export class TaskReQualificationCreateOption {
    suggestionList: SuggestionList[]=[];
    taskDescription: string;
    traineeId: string;
    taskQualificationId: string;
    taskId: string;
  }
  
  export interface SuggestionList {
    suggestionId: string
    suggesntionDescription: string
    comments: string
    isCompleted: boolean
  }
  

  export class TaskReQualificationStepsCreateOption {
    stepsList: StepsList[]=[]
    taskDescription: string;
    traineeId: string;
    taskQualificationId: string;
    taskId: string;
  }
  
  export interface StepsList {
    stepId: string;
    stepDescription: string
    comments: string
    isCompleted: boolean
  }
  
  export class TaskReQualificationQuestionsCreateOption {
    quesionAnswerList: QuesionAnswerList[]=[];
    taskDescription: string;
    traineeId: string;
    taskQualificationId: string;
    taskId: string;
  }
  
  export interface QuesionAnswerList {
    questionId: string;
    questionDescription: string
    answer: string
    comments: string
    isCompleted: boolean
  }

  export class TaskReQualificationSignOffOption {
    taskQualificationId: string;
    isCriteriaMet: boolean;
    comments: string;
    evaluatorId: string;
    evaluationMethodId: string;
    taskQualificationDate: string;
    traineeId: string;
    isFormSubmitted!:boolean;
    isEvaluatorSignOff!:boolean;
    isTraineeSignOff!:boolean;
  }
  