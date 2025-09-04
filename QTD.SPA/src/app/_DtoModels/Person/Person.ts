import { ClientUser } from '../ClientUser/ClientUser';
import { Employee } from '../Employee/Employee';
import { Entity } from '../Entity';

export class Person extends Entity {
  firstName!: string;
  middleName!: string;
  lastName!: string;
  username!: string;
  clientUser!: ClientUser;
  employee!: Employee;
  password!:any;
  address!: string;
  city!: string;
  state!: string;
  zipcode!: string;
  phoneNumber!: any;
  workLocation!: string;
  notes!: string;
  tqEvaluator!: any;
  image!:any;
  publicUser!: any; 
}
