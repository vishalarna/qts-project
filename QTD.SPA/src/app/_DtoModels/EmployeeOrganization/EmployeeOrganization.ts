import { Employee } from '../Employee/Employee';
import { Entity } from '../Entity';
import { Organization } from '../Organization/Organization';

export class EmployeeOrganization extends Entity {
  employeeId!: any;
  organizationId!: any;
  employee!: Employee;
  organization!: Organization;
  isManager : boolean;
  firstName:any;
  lastName:any;
}
