import { EnablingObjective } from "../EnablingObjective/EnablingObjective";
import { RegulatoryRequirement } from "../RegulatoryRequirements/RegulatoryRequirement";

export class RR_EO_LinkOptions{
  regulatoryRequirementId!: any;
  EOIds!: any[];
  changeNotes!:string;
  effectiveDate!:Date;
  regulatoryRequirement!: RegulatoryRequirement;
  enablingObjective!: EnablingObjective;
}
