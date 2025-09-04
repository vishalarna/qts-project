import { Certification } from '../Certification/Certification';
import { Employee } from '../Employee/Employee';
import { Entity } from '../Entity';

export class EmployeeCertification extends Entity {
  employeeId!: any;
  certificationId!: any;
  certificationNumber!: string;
  issueDate!: Date | string;
  expirationDate!: Date | string | null;
  certificationArea!: number | null;
  employee!: Employee;
  certification!: Certification;
}
