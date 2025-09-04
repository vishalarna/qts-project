import { Entity } from "../Entity";

export class RegulatoryRequirement extends Entity{
  issuingAuthorityId!:any;
  number!:any;
  title!:string;
  description!:string;
  revisionNumber!:string;
  effectiveDate!:Date;
  uploads!:BlobPart;
  fileName:any;
  active!:boolean;
}
