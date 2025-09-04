import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { Entity } from '../Entity';

export class EnablingObjective_Step extends Entity {
  eoId!: any;
  description!: string;
  number!: number;
  active!: boolean;
  eo!: EnablingObjective;
}
