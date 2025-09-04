import { Procedure } from '../Procedure/Procedure';
import { SaftyHazard } from '../SaftyHazard/SaftyHazard';

export class Procedure_SaftyHazard_Link {
  procedureId!: any;
  saftyHazardIds: any[] = [];
  procedure!: Procedure;
  saftyHazard!: SaftyHazard;
  changeNotes!:string;
  effectiveDate?:Date;
}
