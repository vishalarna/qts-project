import { EnablingObjective_Procedure_Link } from '../EnablingObjective_Procedure_Link/EnablingObjective_Procedure_Link';
import { Entity } from '../Entity';
import { Procedure_EnablingObjective_Link } from '../Procedure_EnablingObjective_Link/Procedure_EnablingObjective_Link';
import { Procedure_IssuingAuthority } from '../Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { Procedure_SaftyHazard_Link } from '../Procedure_SaftyHazard_Link/Procedure_SaftyHazard_Link';
import { Task_Procedure_Link } from '../Task_Procedure_Link/Task_Procedure_Link';

export class Procedure extends Entity {
  issuingAuthorityId!: any;
  title!: string;
  number!: string;
  description!: string;
  revisionNumber!: string;
  hyperlink!: string;
  effectiveDate!: Date;
  proceduresFileUpload!: string;
  isActive!: boolean;
  isDeleted!: boolean;
  isPublished!: boolean;
  file!:any;
  fileName!:string;

  procedure_IssuingAuthority!: Procedure_IssuingAuthority;
  task_Procedure_Links!: Task_Procedure_Link[];
  procedure_SaftyHazard_Links!: Procedure_SaftyHazard_Link[];
  procedure_EnablingObjective_Links!: Procedure_EnablingObjective_Link[];
  enablingObjective_Procedure_Links!: EnablingObjective_Procedure_Link[];
}
