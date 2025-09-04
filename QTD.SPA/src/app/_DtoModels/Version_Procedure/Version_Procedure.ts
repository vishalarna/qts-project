import { Entity } from '../Entity';
import { Procedure } from '../Procedure/Procedure';
import { Version_EnablingObjective_Procedure_Link } from '../Version_EnablingObjective_Procedure_Link/Version_EnablingObjective_Procedure_Link';
import { Version_Procedure_EnablingObjective_Link } from '../Version_Procedure_EnablingObjective_Link/Version_Procedure_EnablingObjective_Link';
import { Version_Procedure_SaftyHazard_Link } from '../Version_Procedure_SaftyHazard_Link/Version_Procedure_SaftyHazard_Link';
import { Version_Procedure_Tool_Link } from '../Version_Procedure_Tool_Link/Version_Procedure_Tool_Link';
import { Version_Task_Procedure_Link } from '../Version_Task_Procedure_Link/Version_Task_Procedure_Link';

export class Version_Procedure extends Entity {
  procedureId!: any;
  procedureNumber!: string;
  title!: string;
  majorVersion!: number;
  minorVersion!: number;
  procedure!: Procedure;
  version_Task_Procedure_Links!: Version_Task_Procedure_Link[];
  version_Procedure_Tool_Links!: Version_Procedure_Tool_Link;
  version_Procedure_SaftyHazard_Links!: Version_Procedure_SaftyHazard_Link;
  version_EnablingObjective_Procedure_Links!: Version_EnablingObjective_Procedure_Link;
  version_Procedure_EnablingObjective_Links!: Version_Procedure_EnablingObjective_Link;
}
