import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { Task } from '../Task/Task';

export class Task_EnablingObjective_Link {
  taskId!: any;
  enablingObjectiveId!: any;
  task!: Task;
  enablingObjective!: EnablingObjective;
}
