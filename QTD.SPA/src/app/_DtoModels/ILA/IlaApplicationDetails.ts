import { Entity } from "../Entity";

export class IlaApplicationDetails extends Entity{
  startDate!:any;
  hasPilotData:boolean;
  assessmentToolIds:any[];
  targetedAudienceIds:any[];
  applicationSubmissionDate:any[];
  approvalDate:any;
  expirationDate:any;
  operatorsTrainingTopics:any[];
  otherNercTargetAudience!:any;
  otherAssesmentTool!:any;
}
