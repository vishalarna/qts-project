import { RR_IssuingAuthorityCompact } from "../RR_IssuingAuthority/RR_IssuingAuthorityCompact";

export class RegulatoryRequirementsCompact{
  description!:string;
  title!:string;
  id!:any;
  active!:boolean;
  issuingAuthorityId!:any;
  revisionNumber!:string;
  effectiveDate!:Date;
  number!:string;
  hyperLink!:string;
  fileName!:any;
  issuingAuthorityCompact?:RR_IssuingAuthorityCompact;
}
