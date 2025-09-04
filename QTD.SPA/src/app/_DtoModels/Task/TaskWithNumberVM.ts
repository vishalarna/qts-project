import { TQEmpWithTasksVM } from "@models/TaskQualification/TQEmpWithTasksVM";
import { Task } from "./Task";

export class TaskWithNumberVM{
  daNumber!:number;
  sdaNumber!:number;
  letter!:string;
  task!:Task;
  tqId!:any;
  dueDate:Date;
  releaseDate:Date;
  status:string;
  tqEmpWithTasksVM:TQEmpWithTasksVM[] =[];
}
