import { Entity } from "../Entity";
import { ClassSchedules } from "../SchedulesClassses/ClassSchedules";
import { DeliveryMethod } from "../DeliveryMethod/DeliveryMethod";
import { CBT } from "../ILA_CBT/CBT";
import { ILATraineeEvaluation } from "../ILATraineeEvaluation/ILATraineeEvaluation";
import { TestReleaseEMPSettings } from "../EMPSettings/TestReleaseEMPSettings";
import { ILA_Topic_Link } from "@models/ILA_Topic_Link/ILA_Topic_Link";

export class ILA extends Entity{
  name:string
  nickName:string
  number:any
  description:string
  image:string
  providerId:any
  provider:any
  ilaCertificationLinks:any
  ilA_Topic_Links:ILA_Topic_Link[];
  isSelfPaced:boolean
  isOptional:boolean
  isPublished:boolean
  useForEMP!:boolean
  publishDate?:Date
  deliveryMethodId:any
  hasPilotData:boolean
  isProgramManual:boolean
  submissionDate?:Date
  approvalDate?:Date
  expirationDate?:Date
  uploads?:Uint8Array;
  classSize?: number;
  ilA_SelfRegistrationOption!: any;
  classSchedules!:ClassSchedules[];
  deliveryMethod!:DeliveryMethod;
  cbTs: Array<CBT>;
  cbtRequiredForCourse: boolean;
  trainingPlan!:string;
  totalTrainingHours?:number;
  ilaTraineeEvaluations!:ILATraineeEvaluation[];
  testReleaseEMPSettings!:TestReleaseEMPSettings;
  wriitenOrOnlineAssessmentTool?:boolean;
  performAssessmentTool?:boolean;
  prerequisites:string;
}
