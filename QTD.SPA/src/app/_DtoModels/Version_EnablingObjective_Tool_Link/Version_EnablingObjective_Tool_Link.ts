import { Entity } from '../Entity';
import { Version_EnablingObjective } from '../Version_EnablingObjective/Version_EnablingObjective';
import { Version_Tool } from '../Version_Tool/Version_Tool';

export class Version_EnablingObjective_Tool_Link extends Entity {
  version_EnablingObjectiveId!: any;
  version_ToolId!: any;
  version_Tool!: Version_Tool;
  version_EnablingObjective!: Version_EnablingObjective;
}
