import { Entity } from '../Entity';
import { Task_History } from '../Task_History/Task_History';
import { Version_Task_EnablingObjective_Link } from '../Version_Task_EnablingObjective_Link/Version_Task_EnablingObjective_Link';
import { Version_Task_Question } from '../Version_Task_Question/Version_Task_Question';
import { Version_Task_SaftyHazard_Link } from '../Version_Task_SaftyHazard_Link/Version_Task_SaftyHazard_Link';
import { Version_Task_Step } from '../Version_Task_Step/Version_Task_Step';
import { Version_Task_Tool_Link } from '../Version_Task_Tool_Link/Version_Task_Tool_Link';

export class Version_Task extends Entity {
  taskId!: any;
  taskNumber!: string;
  description!: string;
  conditions!: string;
  standards!: string;
  critical!: boolean;
  tools!: string;
  references!: string;
  requiredTime!: number;
  versionNumber!: any;
  isInUse !: boolean;
  requalificationDueDate!: string;
  state !: number;
  requalificationRequired!: boolean
  requalificationNotes!:string;
  effectiveDate?:Date;
  task_Histories !: Task_History[];
  task!: Task;
  version_Task_Steps!: Version_Task_Step[];
  version_Task_Questions!: Version_Task_Question[];
  version_Task_Procedure_Links!: Version_Task_Question[];
  version_Task_Tool_Links!: Version_Task_Tool_Link[];
  version_Task_SaftyHazard_Links!: Version_Task_SaftyHazard_Link[];
  version_Task_EnablingObjective_Links!: Version_Task_EnablingObjective_Link[];
}
