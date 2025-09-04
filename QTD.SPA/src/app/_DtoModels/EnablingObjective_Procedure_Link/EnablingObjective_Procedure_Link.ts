import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { Procedure } from '../Procedure/Procedure';

export class EnablingObjective_Procedure_Link {
  procedureId!: any;
  enablingObjectiveId!: any;
  procedure!: Procedure;
  enablingObjective!: EnablingObjective;
}
