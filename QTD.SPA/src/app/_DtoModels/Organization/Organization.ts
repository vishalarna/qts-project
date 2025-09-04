import { EmployeeOrganization } from '../EmployeeOrganization/EmployeeOrganization';
import { Entity } from '../Entity';

export class Organization extends Entity {
  name!: string;
  employeeOrganizations: EmployeeOrganization[];
}
