import { Entity } from '../Entity';
import { Task_Question } from '../Task_Question/Task_Question';
import { Version_Task } from '../Version_Task/Version_Task';

export class Version_Task_Question extends Entity {
  taskQuestionId!: any;
  versionTaskId!: any;
  question!: string;
  answer!: string;
  Version_Task!: Version_Task;
  task_Question!: Task_Question;
}
