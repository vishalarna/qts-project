import { Entity } from "../Entity";

export class Segment extends Entity{
  title!:string;
  duration!:string;
  isPartialCredit!:boolean;
  isNercStandard!:boolean;
  isNercOperatingTopics!:boolean;
  isNercSimulation!:boolean;
  content!:string;
  uploads!:Uint8Array;
  segmentObjective_Link:any[];
}
