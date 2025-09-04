import { Entity } from '../Entity';
import { Version_SaftyHazard } from '../Version_SaftyHazard/Version_SaftyHazard';
import { Version_Task } from '../Version_Task/Version_Task';

export class Version_Task_SaftyHazard_Link extends Entity {
  version_TaskId!: any;
  version_SaftyHazardId!: any;
  version_Task!: Version_Task;
  version_SaftyHazard!: Version_SaftyHazard;
}
