import { Entity } from '../Entity';
import { Task } from '../Task/Task';

export class Task_Step extends Entity {
  taskId!: any;
  description!: string;
  number!: number;
  active!: boolean;
  task!: Task;
}
