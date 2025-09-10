import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SimulatorScenario_CollaboratorPermissions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_CollaboratorPermissions_VM';
import { SimulatorScenario_Collaborator_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Collaborator_VM';
import { SimulatorScenario_Difficulty_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Difficulty_VM';
import { SimulatorScenario_EnablingObjective_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_EnablingObjective_VM';
import { SimulatorScenario_EventAndScript_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_EventAndScript_VM';
import { SimulatorScenario_ILA_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_ILA_VM';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SimulatorScenario_Prerequisite_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Prerequisite_VM';
import { SimulatorScenario_Procedure_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Procedure_VM';
import { SimulatorScenario_Status_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Status_VM';
import { SimulatorScenario_Task_Criteria_By_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_By_Position_VM';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';
import { SimulatorScenario_TasksResponseVM } from '@models/SimulatorScenarios_New/SimulatorScenario_TasksResponseVM';
import { SimulatorScenario_UpdateCollaborators_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateCollaborators_VM';
import { SimulatorScenario_UpdateEnablingObjectives_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateEnablingObjectives_VM';
import { SimulatorScenario_UpdateEventsAndScriptsOrder_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateEventsAndScriptsOrder_VM';
import { SimulatorScenario_UpdateILAs_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateILAs_VM';
import { SimulatorScenario_UpdatePositions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePositions_VM';
import { SimulatorScenario_UpdatePrerequisites_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePrerequisites_VM';
import { SimulatorScenario_UpdateProcedures_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateProcedures_VM';
import { SimulatorScenario_UpdateTasks_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateTasks_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SimulatorScenariosService {
  baseUrlWithSim = environment.QTD + `simulatorScenarios`;
  baseUrl = environment.QTD;
  constructor(private http: HttpClient) {}

  getAllDifficultyAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `simulatorScenarioDifficulties`)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Difficulty_VM[];
        })
      )
      );
  }

  getAsync(id: string) {
    return firstValueFrom(this.http
      .get(this.baseUrlWithSim + `/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_VM;
        })
      )
      );
  }

  createAsync(options: SimulatorScenario_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_VM;
        })
      )
      );
  }

  updateAsync(id: string, options: SimulatorScenario_VM) {
    return firstValueFrom(this.http
      .put(this.baseUrlWithSim + `/${id}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_VM;
        })
      )
      );
  }

  linkPosistionToScenarios(id: any, options: SimulatorScenario_UpdatePositions_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/positions`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Position_VM[];
        })
      )
      );
  }

  linkTaskToScenarios(id: any, options: SimulatorScenario_UpdateTasks_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/tasks`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_TasksResponseVM;
        })
      )
      );
  }

  linkEOsToScenarios(
    id: any,
    options: SimulatorScenario_UpdateEnablingObjectives_VM
  ) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/enablingObjectives`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_EnablingObjective_VM[];
        })
      )
      );
  }

  linkProcToScenarios(id: any, options: SimulatorScenario_UpdateProcedures_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/procedures`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Procedure_VM[];
        })
      )
      );
  }

  linkILAsToScenarios(id: any, options: SimulatorScenario_UpdateILAs_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/ilas`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_ILA_VM[];
        })
      )
      );
  }

  linkPreReqsToScenarios(id: any, options: SimulatorScenario_UpdatePrerequisites_VM) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/prerequisites`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Prerequisite_VM[];
        })
      )
      );
  }

  getTaskCriteriaByPosition(id: string, positionId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrlWithSim + `/${id}/positions/${positionId}/taskCriterias`)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Task_Criteria_By_Position_VM[];
        })
      )
      );
  }

  getAllTaskCriterias(id: string) {
    return firstValueFrom(this.http
      .get(this.baseUrlWithSim + `/${id}/positions/allTaskCriterias`)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Task_Criteria_By_Position_VM[];
        })
      )
      );
  }

  createEventAndScript(
    id: string,
    options: SimulatorScenario_EventAndScript_VM
  ) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/eventsAndScripts`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_EventAndScript_VM;
        })
      )
      );
  }

  createTaskCriteriaAsync(
    id: string,
    options: SimulatorScenario_Task_Criteria_VM
  ) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/taskCriterias`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Task_Criteria_VM;
        })
      )
      );
  }

  updateTaskCriteriaAsync(id: string, simulatorScenarioTaskCriteriaId: string, options: SimulatorScenario_Task_Criteria_VM,) {
    return firstValueFrom(this.http
      .put(this.baseUrlWithSim + `/${id}/taskCriterias/${simulatorScenarioTaskCriteriaId}`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_Task_Criteria_VM;
        })
      )
      );
  }

  getOverviewAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrlWithSim + `/overview`)
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }

  copyScenarioById(id: any) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}`, {})
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }
  deleteScenarioById(id: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrlWithSim + `/${id}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  activeAsync(id: any) {
    return firstValueFrom(this.http
      .put(this.baseUrlWithSim + `/${id}/active`, {})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  inactiveAsync(id: any) {
    return firstValueFrom(this.http
      .put(this.baseUrlWithSim + `/${id}/inactive`, {})
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getAllSimulatorScenarioStatuses() {
    return firstValueFrom(this.http
    .get(this.baseUrl + `simulatorScenarioStatuses`)
    .pipe(
      map((res: any) => {
        return res['result'] as SimulatorScenario_Status_VM[];
      })
    )
    );
  }

  deleteTaskCriteriaAsync(id: string, simulatorScenarioTaskCriteriaId : string) {
    return firstValueFrom(this.http
      .delete(this.baseUrlWithSim + `/${id}/taskCriterias/${simulatorScenarioTaskCriteriaId}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  getEventAndScriptAsync(id: string,eventAndScriptId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrlWithSim + `/${id}/eventsAndScripts/${eventAndScriptId}`)
      .pipe(
        map((res: any) => {
          return res.result as SimulatorScenario_EventAndScript_VM;
        })
      )
      );
  }

  updateEventAndScriptAsync(id: string,eventAndScriptId:string, option : SimulatorScenario_EventAndScript_VM) {
    return firstValueFrom(this.http
      .put(this.baseUrlWithSim + `/${id}/eventsAndScripts/${eventAndScriptId}`,option)
      .pipe(
        map((res: any) => {
          return res.result as SimulatorScenario_EventAndScript_VM;
        })
      )
      );
  }

  copyEventAndScriptAsync(id: string,eventAndScriptId:string) {
    return firstValueFrom(this.http
      .post(this.baseUrlWithSim + `/${id}/eventsAndScripts/${eventAndScriptId}`, {})
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }

  deleteEventAndScriptAsync(id: string,eventAndScriptId:string) {
    return firstValueFrom(this.http
      .delete(this.baseUrlWithSim + `/${id}/eventsAndScripts/${eventAndScriptId}`)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  updateEventsAndScriptsOrderAsync(id: string, options: SimulatorScenario_UpdateEventsAndScriptsOrder_VM) {
    return firstValueFrom(this.http
      .put(this.baseUrlWithSim + `/${id}/eventsAndScripts/order`, options)
      .pipe(
        map((res: any) => {
          return res;
        })
      )
      );
  }

  publishScenarioAsync(id: string, options:SimulatorScenario_VM){
    return firstValueFrom(this.http
    .post(this.baseUrlWithSim + `/${id}/publish`, options)
    .pipe(
      map((res: any) => {
        return res;
      })
    )
    );
  }

  getAllCollaboratorPermissionsAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `simulatorScenarioCollaboratorPermissions`)
      .pipe(
        map((res: any) => {
          return res['result'] as SimulatorScenario_CollaboratorPermissions_VM[];
        })
      )
      );
  }

  addCollaboratorScenarioAsync(id: string, options:SimulatorScenario_UpdateCollaborators_VM){
    return firstValueFrom(this.http
    .post(this.baseUrlWithSim + `/${id}/collaborators`, options)
    .pipe(
      map((res: any) => {
        return res.result as SimulatorScenario_Collaborator_VM[];
      })
    )
    );
  }

}
