import { SimulatorScenario_Task_VM } from "./SimulatorScenario_Task_VM";

export class SimulatorScenario_UpdateTasks_VM {
    includeEnablingObjectives: boolean;
    includeProcedures: boolean;
    tasks: SimulatorScenario_Task_VM[] = []

    setTasks(task: SimulatorScenario_Task_VM){
        var selectedTask = this.tasks.filter(r => r.taskId == task.taskId);
        if(selectedTask.length === 0){
            this.tasks.push(task);
        }
    }
}