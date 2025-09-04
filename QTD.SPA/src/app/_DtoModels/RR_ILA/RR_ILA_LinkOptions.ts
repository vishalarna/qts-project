import { EnablingObjective } from "../EnablingObjective/EnablingObjective";
import { ILA } from "../ILA/ILA";
import { RegulatoryRequirement } from "../RegulatoryRequirements/RegulatoryRequirement";

export class RR_ILA_LinkOptions{
  regRequirementId!:any;
  ilaIds!:any[];
  changeNotes!:string;
  effectiveDate!:Date;
}
