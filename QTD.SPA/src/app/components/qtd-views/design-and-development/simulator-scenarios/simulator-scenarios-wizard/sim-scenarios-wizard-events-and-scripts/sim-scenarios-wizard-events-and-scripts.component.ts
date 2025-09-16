import { moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, QueryList, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SimulatorScenario_SimulatorScenarioEventAndScript_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_SimulatorScenarioEventAndScript_VM';
import { SimulatorScenario_UpdateEventsAndScriptsOrder_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateEventsAndScriptsOrder_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { FlyPanelAddEventComponent } from './fly-panel-add-event/fly-panel-add-event.component';
import { SimulatorScenario_Event_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Event_VM';
import { SimulatorScenario_Script_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Script_VM';
import { SimulatorScenarioService } from 'src/app/_Services/QTD/simulator-scenario.service';
import { FlyPanelAddScriptComponent } from './fly-panel-add-script/fly-panel-add-script.component';
import { MatSort } from '@angular/material/sort';

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
  @ViewChild("addScript") addScriptComponent: FlyPanelAddScriptComponent;
  eventsDataSource: MatTableDataSource<SimulatorScenario_SimulatorScenarioEventAndScript_VM>;
  scriptDataSource: MatTableDataSource<SimulatorScenario_Script_VM>;
  selectedEventAndScriptVM:any;
  editEventId: string;
  editScriptId: string;
  expandedEventElement: SimulatorScenario_SimulatorScenarioEventAndScript_VM | null =null;
  displayEventsColumns: string[] = ['expandSimScenarioEvent','drag', 'title', 'description', 'id'];
  displayScriptColumns:string[] =  [ 'title', 'description', 'action'];
  @ViewChild('eventSort') set eventSort(sorting: MatSort) {
      if (sorting) this.eventsDataSource.sort = sorting;
    }
  @ViewChildren('scriptSort') scriptSorts!: QueryList<MatSort>;

  constructor(
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    private simScenarioService: SimulatorScenarioService
  ) { }

  ngOnInit(): void {
    this.getEventAndScriptDataSource();
    this.scriptDataSource = new MatTableDataSource<SimulatorScenario_Script_VM>();
  }

  getEventAndScriptDataSource(){
    var datasource =  new MatTableDataSource(this.inputSimulatorScenario_VM?.eventsAndScripts?.sort((a,b)=>{
      return Number(a.order) - Number(b.order);
    }));
    this.eventsDataSource = datasource;
  }

  getNewlyAddedEventData(data:any){
    this.inputSimulatorScenario_VM?.eventsAndScripts.push(data);
    this.eventsDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }
  getUpdatedEventData(data:SimulatorScenario_Event_VM){
    let eventToUpdate = this.inputSimulatorScenario_VM?.eventsAndScripts.find(x=>x.id == data.id);
    eventToUpdate.title = data.title;
    eventToUpdate.description = data.description;
    this.eventsDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }

  openFlypanel(templateRef: any, action: string, row: any) {
    this.mode = action;
    this.editEventId = row?.id;
    this.editScriptId = null;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

   openScriptFlypanel(templateRef: any, action: string, row: any) {
    this.mode = action;
    this.editEventId = row?.eventId;
    this.editScriptId =row?.id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async copyEventAndScript(eventAndScriptId:string){
    var result =await this.simSceariosService.copyEventAsync(this.inputSimScenariosId,eventAndScriptId);
    this.openFlypanel(this.addEventComponent,"edit",result);
    this.alert.successToast('Simulator Scenario Event Copied Successfully');
    this.inputSimulatorScenario_VM?.eventsAndScripts.push(result);
    this.eventsDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }

  async deleteEventAndScript(eventAndScriptId:string){
    await this.simSceariosService.deleteEventAsync(this.inputSimScenariosId,eventAndScriptId);
    var deletedIdIndex = this.inputSimulatorScenario_VM?.eventsAndScripts.findIndex(x=>x.id==eventAndScriptId);
    this.inputSimulatorScenario_VM?.eventsAndScripts.splice(deletedIdIndex,1);
    this.alert.successToast('Simulator Scenario Event Deleted Successfully');
    this.eventsDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
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
    await this.simSceariosService.updateEventsOrderAsync(this.inputSimScenariosId,eventAndScriptUpdateOptions).then(res=>{
      this.alert.successToast("Simulator Scenario Events Reordered Successfully");
    });
    this.eventsDataSource.data = this.inputSimulatorScenario_VM?.eventsAndScripts;
  }

   getExpandedEventElement(row: SimulatorScenario_SimulatorScenarioEventAndScript_VM) {
    this.expandedEventElement = this.expandedEventElement === row ? null : row;
    this.scriptDataSource.data = this.expandedEventElement ? this.expandedEventElement.simulatorScenario_Script_VMs : [];
  }

  async copyScriptAsync(scriptId:string,eventId:string){
    var result =await this.simScenarioService.copyScriptAsync(scriptId,eventId);
    this.alert.successToast('Simulator Scenario Script Copied Successfully');
    const event = this.inputSimulatorScenario_VM?.eventsAndScripts.find(e => e.id === eventId);
    if (event) {
    event.simulatorScenario_Script_VMs = [...event.simulatorScenario_Script_VMs, result];
    if (this.expandedEventElement?.id === eventId) {
      this.scriptDataSource.data = [...event.simulatorScenario_Script_VMs];
    }
   }
  }

 async deleteScriptAsync(scriptId: string, eventId: string) {
  await this.simScenarioService.deleteScriptAsync(scriptId);
  const event = this.inputSimulatorScenario_VM?.eventsAndScripts.find(e => e.id === eventId);
  if (event) {
    const scriptIndex = event.simulatorScenario_Script_VMs.findIndex(s => s.id === scriptId);
    if (scriptIndex > -1) {
      event.simulatorScenario_Script_VMs.splice(scriptIndex, 1);
    }
    if (this.expandedEventElement?.id === eventId) {
      this.scriptDataSource.data = [...event.simulatorScenario_Script_VMs];
    }
  }
  this.alert.successToast('Simulator Scenario Script Deleted Successfully');
 }

 getNewlyAddedScriptsData(script: SimulatorScenario_Script_VM) {
  const event = this.inputSimulatorScenario_VM.eventsAndScripts?.find(e => e.id === script.eventId);
  if (event) {
    event.simulatorScenario_Script_VMs = event.simulatorScenario_Script_VMs ?? [];
    event.simulatorScenario_Script_VMs.push(script);
    if (this.expandedEventElement?.id === script.eventId) {
      this.scriptDataSource.data = [...event.simulatorScenario_Script_VMs];
    }
  }
 } 

 updateScriptInEvent(script: SimulatorScenario_Script_VM) {
  const event = this.inputSimulatorScenario_VM.eventsAndScripts?.find(e => e.id === script.eventId);
  if (event) {
    const idx = event.simulatorScenario_Script_VMs.findIndex(s => s.id === script.id);
    if (idx > -1) {
      event.simulatorScenario_Script_VMs[idx] = script;
      if (this.expandedEventElement?.id === script.eventId) {
        this.scriptDataSource.data = [...event.simulatorScenario_Script_VMs];
      }
    }
  }
 }
}
