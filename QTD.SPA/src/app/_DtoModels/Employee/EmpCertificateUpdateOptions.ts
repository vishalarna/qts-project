export class EmpCertificateUpdateOptions {        
        employeeId: number;
        empCertificationId:any;
        certificationId: number;

        certificationNumber: string;

        expirationDate: Date | string | null;

        issueDate: Date | string;

        renewalDate: Date | string | null;
        rolloverHours: number | null;

        effectedDate: Date | string;

        reason: string;

    }