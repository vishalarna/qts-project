import { SimulatorScenario_EventAndScript_Criteria_VM } from "./SimulatorScenario_EventAndScript_Criteria_VM";

export class SimulatorScenario_EventAndScript_VM {
    id: string;
    order: number;
    title: string;
    description: string;
    initiatorId: string;
    criterias: SimulatorScenario_EventAndScript_Criteria_VM[] = [];
    time?: Date;
}