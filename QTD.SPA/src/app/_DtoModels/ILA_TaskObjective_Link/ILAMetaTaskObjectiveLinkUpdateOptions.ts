import { TaskSequenceModel } from "./TaskSequenceModel";

export class ILAMetaTaskObjectiveLinkUpdateOptions {
  metaTaskLinks: TaskSequenceModel[];
  isIncludeTaskAndEos: boolean = false;
}
  