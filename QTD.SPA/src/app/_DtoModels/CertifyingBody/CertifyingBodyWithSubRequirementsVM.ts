import { CertifyingBody } from './CertifyingBody'; // Import CertifyingBody if defined in another file
import { CertificationSubRequirement } from '../Certification/CertificationSubRequirement'; // Import CertificationSubRequirement if defined in another file

export class CertifyingBodyWithSubRequirementsVM {
    public certifyingBody: CertifyingBody;
    public certificationSubRequirements: SubRequirementVM[];
    public isIncludeSimulation!:any;
    public isEmergencyOpHours!:any;
    public isPartialCreditHours!:any;
    public cehHours?:number;
    public certifyingBodyConsistencyResults:CertifyingBodyConsistencyResult[];
}
  
  export class SubRequirementVM {
    reqName!: string;
    reqHour!: any;
    subRequirementId!: string;
  }

  export class CertifyingBodyConsistencyResult {
    certifyingBody!: CertifyingBody;
    isConsistent!: boolean;
    certifyingBodyInconsistencies!: CertifyingBodyInconsistency[];
}

export class CertifyingBodyInconsistency {
    name!: string;
    message!: string;
    displayName!: boolean;
}