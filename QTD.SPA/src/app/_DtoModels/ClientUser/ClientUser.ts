import { Entity } from '../Entity';
import { Person } from '../Person/Person';

export class ClientUser extends Entity {
  personId!: any;
  person!: Person;
}
