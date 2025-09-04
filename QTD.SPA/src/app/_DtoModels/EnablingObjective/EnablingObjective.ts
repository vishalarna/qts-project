import { ILA_EnablingObjective_LinkOptions } from 'src/app/_DtoModels/ILA_EnablingObjective_Link/ILA_EnablingObjective_LinkOptions';
import { EnablingObjective_Category } from '../EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_Procedure_Link } from '../EnablingObjective_Procedure_Link/EnablingObjective_Procedure_Link';
import { EnablingObjective_SaftyHazard_Link } from '../EnablingObjective_SaftyHazard_Link/EnablingObjective_SaftyHazard_Link';
import { EnablingObjective_SubCategory } from '../EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from '../EnablingObjective_Topic/EnablingObjective_Topic';
import { Entity } from '../Entity';
import { Procedure_EnablingObjective_Link } from '../Procedure_EnablingObjective_Link/Procedure_EnablingObjective_Link';
import { Task_EnablingObjective_Link } from '../Task_EnablingObjective_Link/Task_EnablingObjective_Link';

export class EnablingObjective extends Entity {
  categoryId!: any;
  subCategoryId!: any;
  topicId!: any;
  number!: any;
  fullNumber!:string;
  statement!: any;
  isMetaEO!: boolean;
  isSkillQualification !: boolean;
  description!:string;
  active !: boolean;
  criteria!:string;
  references!: string;
  conditions!: string;
  effectiveDate!:any;

  enablingObjective_Topic!: EnablingObjective_Topic;
  enablingObjective_Category!: EnablingObjective_Category;
  enablingObjective_SubCategory!: EnablingObjective_SubCategory;

  task_EnablingObjective_Links!: Task_EnablingObjective_Link[];
  procedure_EnablingObjective_Links!: Procedure_EnablingObjective_Link[];
  enablingObjective_SaftyHazard_Links!: EnablingObjective_SaftyHazard_Link[];
  enablingObjective_Procedure_Links!: EnablingObjective_Procedure_Link[];
  ila_enablingObjective_Links!:ILA_EnablingObjective_LinkOptions[];
}
