import { CertificationCompactOptions } from "../Certification/CertificationCompactOptions";
import { CertifyingBody } from "./CertifyingBody";

export class CertifyingBodyCompactOptions
{
    certifyingBody!:CertifyingBody;
    certificationCompactOptions!:CertificationCompactOptions[];
}

export class CertificationILAVM{
  certificationId!:string;
  ilaId!:string;
  isIncludeSimulation!:any;
  isEmergencyOpHours!:any;
  isPartialCreditHours!:any;
  isAlreadySaved!:boolean;
  cehHours?:number;
  name!:string;
  isNerc!:boolean;
  certificationSubRequirements!: SubRequirementVM[];
}

export class SubRequirementVM {
  reqName!: string;
  reqHour!: any;
  subRequirementId!: string;
}
