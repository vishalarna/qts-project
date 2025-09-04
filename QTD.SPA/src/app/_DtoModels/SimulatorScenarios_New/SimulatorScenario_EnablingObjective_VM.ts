export class SimulatorScenario_EnablingObjective_VM {
    id?: string;
    enablingObjectiveId: string;
    type?:string;
    number: string;
    description: string;

    constructor(enablingObjectiveId: string, description: string) {
        this.enablingObjectiveId = enablingObjectiveId;
        this.description = description;
    }
    
}
