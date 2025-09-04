import { Entity } from "../Entity";

export class SimulatorScenario extends Entity{
  simScenarioDiffID!:any;
  simScenarioTitle!:string;
  simScenarioDesc!:string;
  simScenarioDurationHours!:any;
  simScenarioDurationMins!:any;
  simScenarioUpload!:Uint8Array;
}
