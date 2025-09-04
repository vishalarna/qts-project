import { EnablingObjective_SaftyHazard_Link } from '../EnablingObjective_SaftyHazard_Link/EnablingObjective_SaftyHazard_Link';
import { Entity } from '../Entity';
import { Procedure_SaftyHazard_Link } from '../Procedure_SaftyHazard_Link/Procedure_SaftyHazard_Link';
import { SaftyHazard_Abatement } from '../SaftyHazard_Abatement/SaftyHazard_Abatement';
import { SaftyHazard_Category } from '../SaftyHazard_Category/SaftyHazard_Category';
import { SaftyHazard_Control } from '../SaftyHazard_Control/SaftyHazard_Control';
import { SafetyHazard_Set } from '../SaftyHazard_Set/SafetyHazard_Set';
import { Task_SaftyHazard_Link } from '../Task_SaftyHazard_Link/Task_SaftyHazard_Link';

export class SaftyHazard extends Entity {
  saftyHazardCategoryId!: any;
  number!: string;
  title!: string;
  files!:string;
  fileName!:any;
  text!:string;
  revisionNumber?:string;
  effectiveDate?:Date;
  description!: string;
  hyperLinks?:string;
  personalProtectiveEquipment!: string;
  active!: boolean;
  majorVersion!: number;
  minorVersion!: number;
  saftyHazard_Category!: SaftyHazard_Category;
  task_SaftyHazard_Links!: Task_SaftyHazard_Link[];
  procedure_SaftyHazard_Links!: Procedure_SaftyHazard_Link[];
  enablingObjective_SaftyHazard_Links!: EnablingObjective_SaftyHazard_Link[];
  saftyHazard_Abatements!: SaftyHazard_Abatement[];
  saftyHazard_Controls!: SaftyHazard_Control[];
  saftyHazard_Sets!:SafetyHazard_Set[];
}
