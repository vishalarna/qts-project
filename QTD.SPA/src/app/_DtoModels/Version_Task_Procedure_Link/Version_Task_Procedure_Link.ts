import { Entity } from '../Entity';
import { Version_Procedure } from '../Version_Procedure/Version_Procedure';
import { Version_Task } from '../Version_Task/Version_Task';

export class Version_Task_Procedure_Link extends Entity {
  version_TaskId!: any;
  version_ProcedureId!: any;
  version_Task!: Version_Task;
  version_Procedure!: Version_Procedure;
}
