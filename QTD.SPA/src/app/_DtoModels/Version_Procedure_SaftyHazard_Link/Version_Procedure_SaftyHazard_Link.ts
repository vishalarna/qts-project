import { Entity } from '../Entity';
import { Version_Procedure } from '../Version_Procedure/Version_Procedure';
import { Version_SaftyHazard } from '../Version_SaftyHazard/Version_SaftyHazard';

export class Version_Procedure_SaftyHazard_Link extends Entity {
  version_SaftyHazardId!: any;
  version_ProcedureId!: any;
  version_SaftyHazard!: Version_SaftyHazard;
  version_Procedure!: Version_Procedure;
}
