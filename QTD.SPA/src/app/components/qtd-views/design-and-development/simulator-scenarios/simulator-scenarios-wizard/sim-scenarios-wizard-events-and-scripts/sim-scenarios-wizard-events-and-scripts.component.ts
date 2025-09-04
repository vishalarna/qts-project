import { moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SimulatorScenario_EventAndScript_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_EventAndScript_VM';
import { SimulatorScenario_SimulatorScenarioEventAndScript_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_SimulatorScenarioEventAndScript_VM';
import { SimulatorScenario_UpdateEventsAndScriptsOrder_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateEventsAndScriptsOrder_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { FlyPanelAddEventComponent } from './fly-panel-add-event/fly-panel-add-event.component';

@Component({
  selector: 'app-sim-scenarios-wizard-events-and-scripts',
  templateUrl: './sim-scenarios-wizard-events-and-scripts.component.html',
  styleUrls: ['./sim-scenarios-wizard-events-and-scripts.component.scss']
})
export class SimScenariosWizardEventsAndScriptsComponent implements OnInit {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Input() inputSimScenariosId: string;
  @Input() mode:string;
  @ViewChild("addEvent") addEventComponent: FlyPanelAddEventComponent;
  eventsAndScriptDataSource: MatTableDataSource<SimulatorScenario_SimulatorScenarioEventAndScript_VM>;
  selectedEventAndScriptVM:any;
  editEventId: string;
  displayEventsAndScriptsColumns: string[] = ['drag', 'title', 'description', 'id']

  constructor(
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService
  ) { }

  ngOnInit(): void {
    this.getEventAndScriptDataSource();
  }

  getEventAndScriptDataSource(){
    var datasource =  new MatTableDataSource(this.inputSimulatorScenario_VM?.eventsAndScripts?.sort((a,b)=>{
      return Number(a.order) - Number(b.order);
    }));
    this.eventsAndScriptDataSource = datasource;
  }

  getNewlyAddedEventData(data:any){
    this.inputSimulatorScenario_VM?.eventsAndScripts.push(data);
    this.eventsAndScriptDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }
  getUpdatedEventData(data:SimulatorScenario_EventAndScript_VM){
    let eventToUpdate = this.inputSimulatorScenario_VM?.eventsAndScripts.find(x=>x.id == data.id);
    eventToUpdate.title = data.title;
    eventToUpdate.description = data.description;
    this.eventsAndScriptDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }

  openFlypanel(templateRef: any, action: string, row: any) {
    this.mode = action;
    this.editEventId = row?.id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async copyEventAndScript(eventAndScriptId:string){
    var result =await this.simSceariosService.copyEventAndScriptAsync(this.inputSimScenariosId,eventAndScriptId);
    this.openFlypanel(this.addEventComponent,"edit",result);
    this.alert.successToast('Simulator Scenario Event Copied Successfully');
    this.inputSimulatorScenario_VM?.eventsAndScripts.push(result);
    this.eventsAndScriptDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;

  }

  async deleteEventAndScript(eventAndScriptId:string){
    await this.simSceariosService.deleteEventAndScriptAsync(this.inputSimScenariosId,eventAndScriptId);
    var deletedIdIndex = this.inputSimulatorScenario_VM?.eventsAndScripts.findIndex(x=>x.id==eventAndScriptId);
    this.inputSimulatorScenario_VM?.eventsAndScripts.splice(deletedIdIndex,1);
    this.alert.successToast('Simulator Scenario Event Deleted Successfully');
    this.eventsAndScriptDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
    
  }

  async dropTable(event: any) {
    moveItemInArray(this.inputSimulatorScenario_VM.eventsAndScripts, event.previousIndex , event.currentIndex);
    var totalLinkedIds = this.inputSimulatorScenario_VM.eventsAndScripts.map((x)=>x.id);
    totalLinkedIds = Array.from(new Set(totalLinkedIds));
    const eventAndScriptUpdateOptions = new SimulatorScenario_UpdateEventsAndScriptsOrder_VM();
    eventAndScriptUpdateOptions.eventsAndScripts = totalLinkedIds.map((eventAndScriptId, index) => ({
        eventAndScriptId,
        order: index + 1
    }));
    await this.simSceariosService.updateEventsAndScriptsOrderAsync(this.inputSimScenariosId,eventAndScriptUpdateOptions).then(res=>{
      this.alert.successToast("Simulator Scenario Events Reordered Successfully");
    });
    this.eventsAndScriptDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }

}
