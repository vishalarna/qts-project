import { Entity } from "../Entity";
import { Version_EnablingObjective } from "../Version_EnablingObjective/Version_EnablingObjective";
import { Version_SaftyHazard } from "../Version_SaftyHazard/Version_SaftyHazard";

export class Version_EnablingObjective_SaftyHazard_Link extends Entity{
    Version_EnablingObjectiveId!: any;
        Version_SaftyHazardId!: any;
       Version_EnablingObjective!: Version_EnablingObjective 
       Version_SaftyHazard!: Version_SaftyHazard 
}