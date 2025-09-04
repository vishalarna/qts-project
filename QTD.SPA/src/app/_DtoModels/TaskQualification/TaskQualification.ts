import { Employee } from "../Employee/Employee";
import { Entity } from "../Entity";
import { EvaluationMethod } from "../EvaluationMethod/EvaluationMethod";
import { Task } from "../Task/Task";

export class TaskQualification extends Entity{
  taskId!:any;
  empId!:any;
  evaluationId!:any;
  taskQualificationDate?:Date|string;
  dueDate?:Date;
  taskQualificationEvaluator!:string;
  criteriaMet!:string;
  comments!:string;
  task!:Task;
  employee!:Employee;
  evaluationMethod!:EvaluationMethod;
  evaluatorId!:any;
}
