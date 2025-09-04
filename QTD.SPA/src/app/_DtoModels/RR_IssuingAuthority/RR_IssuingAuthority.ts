import { Entity } from "../Entity";
import { RegulatoryRequirement } from "../RegulatoryRequirements/RegulatoryRequirement";

export class RRIssuingAuthority extends Entity {
  title!: string;
  description!: string;
  website!: string;
  effectiveDate!: Date;
  notes!: string;
  action!:boolean;
  regulatoryRequirements!:RegulatoryRequirement[];
}
