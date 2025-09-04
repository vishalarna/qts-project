import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  Component,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SimulatorScenario_EnablingObjective_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_EnablingObjective_VM';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SimulatorScenario_Procedure_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Procedure_VM';
import { SimulatorScenario_Task_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_VM';
import { SimulatorScenario_UpdateEnablingObjectives_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateEnablingObjectives_VM';
import { SimulatorScenario_UpdatePositions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePositions_VM';
import { SimulatorScenario_UpdateProcedures_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateProcedures_VM';
import { SimulatorScenario_UpdateTasks_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateTasks_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-sim-scenarios-wizard-linkages',
  templateUrl: './sim-scenarios-wizard-linkages.component.html',
  styleUrls: ['./sim-scenarios-wizard-linkages.component.scss'],
})
export class SimScenariosWizardLinkagesComponent implements OnInit {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM ;
  @Input() inputSimScenariosId: string;
  @Input() mode: string;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('procedureSort') procedureSort: MatSort;
  @ViewChild('metaPaginator') set metaPaginator(paging: MatPaginator) {
    if (paging) this.objAndTasksDataSource.paginator = paging;
  }
  @ViewChild('procedurePaginator') set procedurePaginator(
    paging: MatPaginator
  ) {
    if (paging) this.proceduresDataSource.paginator = paging;
  }
  linkedPositionsList: SimulatorScenario_Position_VM[] = [];
  linkedTasksList: SimulatorScenario_Task_VM[] = [];
  positionsUpdateOptions: SimulatorScenario_UpdatePositions_VM;
  procedureUpdateOptions: SimulatorScenario_UpdateProcedures_VM;
  simScenarioTaskUpdateOptions = new SimulatorScenario_UpdateTasks_VM();
  objectiveUpdateOptions: SimulatorScenario_UpdateEnablingObjectives_VM;
  objAndTasksDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  proceduresDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  objectiveSelection = new SelectionModel<any>(true, []);
  procedureSelection = new SelectionModel<any>(true, []);
  taskCheckListSelection = new SelectionModel<any>(true);
  eoCheckListSelection = new SelectionModel<any>(true);
  eoTaskList: Array<any>;
  linkedPositionIds: any[] = [];
  linkedProceduresIds: any[] = [];
  linkedObjectiveIds: string[] = [];
  linkedTaskIds: string[] = [];
  displayObjectivesColumns: Array<string>;
  displayProceduresColumns: Array<string>;
  positionIdToRemove: string;
  showPosLinkLoader: boolean = false;
  showLinkTaskLoader: boolean = false;
  includeEos: boolean = false;
  includeProcedures: boolean = false;
  isRemovingTasks: boolean = false;
  isRemovingObjectives: boolean = false;
  unlinkProcedureDesc: string;
  unlinkProcedureId: string;
  unlinkTaskandEODesc:string;
  unlinkObjectivesDesc:string;
  unlinkProceduresDesc:string;
  unlinkTaskandEOValue:any;
  unlinkPositionDesc: string;
  unlinkPositionId: string;
  isObjectiveLinkUnlink : boolean = false;
  isProcedureLinkUnlink : boolean = false;
  isProcedureUnlink : boolean = false;
  isObjectiveUnlink : boolean = false;
  get selectedTasks() {
    return this.objectiveSelection.selected.filter((x) => x.type == 'Task');
  }
  get selectedEOs() {
    return this.objectiveSelection.selected.filter((x) => x.type == 'EO');
  }
  constructor(
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    public dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    this.displayObjectivesColumns = [
      'id',
      'number',
      'type',
      'description',
      'action',
    ];
    this.displayProceduresColumns = ['id', 'number', 'description', 'action'];
    this.eoTaskList = [];
    this.objectiveUpdateOptions = new SimulatorScenario_UpdateEnablingObjectives_VM();
    this.positionsUpdateOptions = new SimulatorScenario_UpdatePositions_VM();
    this.procedureUpdateOptions = new SimulatorScenario_UpdateProcedures_VM();
    await this.loadAsync();
  }

  async loadAsync() {
    this.scrollToTop();
    this.linkedPositionsList = this.inputSimulatorScenario_VM?.positions;
    this.linkedPositionIds =
      this.inputSimulatorScenario_VM?.positions?.map(
        (position) => position?.positionId
      ) ?? [];
    this.linkedTaskIds =
      this.inputSimulatorScenario_VM?.tasks?.map((k) => k.taskId) ?? [];
    this.linkedObjectiveIds =
      this.inputSimulatorScenario_VM?.enablingObjectives?.map(
        (k) => k.enablingObjectiveId
      ) ?? [];
    this.setObjAndTaskvalues();
    this.proceduresDataSource.data = this.inputSimulatorScenario_VM?.procedures;
    setTimeout(() => {
      this.proceduresDataSource.sort = this.procedureSort;
    }, 1);
    this.linkedProceduresIds =
      this.inputSimulatorScenario_VM?.procedures?.map(
        (procedure) => procedure?.procedureId
      ) ?? [];
  }

  setObjAndTaskvalues() {
    if (this.inputSimulatorScenario_VM) {
      this.eoTaskList = [];
      this.eoTaskList = [
        ...this.inputSimulatorScenario_VM.enablingObjectives,
        ...this.inputSimulatorScenario_VM.tasks,
      ];
      this.objAndTasksDataSource = new MatTableDataSource<any>(this.eoTaskList);
      setTimeout(() => {
        this.objAndTasksDataSource.sort = this.sort;
      }, 1);
    }
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async handleNewPositionsLinked(event: any) {
    const newPositions = event;
    newPositions.forEach((newPosition) => {
      const exists = this.linkedPositionsList.some(
        (position) => position.id === newPosition.id
      );
      if (!exists) {
        const position = {
          id: null,
          positionId: newPosition.positionId,
          positionTitle: newPosition.positionTitle,
        };
        this.linkedPositionsList.push(position);
        this.linkedPositionIds.push(newPosition.positionId);
      }
    });
    await this.linkPosToScenariosAsync();
    this.alert.successToast('Simulator Scenario ' + await this.labelPipe.transform('Position') + 's Linked Successfully');
  }

  async linkPosToScenariosAsync() {
    this.showPosLinkLoader = true;
    this.positionsUpdateOptions.positions = this.linkedPositionsList;
    let options = this.positionsUpdateOptions
    await this.simSceariosService.linkPosistionToScenarios(this.inputSimScenariosId, options).then(async (res) => {
      this.inputSimulatorScenario_VM.positions = res;
    }).finally(() => {
      this.showPosLinkLoader = false;
    });
  }

  async unlinkPositionModal(templateRef: any, position?: any) {
    this.unlinkPositionDesc = `You are selecting to unlink ` + await this.labelPipe.transform('Position') +` <b> ${position?.positionTitle} </b> from the Simulator Scenario.`;
    this.unlinkPositionId = position?.positionId;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async removePositionAsync() {
    this.linkedPositionsList = this.linkedPositionsList.filter(
      (x) => x.positionId != this.unlinkPositionId
    );
    await this.linkPosToScenariosAsync();
    this.linkedPositionIds = this.linkedPositionIds.filter(
      (id) => id !== this.unlinkPositionId
    );
    this.alert.successToast('Simulator Scenario ' + await this.labelPipe.transform('Position') + ' Unlinked Successfully');
  }

  async handleObjectivesLinked(event: any) {
    this.isObjectiveLinkUnlink = true;
    const objectives = event;
    objectives?.forEach((val) => {
      var objectivesVm = new SimulatorScenario_EnablingObjective_VM(
        val.id,
        val.description
      );
      this.objectiveUpdateOptions.setEnablingObjectives(objectivesVm);
    });
    this.inputSimulatorScenario_VM.enablingObjectives.forEach((res) => {
      this.objectiveUpdateOptions.setEnablingObjectives(res);
    });
    await this.updateSSEnablingObjectives();
    this.alert.successToast('Simulator Scenario ' + (await this.labelPipe.transform('Enabling Objective')) + 's Updated Successfully' );
    this.isObjectiveLinkUnlink = false;
  }

  async updateSSEnablingObjectives() {
    await this.simSceariosService
      .linkEOsToScenarios(this.inputSimScenariosId, this.objectiveUpdateOptions)
      .then(async (res) => {
        this.objectiveSelection.clear();
        this.inputSimulatorScenario_VM.enablingObjectives = res;
        this.linkedObjectiveIds =
          this.inputSimulatorScenario_VM.enablingObjectives.map(
            (k) => k.enablingObjectiveId
          );
        this.eoTaskList = [
          ...this.inputSimulatorScenario_VM.tasks,
          ...this.inputSimulatorScenario_VM.enablingObjectives,
        ];
        this.objAndTasksDataSource.data = this.eoTaskList;
       
      });
  }

  async handleTasksLinked(event: any) {
    this.isObjectiveLinkUnlink = true;

    const tasks = event[0];
    tasks?.forEach((val) => {
      var taskVm = new SimulatorScenario_Task_VM(val.id,val.number,val.description,'Task');
      this.simScenarioTaskUpdateOptions.setTasks(taskVm);
    });
    this.inputSimulatorScenario_VM.tasks.forEach((res) => {
      this.simScenarioTaskUpdateOptions.setTasks(res);
    });
    await this.updateSSTasks(event[1],event[2]);
    this.alert.successToast('Simulator Scenario ' + (await this.labelPipe.transform('Task')) + 's Updated Successfully');
    this.isObjectiveLinkUnlink = false;
  }

  async updateSSTasks(includeEnablingObjectives : boolean = false, includeProcedures : boolean = false) {
    this.simScenarioTaskUpdateOptions.includeEnablingObjectives = includeEnablingObjectives;
    this.simScenarioTaskUpdateOptions.includeProcedures = includeProcedures;
    await this.simSceariosService
      .linkTaskToScenarios(
        this.inputSimScenariosId,
        this.simScenarioTaskUpdateOptions
      )
      .then(async (res) => {
        this.objectiveSelection.clear();
        this.inputSimulatorScenario_VM.tasks  = res.simulatorScenarioTaskVMs;
        if(includeEnablingObjectives){
          this.inputSimulatorScenario_VM.enablingObjectives = res.simulatorScenarioEnablingObjectiveVMs;
          this.linkedObjectiveIds = this.inputSimulatorScenario_VM.enablingObjectives.map(
            (k) => k.enablingObjectiveId
          );
        }
        if(includeProcedures){
          this.inputSimulatorScenario_VM.procedures = res.simulatorScenarioProcedureVMs; 
          this.linkedProceduresIds = this.inputSimulatorScenario_VM.procedures.map((k) => k.procedureId);
          this.proceduresDataSource.data = this.inputSimulatorScenario_VM.procedures;
        }
        this.linkedTaskIds = this.inputSimulatorScenario_VM.tasks.map(
          (k) => k.taskId
        );
        this.eoTaskList = [
          ...this.inputSimulatorScenario_VM.tasks,
          ...this.inputSimulatorScenario_VM.enablingObjectives,
        ];
        this.objAndTasksDataSource.data = this.eoTaskList;
      });
  }

  async removeTasksandObjAsync() {
    this.isObjectiveLinkUnlink = true;
    this.simScenarioTaskUpdateOptions = new SimulatorScenario_UpdateTasks_VM();
    let tasks = this.objectiveSelection.selected.filter(
      (x) => x.type == 'Task'
    );
    let objectives = this.objectiveSelection.selected.filter(
      (x) => x.type == 'EO'
    );

    if (tasks.length > 0) {
      tasks.forEach((val) => {
        var ssTaskIndex = this.inputSimulatorScenario_VM.tasks.findIndex(
          (r) => r.id == val.id
        );
        this.inputSimulatorScenario_VM.tasks.splice(ssTaskIndex, 1);
        this.simScenarioTaskUpdateOptions.tasks =
          this.inputSimulatorScenario_VM.tasks;
        });
      await this.updateSSTasks();
    }

    if (objectives.length > 0) {
      objectives.forEach((val) => {
        var ssEOIndex =
          this.inputSimulatorScenario_VM.enablingObjectives.findIndex(
            (r) => r.id == val.id
          );
        this.inputSimulatorScenario_VM.enablingObjectives.splice(ssEOIndex, 1);
        this.objectiveUpdateOptions.enablingObjectives =
          this.inputSimulatorScenario_VM.enablingObjectives;
        });
      await this.updateSSEnablingObjectives();
    }

    this.alert.successToast('Simulator Scenario Objectives Updated Successfully' );
    this.isObjectiveLinkUnlink = false;
    this.isObjectiveUnlink = false;
  }

  async removeProcedureAsync() {
    this.procedureUpdateOptions = new SimulatorScenario_UpdateProcedures_VM();
    this.procedureSelection.selected.forEach((val) => {
      var ssProcedureIndex =
        this.inputSimulatorScenario_VM.procedures.findIndex(
          (r) => r.id == val.id
        );
      this.inputSimulatorScenario_VM.procedures.splice(ssProcedureIndex, 1);
      this.procedureUpdateOptions.procedures =
        this.inputSimulatorScenario_VM.procedures;
      });
    this.updateSSProcedures();
  }

  handleProceduresLinked(event: any) {
    const procedures = event;
    procedures?.forEach((val) => {
      var procedureVm = new SimulatorScenario_Procedure_VM(
        val.id,
        val.description
      );
      this.procedureUpdateOptions.setProcedures(procedureVm);
    });

    this.inputSimulatorScenario_VM.procedures.forEach((res) => {
      this.procedureUpdateOptions.setProcedures(res);
    });
    this.updateSSProcedures();
  }

  async updateSSProcedures() {
    this.isProcedureLinkUnlink = true;
    await this.simSceariosService
      .linkProcToScenarios(
        this.inputSimScenariosId,
        this.procedureUpdateOptions
      )
      .then(async (res) => {
        this.procedureSelection.clear();
        this.inputSimulatorScenario_VM.procedures = res;
        this.linkedProceduresIds =
          this.inputSimulatorScenario_VM.procedures.map((k) => k.procedureId);
        this.proceduresDataSource.data =
          this.inputSimulatorScenario_VM.procedures;
        this.alert.successToast('Simulator Scenario ' +
          (await this.labelPipe.transform('Procedure')) +
            's Updated Successfully'
        );
      })
      .finally(()=>{
        this.isProcedureLinkUnlink = false;
        this.isProcedureUnlink = false;
      });
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }

  isAllObjectivesSelected() {
    const numSelected = this.objectiveSelection.selected.length;
    const numRows = this.objAndTasksDataSource.data.length;
    return numSelected === numRows;
  }

  masterToggleObjectives() {
    this.isAllObjectivesSelected()
      ? this.objectiveSelection.clear()
      : this.objAndTasksDataSource.data.forEach((row) =>
          this.objectiveSelection.select(row)
        );
  }

  isAllProceduresSelected() {
    const numSelected = this.procedureSelection.selected.length;
    const numRows = this.proceduresDataSource.data.length;
    return numSelected === numRows;
  }

  masterToggleProcedures() {
    this.isAllProceduresSelected()
      ? this.procedureSelection.clear()
      : this.proceduresDataSource.data.forEach((row) =>
          this.procedureSelection.select(row)
        );
  }

  async unlinkProcedureModal(templateRef: any, row?: any) {
    this.unlinkProcedureDesc = `You are selecting to unlink ` + await this.labelPipe.transform('Procedure') +` <b> ${row.number} ${row.description == null ? "" : row.description}</b> from the Simulator Scenario.`;
    this.unlinkProcedureId = row.procedureId;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  unlinkProcedureAsync() {
    this.procedureUpdateOptions.procedures = [];
    this.inputSimulatorScenario_VM.procedures.forEach((val) => {
      this.procedureUpdateOptions.setProcedures(val);
    });
    const index = this.procedureUpdateOptions.procedures.findIndex(
      (r) => r.procedureId === this.unlinkProcedureId
    );
    if (index !== -1) {
      this.procedureUpdateOptions.procedures.splice(index, 1);
    }
    this.updateSSProcedures();
  }

  async unlinkTaskModal(templateRef: any, row?: any) {
    if(row.type === "Task"){
    this.unlinkTaskandEODesc = `You are selecting to unlink ${await this.labelPipe.transform('Task')} <b>${row.number} ${row.description}</b> from the Simulator Scenario.`;
    this.unlinkTaskandEOValue = row;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }else if (row.type === "EO") {
    this.unlinkTaskandEODesc = `You are selecting to unlink ${await this.labelPipe.transform('Enabling Objective')} <b>${row.number} ${row.description}</b> from the Simulator Scenario.`;
    this.unlinkTaskandEOValue = row;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  
  }
  unlinkObjectivesModal(templateRef: any) {
    this.unlinkObjectivesDesc = "You are selecting to unlink the following Objectives from the Simulator Scenario.";
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async unlinkProceduresModalAsync(templateRef: any) {
    this.unlinkProcedureDesc = `You are selecting to unlink the following ${await this.labelPipe.transform('Procedure')}s from the Simulator Scenario.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }



  async unlinkObjectiveAsync() {
    this.isObjectiveLinkUnlink = true;
    if(this.unlinkTaskandEOValue.type === "Task"){
    this.simScenarioTaskUpdateOptions.tasks = [];
    this.inputSimulatorScenario_VM.tasks.forEach((val) => {
      this.simScenarioTaskUpdateOptions.setTasks(val);
    });
    const index = this.simScenarioTaskUpdateOptions.tasks.findIndex(
      (r) => r.taskId === this.unlinkTaskandEOValue.taskId
    );
    if (index !== -1) {
      this.simScenarioTaskUpdateOptions.tasks.splice(index, 1);
    }
    await this.updateSSTasks();
    this.alert.successToast('Simulator Scenario ' + (await this.labelPipe.transform('Task')) + 's Updated Successfully');
  }
  else if(this.unlinkTaskandEOValue.type === "EO"){
    this.objectiveUpdateOptions.enablingObjectives = [];
    this.inputSimulatorScenario_VM.enablingObjectives.forEach((val) => {
      this.objectiveUpdateOptions.setEnablingObjectives(val);
    });
    const index = this.objectiveUpdateOptions.enablingObjectives.findIndex((r) => r.enablingObjectiveId === this.unlinkTaskandEOValue.enablingObjectiveId);
    if (index !== -1) {
      this.objectiveUpdateOptions.enablingObjectives.splice(index, 1);
    }
    await this.updateSSEnablingObjectives();
    this.alert.successToast('Simulator Scenario ' + (await this.labelPipe.transform('Enabling Objective')) + 's Updated Successfully' );
  }
  this.isObjectiveLinkUnlink = false;
  }

   getUnlinkSelectedText(){
    const taskCount = this.selectedTasks.length;
    const eoCount = this.selectedEOs.length;
    const taskLabel = `${taskCount} Task${taskCount !== 1 ? 's' : ''}`;
    const eoLabel = `${eoCount} Enabling Objective${eoCount !== 1 ? 's' : ''}`;
    let text = taskCount > 0 ? taskLabel : '';
    text += text === '' ? '' : (eoCount > 0 ? ' and ' : ' selected');
    text += eoCount > 0 ? `${eoLabel} selected` : '';
    return text;
  }
}
