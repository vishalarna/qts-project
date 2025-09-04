import { SimulatorScenario_Prerequisite_VM } from "./SimulatorScenario_Prerequisite_VM";

export class SimulatorScenario_UpdatePrerequisites_VM {
    prerequisites: SimulatorScenario_Prerequisite_VM[] = [];

    setPrerequisites(prerequisite:SimulatorScenario_Prerequisite_VM){
        if(!this.prerequisites) this.prerequisites =[];
        var selectedprerequisites =this.prerequisites?.filter(r=>r.ilaId == prerequisite.ilaId);
        if(selectedprerequisites.length === 0){
            this.prerequisites.push(prerequisite);
        }
    }

}