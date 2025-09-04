import { Entity } from '../Entity';
import { Version_Task_Question } from '../Version_Task_Question/Version_Task_Question';

export class Task_Question extends Entity {
  taskId!: any;
  question!: string;
  answer!: string;
  task!: Task;
  questionNumber !: number;
  version_Task_Questions!: Version_Task_Question[];
}
