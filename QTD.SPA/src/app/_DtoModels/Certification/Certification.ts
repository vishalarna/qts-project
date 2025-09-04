import { CertifyingBody } from '../CertifyingBody/CertifyingBody';
import { EmployeeCertification } from '../EmployeeCertification/EmployeeCertification';
import { Entity } from '../Entity';
import { CertificationSubRequirement } from './CertificationSubRequirement';

export class Certification extends Entity {
  name!: string;
  certAcronym !: any;
  certDesc !: string;
  renewalTimeFrame !: any;

  renewalInterval !: any;
  creditHrsReq !: any;
  creditHrs !: any;
  certSubReq !: any;
  certSubReqName !: any;
  certSubReqHours !: any;
  certMemberNum !: any;
  certifiedDate !: any;
  renewalDate !: any;
  expirationDate !: any;

  allowRolloverHours!: any;
  additionalHours!: any;
  effectiveDate:any;


  certifyingBodyId!: any;
  certifyingBody!: CertifyingBody;
  employeeCertifications!: EmployeeCertification[];
  employeeCertificationHistory:any[];
}
