import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { Position } from '@models/Position/Position';
import { SimulatorScenario_Event_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Event_VM';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-event',
  templateUrl: './fly-panel-add-event.component.html',
  styleUrls: ['./fly-panel-add-event.component.scss']
})
export class FlyPanelAddEventComponent implements OnInit {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Output() closed = new EventEmitter<Event>();
  @Output() newEventAdded = new EventEmitter<SimulatorScenario_Event_VM>();
  @Output() eventUpdated = new EventEmitter<SimulatorScenario_Event_VM>();
  @Input() mode: string;
  @Input() editEventId : string;
  @Input() inputSimScenariosId: string;
  simScnerioEventAndScript: SimulatorScenario_Event_VM;
  addEventForm: UntypedFormGroup;
  loader: boolean = false;
  editEventDetails:SimulatorScenario_Event_VM;
  constructor(
    private formBuilder: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService
  ) { }

  ngOnInit() {
    this.simScnerioEventAndScript = new SimulatorScenario_Event_VM();
    this.initializeAddEventForm();
    this.loadAsync();
  }


  initializeAddEventForm() {
    this.addEventForm = this.formBuilder.group({
      eventTitle: [this.editEventDetails?.title,Validators.required],
      eventDescription: [this.editEventDetails?.description],
      isAnotherEvent: [false]
    });
  }

  async loadAsync(){
    await this.getEventAndScriptAsync();

  }

  async getEventAndScriptAsync(){
    if(this.editEventId){
      await this.simSceariosService.getEventAsync(this.inputSimScenariosId,this.editEventId).then(res=>{
        this.editEventDetails = res ;
        this.initializeAddEventForm();
      })
    }
  }

  setCreateUpdateOptions(){
    this.simScnerioEventAndScript.title = this.addEventForm.get('eventTitle')?.value;
    this.simScnerioEventAndScript.description = this.addEventForm.get('eventDescription')?.value;
  }
  async addEventAsync() {
    this.setCreateUpdateOptions();
    await this.simSceariosService.createEventAsync(this.inputSimScenariosId, this.simScnerioEventAndScript).then((res) => {
      this.alert.successToast('Simulator Scenario Event Added Successfully');
      this.newEventAdded.emit(res);
      if (this.addEventForm.get('isAnotherEvent')?.value == true) {
        this.addEventForm.reset();
      }
      else {
        this.closed.emit();
      }

    }).catch(() => {
      this.alert.errorToast('Simulator Scenario Event Not Added');
    });
  }

  async updateEventAync(){
    this.setCreateUpdateOptions();
    this.simScnerioEventAndScript.order = this.editEventDetails.order;
    this.simScnerioEventAndScript.id = this.editEventId;
    await this.simSceariosService.updateEventAsync(this.inputSimScenariosId,this.editEventId, this.simScnerioEventAndScript).then((res) => {
      this.alert.successToast('Simulator Scenario Event Updated Successfully');
      this.eventUpdated.emit(res);
      this.closed.emit();
    }).catch(() => {
      this.alert.errorToast('Simulator Scenario Event Not Updated');
    });
  }

  validateNumberInput(event: KeyboardEvent): void {
    const key = event.key;
    if (!/^[0-9]$/.test(key)) {
      event.preventDefault();
    }
  }

}
