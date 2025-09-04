import { ILA_Topic_Link } from '@models/ILA_Topic_Link/ILA_Topic_Link';
import { Entity } from '../Entity';

export class ILA_Topic extends Entity {
  name!: string;
  isPriority!: boolean;
  ilA_Topic_Links!: ILA_Topic_Link[];
}
