import { Entity } from '../Entity';
import { Task_Step } from '../Task_Step/Task_Step';
import { Version_Task } from '../Version_Task/Version_Task';

export class Version_Task_Step extends Entity{
  taskStepId!: any;
  versionTaskId!: any;
  description!: string;
  number!: number;
  version_Task!: Version_Task;
  task_Step!: Task_Step;
}
