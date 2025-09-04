import { TaskSequenceModel } from "./TaskSequenceModel";

export class ILATaskObjectiveLinkUpdateOptions {
  taskLinks: TaskSequenceModel[];
  isIncludeProcedures: boolean = false;
  isIncludeEos: boolean = false;
  isIncludeMetaTask: boolean = false;
}
  