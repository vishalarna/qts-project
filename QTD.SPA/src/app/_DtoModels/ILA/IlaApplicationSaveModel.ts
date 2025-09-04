import { Entity } from "../Entity";

export class IlaApplicationSaveModel extends Entity{
  ilaId!:any;
  startDate!:any;
  applicationSubmissionDatertDate!:any;
  approvalDate!:any;
  expirationDate!:any;
  hasPilotData!:any;
  hasPilotDataNA!:any;
  doesActivityConform!:any;
  // assesmentIds!:any[];
  nercTargetIds!:any[];
  trainingTopicIds!:any[];
  otherNercTargetAudience!:any;
  otherAssesmentTool!:any;
  wriitenOrOnlineAssessmentTool?:boolean;
  performAssessmentTool?:boolean;
}
