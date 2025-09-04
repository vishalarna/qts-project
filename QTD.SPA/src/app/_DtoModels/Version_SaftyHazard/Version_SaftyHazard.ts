import { Entity } from '../Entity';
import { SaftyHazard } from '../SaftyHazard/SaftyHazard';
import { Version_EnablingObjective_SaftyHazard_Link } from '../Version_EnablingObjective_SaftyHazard_Link/Version_EnablingObjective_SaftyHazard_Link';
import { Version_Procedure_SaftyHazard_Link } from '../Version_Procedure_SaftyHazard_Link/Version_Procedure_SaftyHazard_Link';
import { Version_SaftyHazard_Abatement } from '../Version_SaftyHazard_Abatement/Version_SaftyHazard_Abatement';
import { Version_SaftyHazard_Control } from '../Version_SaftyHazard_Control/Version_SaftyHazard_Control';
import { Version_Task_SaftyHazard_Link } from '../Version_Task_SaftyHazard_Link/Version_Task_SaftyHazard_Link';

export class Version_SaftyHazard extends Entity {
  saftyHazardId!: any;
  title!: string;
  description!: string;
  personalProtectiveEquipment!: string;
  minorVersion!: number;
  majorVersion!: number;
  saftyHazard!: SaftyHazard;
  version_SaftyHazard_Abatements!: Version_SaftyHazard_Abatement[];
  version_SaftyHazard_Controls!: Version_SaftyHazard_Control[];
  version_Task_SaftyHazard_Links!: Version_Task_SaftyHazard_Link[];
  version_Procedure_SaftyHazard_Links!: Version_Procedure_SaftyHazard_Link[];
  version_EnablingObjective_SaftyHazard_Links!: Version_EnablingObjective_SaftyHazard_Link[];
}
