export class EmployeeCertificateCreateOptions {
  certificationId!: any;
  certifyingBodyId!: any;
  certificationNumber!: string;
  issueDate!: Date | string;
  expirationDate!: Date | string | null;
}
