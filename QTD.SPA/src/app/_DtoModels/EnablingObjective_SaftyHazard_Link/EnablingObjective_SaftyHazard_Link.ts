import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { SaftyHazard } from '../SaftyHazard/SaftyHazard';

export class EnablingObjective_SaftyHazard_Link {
  saftyHazardId!: any;
  enablingObjectiveId!: any;
  saftyHazard!: SaftyHazard;
  enablingObjective!: EnablingObjective;
}
