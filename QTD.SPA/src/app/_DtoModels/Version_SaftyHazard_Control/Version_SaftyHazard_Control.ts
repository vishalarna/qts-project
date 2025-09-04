import { Entity } from "../Entity";
import { Version_SaftyHazard } from "../Version_SaftyHazard/Version_SaftyHazard";

export class Version_SaftyHazard_Control extends Entity {
    version_SaftyHazardId!: any;
    description!: string;
    number!: number;
    version_SaftyHazard!: Version_SaftyHazard;
  }