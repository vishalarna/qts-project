import { Entity } from '../Entity';
import { Version_Procedure } from '../Version_Procedure/Version_Procedure';
import { Version_Tool } from '../Version_Tool/Version_Tool';

export class Version_Procedure_Tool_Link extends Entity {
  version_ProcedureId!: any;
  version_ToolId!: any;
  version_Procedure!: Version_Procedure;
  version_Tool!: Version_Tool;
}
