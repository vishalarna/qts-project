import { SimulatorScenario_Task_VM } from "./SimulatorScenario_Task_VM";
import { SimulatorScenario_EnablingObjective_VM } from "./SimulatorScenario_EnablingObjective_VM";
import { SimulatorScenario_Procedure_VM } from "./SimulatorScenario_Procedure_VM";
import { SimulatorScenario_Task_Criteria_VM } from "./SimulatorScenario_Task_Criteria_VM";
import { SimulatorScenario_SimulatorScenarioEventAndScript_VM } from "./SimulatorScenario_SimulatorScenarioEventAndScript_VM";
import { SimulatorScenario_ILA_VM } from "./SimulatorScenario_ILA_VM";
import { SimulatorScenario_Prerequisite_VM } from "./SimulatorScenario_Prerequisite_VM";
import { SimulatorScenario_Collaborator_VM } from "./SimulatorScenario_Collaborator_VM";
import { SimulatorScenario_Position_VM } from "./SimulatorScenario_Position_VM";
import { SimulatorScenario_CollaboratorPermissions_VM } from "./SimulatorScenario_CollaboratorPermissions_VM";


export class SimulatorScenario_VM{
    id: string | null;
    title: string;
    description: string | null;
    durationHours: number | null;
    durationMinutes: number | null;
    difficultyId: string | null;
    positions: SimulatorScenario_Position_VM[] = [];
    tasks: SimulatorScenario_Task_VM[] = [];
    enablingObjectives: SimulatorScenario_EnablingObjective_VM[] = [];
    procedures: SimulatorScenario_Procedure_VM[] = [];
    taskCriterias: SimulatorScenario_Task_Criteria_VM[] = [];
    networkConfiguration: string | null;
    loadingConditions: string | null;
    generation: string | null;
    interchange: string | null;
    otherBaseCase: string | null;
    validityChecks: string | null;
    rolePlays: string | null;
    documentation: string | null;
    eventsAndScripts: SimulatorScenario_SimulatorScenarioEventAndScript_VM[] = [];
    ratingScaleId: string| null;
    operatingSkillsEvaluationMethod: string| null;
    notes: string| null;
    makeAvailableForAllILAs: boolean;
    ilAs: SimulatorScenario_ILA_VM[] = [];
    prerequisites: SimulatorScenario_Prerequisite_VM[] = [];
    collaborators: SimulatorScenario_Collaborator_VM[] = [];
    message: string| null;
    publishedDate: Date| null;
    publishedReason: string| null;
    currentUserPermissions:SimulatorScenario_CollaboratorPermissions_VM;
}