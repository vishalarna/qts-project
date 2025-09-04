import { EvaluatorOption } from "./EvaluatorOptions";

export class TQReleaseByTaskAndSkillOptions{
  taskId:any;
  empIds:any[] = [];
  enablingObjectiveId:any;
  evaluatorOptions:EvaluatorOption[] = [];
  evalMethodId = null;
  dueDate!:Date;
  releaseDate!:Date;
  oneReq!:boolean;
  multiReq!:number;
  orderMatters!:boolean;
  showSuggestions!:boolean;
  showQuestions!:boolean;
}
