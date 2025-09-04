import { CustomEnablingObjective } from "../CustomEnablingObjective/CustomEnablingObjective";
import { EnablingObjective } from "../EnablingObjective/EnablingObjective";
import { Entity } from "../Entity";
import { Task } from "../Task/Task";

export class Segment_ObjectiveLink extends Entity {
  segmentId!: any;
  taskIds!: any;
  enablingObjectiveIds!: any;
  task!:Task;
  enablingObjective!:EnablingObjective;
  customEnablingObjective!:CustomEnablingObjective;
  order?:any;
}
