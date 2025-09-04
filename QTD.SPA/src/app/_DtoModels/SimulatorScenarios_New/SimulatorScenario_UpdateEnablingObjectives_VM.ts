import { SimulatorScenario_EnablingObjective_VM } from './SimulatorScenario_EnablingObjective_VM';

export class SimulatorScenario_UpdateEnablingObjectives_VM {
  enablingObjectives: SimulatorScenario_EnablingObjective_VM[];

  setEnablingObjectives(eo: SimulatorScenario_EnablingObjective_VM) {
    if(!this.enablingObjectives) this.enablingObjectives = [];
    var selectedEo = this.enablingObjectives.filter((r) => r.enablingObjectiveId == eo.enablingObjectiveId);
    if (selectedEo.length == 0) {
      this.enablingObjectives.push(eo);
    }
  }
}
