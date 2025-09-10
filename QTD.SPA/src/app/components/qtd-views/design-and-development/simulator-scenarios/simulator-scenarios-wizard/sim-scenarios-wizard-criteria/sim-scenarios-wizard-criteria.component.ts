import { Component, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { MatLegacySelectChange as MatSelectChange } from '@angular/material/legacy-select';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TemplatePortal } from '@angular/cdk/portal';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-sim-scenarios-wizard-criteria',
  templateUrl: './sim-scenarios-wizard-criteria.component.html',
  styleUrls: ['./sim-scenarios-wizard-criteria.component.scss']
})
export class SimScenariosWizardCriteriaComponent implements OnInit {
  @Input() inputSimScenariosId: string;
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Input() mode:string;
  tasksDataSource: MatTableDataSource<SimulatorScenario_Task_Criteria_VM> = new MatTableDataSource<SimulatorScenario_Task_Criteria_VM>();
  criteriaForm: UntypedFormGroup;
  selectedPosition: string = '';
  taskSelected: SimulatorScenario_Task_Criteria_VM;
  loader: boolean = false;
  displayTasksColumns: string[] = ['positionAbbreviation','completeTaskNumber', 'description', 'criteria', 'actions'];
  deleteDescription: string;
  deletesimulatorTaskCriteriaId: string;
  get positionsList(): SimulatorScenario_Position_VM[] {
    return (this.inputSimulatorScenario_VM?.positions ?? []);
  }
  constructor(
    public flyPanelService: FlyInPanelService,
    private formBuilder: UntypedFormBuilder,
    public vcf: ViewContainerRef,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) { }

  async ngOnInit(): Promise<void> {
    this.initializeCriteria();
  }

  initializeCriteria() {
    this.criteriaForm = this.formBuilder.group({
      positionId: [""],
    });
    this.selectedPosition ='';
    this.tasksDataSource.data=[];
  }


  async onSelectionChange(event: MatSelectChange) {
    if(event.value == "All"){
      this.selectedPosition = "All";
      await this.getAllTaskCriteriasAsync();
    }
    else{
      let selectedPositionDetails = this.positionsList?.find(x => x.id == event.value);
      this.selectedPosition = selectedPositionDetails?.positionTitle;
      console.log(this.positionsList)
      await this.getTaskCriteriaByPositionAsync(this.inputSimScenariosId, selectedPositionDetails?.positionId);
    }
  }

  openFlypanel(templateRef: any, row: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.taskSelected = row;
    this.flyPanelService.open(portal);
  }

  async getTaskCriteriaByPositionAsync(id: string, positionId: string) {
    await this.simSceariosService.getTaskCriteriaByPosition(id, positionId).then((res) => {
      this.tasksDataSource.data = res;
      console.log(res);
    });
  }

  async saveTaskCriteriaValue(value: SimulatorScenario_Task_Criteria_VM){
    if(value.id == null){
      var result = await this.simSceariosService.createTaskCriteriaAsync(this.inputSimScenariosId, value);
      var updatedCriteria = this.tasksDataSource.data.find(r => r.taskId === value.taskId);
      updatedCriteria.id = result.id;
      updatedCriteria.criteria = result.criteria;
      this.inputSimulatorScenario_VM.taskCriterias.push(updatedCriteria);
      this.flyPanelService.close();
      this.alert.successToast(await this.labelPipe.transform('Task') + " Criteria Added Successfully");
    }
    else{
      var result = await this.simSceariosService.updateTaskCriteriaAsync(this.inputSimScenariosId, value.id,value);
      var updatedCriteria = this.inputSimulatorScenario_VM.taskCriterias.find(r => r.taskId === value.taskId);
      updatedCriteria.criteria = result.criteria;
      var dataInTable = this.tasksDataSource.data.find(r => r.taskId === value.taskId);
      dataInTable = updatedCriteria;
      this.flyPanelService.close();
      this.alert.successToast(await this.labelPipe.transform('Task') + " Criteria Updated Successfully");
    }
  }

  async getAllTaskCriteriasAsync() {
    await this.simSceariosService.getAllTaskCriterias(this.inputSimScenariosId).then((res) => {
      this.tasksDataSource.data = res;
      console.log(this.tasksDataSource.data)
    });
  }

  async resetCriteria(templateRef: any, row: any) {
    this.deleteDescription = `You are resetting the criteria statement for <b> ${row.completeTaskNumber} </b> <b> ${row.description} </b> defined from this Simulator Scenario back to the `  + await this.labelPipe.transform('Task') +`’s original criteria.`;
    this.deletesimulatorTaskCriteriaId = row.id;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

 async deleteSimulatorScenarioTaskCrteria(id: string){
    await this.simSceariosService.deleteTaskCriteriaAsync(this.inputSimScenariosId,id);
    var index = this.inputSimulatorScenario_VM.taskCriterias.findIndex(r => r.id === id);
    this.inputSimulatorScenario_VM.taskCriterias.splice(index, 1);
    var dataInTable = this.tasksDataSource.data.find(r => r.id === id);
    dataInTable.id = null;
    dataInTable.criteria =null;
    this.alert.successToast( await this.labelPipe.transform('Task') + ' Criteria Reset Successfully');
  }
  
}
