import { TaskWithNumberVM } from "../Task/TaskWithNumberVM";

export class TQEmpWithTasksVM{
  empFName!:string;
  empLName!:string;
  empEmail!:string;
  empImage?:string;
  empId!:any;
  tqId?:any;
  releaseDate?:any;
  dueDate?:any;
  status?:any;
  tasksWithNumber:TaskWithNumberVM[] = [];
}
