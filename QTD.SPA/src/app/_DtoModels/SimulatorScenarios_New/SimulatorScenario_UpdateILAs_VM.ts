import { SimulatorScenario_ILA_VM } from "./SimulatorScenario_ILA_VM";

export class SimulatorScenario_UpdateILAs_VM {
    iLAs: SimulatorScenario_ILA_VM[] = [];

    setILAs(ila:SimulatorScenario_ILA_VM){
        if(!this.iLAs) this.iLAs =[];
        var selectedILA =this.iLAs?.filter(r=>r.ilaId == ila.ilaId);
        if(selectedILA.length === 0){
            this.iLAs.push(ila);
        }
    }
}