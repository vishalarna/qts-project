import { Entity } from "../Entity";
import { NercStandardMembers } from "../NercStandardMembers/NercStandardMembers";

export class NercStandard extends Entity{
  name!:string;
  isNercStandard!:boolean;
  isUserDefined!:boolean;
  cehHours?:number;
  nercStandardMembers!:NercStandardMembers[];
}
