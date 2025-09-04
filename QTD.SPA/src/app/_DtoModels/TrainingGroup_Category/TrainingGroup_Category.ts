import { Entity } from "../Entity";
import { TrainingGroup } from "../TrainingGroup/TrainingGroup";

export class TrainingGroup_Category extends Entity{
  title !: string;
  description !: string;
  effectiveDate !: Date;
  note !: string;
  trainingGroups !: TrainingGroup[];
}
