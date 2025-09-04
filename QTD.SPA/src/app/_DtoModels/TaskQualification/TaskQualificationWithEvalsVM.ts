import { Employee } from "../Employee/Employee";
import { TaskQualification } from "./TaskQualification";

export class TaskQualificationWithEvalsVM{
  taskQualification!:TaskQualification;
  evaluators!:Employee[];
}
