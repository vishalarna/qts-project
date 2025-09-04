import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { EnablingObjectiveHistory } from '../EnablingObjective/EnablingObjectiveHistory';
import { Entity } from '../Entity';
import { Version_EnablingObjective_Procedure_Link } from '../Version_EnablingObjective_Procedure_Link/Version_EnablingObjective_Procedure_Link';
import { Version_EnablingObjective_SaftyHazard_Link } from '../Version_EnablingObjective_SaftyHazard_Link/Version_EnablingObjective_SaftyHazard_Link';
import { Version_EnablingObjective_Tool_Link } from '../Version_EnablingObjective_Tool_Link/Version_EnablingObjective_Tool_Link';
import { Version_Procedure_EnablingObjective_Link } from '../Version_Procedure_EnablingObjective_Link/Version_Procedure_EnablingObjective_Link';
import { Version_Task_EnablingObjective_Link } from '../Version_Task_EnablingObjective_Link/Version_Task_EnablingObjective_Link';

export class Version_EnablingObjective extends Entity {
  enablingObjectiveId!: any;
  enablingObjectiveNumber!: string;
  description!: string;
  enablingObjective!: EnablingObjective;
  versionNumber !: string;
  enablingObjectiveHistories !: EnablingObjectiveHistory[];
  state !: number;
  isInUse !: boolean;
  version_EnablingObjective_Tool_Links!: Version_EnablingObjective_Tool_Link[];
  version_Task_EnablingObjective_Links!: Version_Task_EnablingObjective_Link[];
  version_EnablingObjective_SaftyHazard_Links!: Version_EnablingObjective_SaftyHazard_Link[];
  version_EnablingObjective_Procedure_Links!: Version_EnablingObjective_Procedure_Link[];
  version_Procedure_EnablingObjective_Links!: Version_Procedure_EnablingObjective_Link[];
}
