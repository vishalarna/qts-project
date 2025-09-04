import { Entity } from "../Entity";

export class NERCTargetAudience extends Entity {
  name!:string;
  isOther!:boolean;
  otherName!:string;
}
