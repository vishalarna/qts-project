import { Entity } from '../Entity';
import { Version_Task } from '../Version_Task/Version_Task';
import { Version_Tool } from '../Version_Tool/Version_Tool';

export class Version_Task_Tool_Link extends Entity {
  version_TaskId!: any;
  version_ToolId!: any;
  version_Task!: Version_Task;
  version_Tool!: Version_Tool;
}
