import { Entity } from '../Entity';

export class EmployeeCertificationHistory extends Entity {
  changeEffectiveDate!: Date;
  changeNotes!: string;
  issueDate!: Date;
  expirationDate!: Date;
  draDate!: Date;
  certificationNumber!: string;
}
