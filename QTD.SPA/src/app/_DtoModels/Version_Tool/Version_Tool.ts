import { Entity } from '../Entity';
import { Tool } from '../Tool/Tool';
import { Version_EnablingObjective_Tool_Link } from '../Version_EnablingObjective_Tool_Link/Version_EnablingObjective_Tool_Link';
import { Version_Procedure_Tool_Link } from '../Version_Procedure_Tool_Link/Version_Procedure_Tool_Link';
import { Version_Task_Tool_Link } from '../Version_Task_Tool_Link/Version_Task_Tool_Link';

export class Version_Tool extends Entity {
  ToolId!: any;
  Description!: string;
  MinorVersion!: number;
  MajorVersion!: number;
  Tool!: Tool;
  Version_Procedure_Tool_Links!: Version_Procedure_Tool_Link[];
  Version_Task_Tool_Links!: Version_Task_Tool_Link[];
  Version_EnablingObjective_Tool_Links!: Version_EnablingObjective_Tool_Link[];
}
