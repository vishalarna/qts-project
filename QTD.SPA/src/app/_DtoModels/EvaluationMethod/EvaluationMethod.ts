import { Entity } from "../Entity";
import { TaskQualification } from "../TaskQualification/TaskQualification";

export class EvaluationMethod extends Entity{
  description !: string;
  taskQualifications!:TaskQualification[];
}
