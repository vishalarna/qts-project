import { SubDutyAreaVm } from "@models/SubdutyArea/SubDutyAreaVm";

export class DutyAreaVm {
  id: number;
  title: string;
  description: string;
  letter: string;
  number: number;
  subDutyArea: SubDutyAreaVm[];
}