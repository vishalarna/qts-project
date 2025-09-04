import { Entity } from "../Entity";
import { ILA } from "../ILA/ILA";

export class TrainingProgram_ILA_Links extends Entity {

    trainingProgramId!:any;
    ilaId!:any;
    ila:ILA;
}
