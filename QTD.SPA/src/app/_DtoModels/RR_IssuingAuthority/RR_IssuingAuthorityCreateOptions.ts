import { RegulatoryRequirement } from "../RegulatoryRequirements/RegulatoryRequirement";

export class RRIssuingAuthorityCreateOptions {
  title!: string;
  description!: string;
  website!: string;
  effectiveDate!: Date;
  notes!: string;
  regulatoryRequirements!:RegulatoryRequirement[];
}
