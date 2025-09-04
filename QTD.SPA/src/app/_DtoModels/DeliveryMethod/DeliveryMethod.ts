import { extend } from 'jquery';
import { Entity } from '../Entity';

export class DeliveryMethod extends Entity {
  name!: string;
  displayName!: string;
  isNerc!:boolean;
  isAvailableForAllIlas!:boolean;
  isUserDefined!:boolean;
  creatorIlaId!:string;
}
