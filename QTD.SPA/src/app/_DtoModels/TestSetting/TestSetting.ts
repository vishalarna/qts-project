import { Entity } from "../Entity";

export class TestSetting extends Entity{
  description!:string;
  isDefault!:boolean;
  isOverride!:boolean;
}
