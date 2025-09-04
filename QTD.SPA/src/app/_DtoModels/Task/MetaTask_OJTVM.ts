export class MetaTask_OJTVM {
  id!: any;
  taskId!: any;
  description!: string;
  parentStepId?: number;
  number?: number;
  isCreated!: boolean;
}

export class MetaTask_QuestionsVM {
  id!: any;
  taskId: any;
  question!: string;
  answer!: string;
  questionNumber!: number;
  isCreated!: boolean;
}

export class MetaTask_SuggestionsVM {
  id!: number;
  taskId!: number;
  description!: string;
  number!: number;
  isCreated!: boolean;
}
