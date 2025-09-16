import { TQEvalSignOffModel } from "./TQEvalSignOffModel";

export class TQEmpWithPosAndTaskVM {
  id?:number;
  empId!:number;
  taskId?:number;
  empReleaseDate?: Date;
  evaluatorNames!:string;
  dueDate?:Date;
  taskDescription!:string;
  number?:string;
  totalRequiredSignOffs!:number;  
  totalCompletedSignOffs!:number;
  posNames!:string;
  tQEvalSignOffModel: TQEvalSignOffModel[];
  comment?:string;
  enablingObjectiveId?:number;
  enablingObjectiveDescription?:string;
  enablingObjectiveNumber?:string;
}
