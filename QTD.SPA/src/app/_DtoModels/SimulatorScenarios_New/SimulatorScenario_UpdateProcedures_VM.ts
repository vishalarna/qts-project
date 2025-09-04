import { SimulatorScenario_Procedure_VM } from "./SimulatorScenario_Procedure_VM";

export class SimulatorScenario_UpdateProcedures_VM{
    procedures: SimulatorScenario_Procedure_VM[] = [];

    setProcedures(procedure: SimulatorScenario_Procedure_VM){
        if(!this.procedures) this.procedures = [];
        var selectedTask = this.procedures?.filter(r => r.procedureId == procedure.procedureId);
        if(selectedTask.length === 0){
            this.procedures.push(procedure);
        }
    }
}