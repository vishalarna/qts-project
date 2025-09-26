export class SimulatorScenario_EnablingObjective_VM {
    id?: string;
    enablingObjectiveId: string;
    type?:string;
    number: string;
    description: string;
    includeMetaEO: boolean;

    constructor(enablingObjectiveId: string, description: string, includeMetaEO:boolean) {
        this.enablingObjectiveId = enablingObjectiveId;
        this.description = description;
        this.includeMetaEO = includeMetaEO;
    }
    
}
