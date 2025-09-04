export interface CertificationLatestActivityVM{
    certId: number;

    certifyingBodyID: number;

    certAcronym: string;

    name: string;

    activityDesc: string;

    createdBy: string;
  
    createdDate: Date | string | null;
  
    modifiedBy: string;
  
    modifiedDate: Date | string | null;
}