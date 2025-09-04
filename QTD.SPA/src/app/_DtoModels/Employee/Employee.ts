import { EmployeeCertification } from '../EmployeeCertification/EmployeeCertification';
import { EmployeeOrganization } from '../EmployeeOrganization/EmployeeOrganization';
import { EmployeePosition } from '../EmployeePosition/EmployeePosition';
import { Entity } from '../Entity';
import { Person } from '../Person/Person';

export class Employee extends Entity {
  personId!: any;
  employeeNumber!: string;
  address: string;

  city: string;

  state: string;

  zipCode: string;

  phoneNumber: number;

  workLocation: string;

  notes: string;

  password: string;

  tqEqulator: boolean;

  idpReviewInformation?:string;

  isCertificationRequired!:boolean;
  person!: Person;
  employeePositions!: EmployeePosition[];
  employeeOrganizations!: EmployeeOrganization[];
  employeeCertifications!: EmployeeCertification[];
  classSchedule_Employee:number;
  taskQualifications:any[];
  inactiveDate:any;
  reason:any;
  publicUser: boolean;
}
