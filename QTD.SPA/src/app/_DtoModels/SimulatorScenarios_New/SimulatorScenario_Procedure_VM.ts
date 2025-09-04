import { Entity } from "@models/Entity";

export class SimulatorScenario_Procedure_VM{
    id?: string;
    procedureId: string;
    number: string;
    description: string;

    constructor(procedureId: string,  description: string){
        this.procedureId = procedureId;
        this.description = description;
    }
}