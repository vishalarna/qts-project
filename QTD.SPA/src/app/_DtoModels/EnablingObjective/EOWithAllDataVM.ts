import { EmployeeWithCountOptions } from "../Employee/EmployeeWithCountOptions";
import { ILAWithCountOptions } from "../ILA/ILAWithCountOptions";
import { ProcedureWithLinkCount } from "../Procedure/ProcedureWithLinkCount";
import { RegulatoryRequirementWithLinkCount } from "../RegulatoryRequirements/RegulatoryRequirementWithLinkCount";
import { SafetyHazardWithLinkCount } from "../SaftyHazard/SafetyHazardWithLinkCount";
import { TaskPositionWithCount } from "../Task/TaskPositionWithCount";
import { TaskWithCountOptions } from "../Task/TaskWithCountOptions";
import { TestItemWithLinkCount } from "../TestItem/TestItemWithLinkCount";

export class EOWithAllDataVM{
  ilasWithCount !: ILAWithCountOptions[];
  tasksWithCount !: TaskWithCountOptions[];
  proceduresWithCount !: ProcedureWithLinkCount[];
  rRsWithCount !: RegulatoryRequirementWithLinkCount[];
  sHsWithCount !: SafetyHazardWithLinkCount[];
  tIsWithCount !: TestItemWithLinkCount[];
  positionsWithCount !: TaskPositionWithCount[];
  employeesWithCount !: EmployeeWithCountOptions[];
}
