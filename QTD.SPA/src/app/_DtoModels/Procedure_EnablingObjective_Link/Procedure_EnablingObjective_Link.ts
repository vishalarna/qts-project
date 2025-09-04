import { EnablingObjective } from '../EnablingObjective/EnablingObjective';
import { Procedure } from '../Procedure/Procedure';

export class Procedure_EnablingObjective_Link {
  procedureId!: any;
  EOIds!: any[];
  procedure!: Procedure;
  enablingObjective!: EnablingObjective;
}
