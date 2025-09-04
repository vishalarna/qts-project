import { Entity } from '../Entity';
import { Version_EnablingObjective } from '../Version_EnablingObjective/Version_EnablingObjective';
import { Version_Task } from '../Version_Task/Version_Task';

export class Version_Task_EnablingObjective_Link extends Entity {
  version_EnablingObjectiveId!: any;
  version_TaskId!: any;
  version_EnablingObjective!: Version_EnablingObjective;
  version_Task!: Version_Task;
}
