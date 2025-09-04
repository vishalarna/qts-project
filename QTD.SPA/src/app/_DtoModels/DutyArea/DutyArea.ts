import { extend } from 'jquery';
import { Entity } from '../Entity';
import { SubdutyArea } from '../SubdutyArea/SubdutyArea';

export class DutyArea extends Entity {
  title!: string;
  description!: string;
  letter!: string;
  number!: number;
  active!: boolean;
  effectiveDate!: Date;
  reasonForRevision!: string;
  subdutyAreas!: SubdutyArea[];
}
