import { Entity } from "../Entity";
import { TrainingGroup_Category } from "../TrainingGroup_Category/TrainingGroup_Category";

export class TrainingGroup extends Entity{
  categoryId!:any;
  groupNumber!:number;
  groupName!:string;
  groupDescription!:string;
  hyperLink!:string;
  pDF!:Uint8Array;
  trainingGroup_Category!:TrainingGroup_Category;
}
