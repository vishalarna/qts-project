export class SimulatorScenario_ILA_VM{
    id: string;
    ilaId: string;
    number: string;
    description: string;

    constructor(ilaId:string,description:string){
        this.ilaId=ilaId;
        this.description=description;
    }
}