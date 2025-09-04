export class SimulatorScenario_Task_VM {
    id?: string;
    taskId: string;
    type?: string;
    number: string;
    description: string;

    constructor(taskId: string, number:string, description:string,  type:string)
    {
        this.taskId = taskId;
        this.number = number;
        this.description = description;
        this.type = type;
    }
}