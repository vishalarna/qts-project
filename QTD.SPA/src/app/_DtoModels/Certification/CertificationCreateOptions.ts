export class CertificationCreateOptions {


  CertifyingBodyId: number;

  CertAcronym!: any;

  Name!: string;

  CertDesc: string;

  RenewalTimeFrame : any;

  RenewalInterval : any;
  CreditHrsReq : any;

  CreditHrs: any;

  CertSubReq: any;

  CertSubReqName: any=[];

  CertSubReqHours: any=[];  

  CertMemberNum: number;

  CertifiedDate: any;

  RenewalDate: any;


  ExpirationDate: any;
  AllowRolloverHours: any;
  AdditionalHours: any;

  Notes: string;

  EffectiveDate: Date | string;
  CertificationSubRequirementsIds:any[] = [];

}
