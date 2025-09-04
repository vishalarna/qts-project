import { SubRequirementUpdateOptions } from "@models/Certification/SubRequirementUpdateOptions";

export class CertifyingBodyCEHUpdateOptions {
    isIncludeSimulation! : boolean;
    isEmergencyOpHours! : boolean;
    isPartialCreditHours! : boolean;
    cehHours: number;
    subRequirements: SubRequirementUpdateOptions[];
}