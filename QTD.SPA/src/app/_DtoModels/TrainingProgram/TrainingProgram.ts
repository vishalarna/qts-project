import { Entity } from '../Entity';
import { Position } from '../Position/Position';
import { TrainingProgramType } from '../TrainingProgramType/TrainingProgramType';
import { TrainingProgram_ILA_Links } from './TrainingProgram_ILA_Links';

export class TrainingProgram extends Entity
{
  positionId: any;

  version: number;
  tpVersionNo!:any;
  year!:string;

  programTitle: string;

  programType: string;

  startDate: Date | string;

  endDate: Date | string | null;

  position: Position;
  trainingProgram_ILA_Links!:TrainingProgram_ILA_Links;

  trainingProgramType!:TrainingProgramType;
  trainingProgramTypeId!:any;
}
