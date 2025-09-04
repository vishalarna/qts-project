import { SimulatorScenario_EnablingObjective_VM } from "./SimulatorScenario_EnablingObjective_VM";
import { SimulatorScenario_Procedure_VM } from "./SimulatorScenario_Procedure_VM";
import { SimulatorScenario_Task_VM } from "./SimulatorScenario_Task_VM";

export class SimulatorScenario_TasksResponseVM{
    simulatorScenarioTaskVMs:SimulatorScenario_Task_VM[];
    simulatorScenarioProcedureVMs:SimulatorScenario_Procedure_VM[];
    simulatorScenarioEnablingObjectiveVMs:SimulatorScenario_EnablingObjective_VM[];
}