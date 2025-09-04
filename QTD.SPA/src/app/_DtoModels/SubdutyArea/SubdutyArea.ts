import { DutyArea } from '../DutyArea/DutyArea';
import { Entity } from '../Entity';
import { Task } from '../Task/Task';

export class SubdutyArea extends Entity {
  dutyAreaId!: any;
  description!: string;
  subNumber!: number;
  active!: boolean;
  title!:string;
  effectiveDate!:Date;
  reasonForRevision!:string;
  dutyArea: DutyArea = new DutyArea();
  tasks: Task[] = [];
}
