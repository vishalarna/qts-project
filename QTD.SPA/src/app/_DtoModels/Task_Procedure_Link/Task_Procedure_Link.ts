import { Procedure } from '../Procedure/Procedure';
import { Task } from '../Task/Task';

export class Task_Procedure_Link {
  procedureId!: any;
  taskId!: any;
  procedure!: Procedure;
  task!: Task;
}
