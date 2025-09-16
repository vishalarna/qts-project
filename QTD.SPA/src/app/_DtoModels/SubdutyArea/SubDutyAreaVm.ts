import { TaskVm } from "@models/Task/TaskVm";

export class SubDutyAreaVm {
  id: number;
  dutyAreaId: number;
  description: string;
  subNumber: number;
  title: string;
  tasks: TaskVm[];
}