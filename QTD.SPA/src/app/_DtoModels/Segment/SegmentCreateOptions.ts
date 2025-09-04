
export class SegmentCreateOptions{
  title!:string;
  duration!:string;
  isNercStandard!:boolean;
  isPartialCredit!:boolean;
  isNercOperatingTopics!:boolean;
  isNercSimulation!:boolean;
  Content!:string;
  uploads!:Uint8Array;
  segmentId?:any;
}
