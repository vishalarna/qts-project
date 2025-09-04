import { Entity } from "../Entity";

export class Task_History extends Entity{
  taskId !: any;
  version_TaskId !: any;
  oldStatus !: boolean;
  newStatus !: any;
  changeEffectiveDate !: Date;
  changeNotes !: string;
}
