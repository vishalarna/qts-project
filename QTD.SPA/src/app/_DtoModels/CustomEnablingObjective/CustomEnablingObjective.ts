import { ILACustomObjective_LinkOptions } from 'src/app/_DtoModels/ILACustomObjective_Link/ILACustomObjective_LinkOptions';
import { Entity } from '../Entity';

export class CustomEnablingObjective extends Entity {
  eoTopicId: number;

  description: string;

  isAddtoEO: boolean;

  customEONumber!:any;

  ila_customObjectives_link : ILACustomObjective_LinkOptions[];
  fullNumber!: string;
}
