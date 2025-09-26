import { SimulatorScenario_CollaboratorPermissions_VM } from "./SimulatorScenario_CollaboratorPermissions_VM";
import { SimulatorScenario_Collaborator_VM } from "./SimulatorScenario_Collaborator_VM";

export class SimulatorScenarioOverview_SimulatorScenario_VM {
    id:string;
    title:string;
    ilAs:string;
    positions:string;
    status:string;
    active:boolean;
    difficulty:string;
    collaborators:SimulatorScenario_Collaborator_VM[];
    currentUserPermissions: SimulatorScenario_CollaboratorPermissions_VM;
    providerIds: string[] = [];
}