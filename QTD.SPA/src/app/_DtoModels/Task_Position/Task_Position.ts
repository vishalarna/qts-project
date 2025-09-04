import { Entity } from '../Entity';
import { Position } from '../Position/Position';

export class Task_Position extends Entity {
  TaskId!: any;
  PositionId!: any;
  Task!: Task;
  Position!: Position;
}
