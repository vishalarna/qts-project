import { Position_Task } from "@models/Position_Task/Position_Task";

export class TaskWithPositionCompactVM {
  id: number;
  taskNumber: string;
  description: string;
  isReliability: boolean;
  positionIds: string[];
}
