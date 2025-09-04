export class ILA_StudentEvaluation_LinkOptions {
  iLAId!:any;
  studentEvalFormIDs:any[] = [];
  studentEvalAvailabilityID!:any;
  studentEvalAudienceID!:any;
  isAllQuestionMandatory!:boolean;
}


export class ILA_StudentEvaluation_LinkOption {
  studentEvalFormID!:any;
  studentEvalAvailabilityID!:any;
  studentEvalAudienceID!:any;
  isAllQuestionMandatory!:boolean;
  ilaStudenEvaluationLinkId!:any;
  isEvalRemove:boolean;
}
