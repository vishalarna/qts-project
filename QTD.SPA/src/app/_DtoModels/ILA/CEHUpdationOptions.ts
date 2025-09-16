import { SubRequirementVM } from "@models/CertifyingBody/CertifyingBodyCompactOptions";

export class CEHUpdateOptions {
    certificationId: string
    isIncludeSimulation! : boolean;
    isEmergencyOpHours! : boolean;
    isPartialCreditHours! : boolean;
    cehHours: number;
    subRequirements: SubRequirementVM[];
}