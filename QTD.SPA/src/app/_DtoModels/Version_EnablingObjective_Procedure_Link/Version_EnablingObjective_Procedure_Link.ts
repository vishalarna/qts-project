import { Entity } from '../Entity';
import { Version_EnablingObjective } from '../Version_EnablingObjective/Version_EnablingObjective';
import { Version_Procedure } from '../Version_Procedure/Version_Procedure';

export class Version_EnablingObjective_Procedure_Link extends Entity {
  version_EnablingObjectiveId!: any;
  version_ProcedureId!: any;
  version_EnablingObjective!: Version_EnablingObjective;
  version_Procedure!: Version_Procedure;
}
