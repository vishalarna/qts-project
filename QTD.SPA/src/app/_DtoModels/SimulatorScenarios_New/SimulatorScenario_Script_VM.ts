import { SimulatorScenario_Script_Criteria_VM } from "./SimulatorScenario_Script_Criteria_VM";

export class SimulatorScenario_Script_VM {
    id: string;
    title: string;
    description: string;
    initiatorId: string;
    criterias: SimulatorScenario_Script_Criteria_VM[] = [];
    time?: Date;
    eventId:string;
      initiatorOther: boolean;
    initiatorInstructor: boolean;
}