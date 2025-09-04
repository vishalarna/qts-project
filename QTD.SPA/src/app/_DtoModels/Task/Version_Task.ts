import { Entity } from "../Entity";
import { Task_History } from "../Task_History/Task_History";

export class Version_Task extends Entity{
  taskId!: any;
  taskNumber!: string;
  description!: string;
  conditions!: string;
  standards!: string;
  critical!: boolean;
  tools!: string;
  references!: string;
  requiredTime!: number;
  versionNumber!: any;
  isInUse !: boolean;
  state !: number;
  task_Histories !: Task_History[];
  requalificationRequired?:boolean;
  requalificationDueDate?:Date;
  requalificationNotes?:string;
  effectiveDate!:Date;
}
