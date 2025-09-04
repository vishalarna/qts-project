import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { Position } from '@models/Position/Position';
import { SimulatorScenario_EventAndScript_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_EventAndScript_VM';
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
  @Output() newEventAdded = new EventEmitter<SimulatorScenario_EventAndScript_VM>();
  @Output() eventUpdated = new EventEmitter<SimulatorScenario_EventAndScript_VM>();
  @Input() mode: string;
  @Input() editEventId : string;
  @Input() inputSimScenariosId: string;
  simScnerioEventAndScript: SimulatorScenario_EventAndScript_VM;
  addEventForm: UntypedFormGroup;
  taskCriteriaControl = new UntypedFormControl([]);
  positionsList: Position[];
  filteredPositions: Position[];
  taskCrtiterias: SimulatorScenario_Task_Criteria_VM[] = [];
  criteriaIds: string[] = [];
  dayPeriod: string = 'AM';
  loader: boolean = false;
  isTwelveHourFormat: boolean = true;
  enteredDate: Date | null = null;
  @ViewChild('selectCriteria') selectCriteria:any;
  @ViewChild('selectControl', { static: false }) selectControl!: MatSelect;
  editEventDetails:SimulatorScenario_EventAndScript_VM;
  get taskCriteriaList(): SimulatorScenario_Task_Criteria_VM[] {
    return (this.inputSimulatorScenario_VM?.taskCriterias ?? []);
  }
  constructor(
    private formBuilder: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private positionService: PositionsService,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService
  ) { }

  ngOnInit() {
    this.simScnerioEventAndScript = new SimulatorScenario_EventAndScript_VM();
    this.initializeAddEventForm();
    this.loadAsync();
  }


  initializeAddEventForm() {
    this.addEventForm = this.formBuilder.group({
      eventTitle: [this.editEventDetails?.title,Validators.required],
      eventDescription: [this.editEventDetails?.description],
      initiator: [this.editEventDetails?.initiatorId, Validators.required],
      eventHours: [],
      eventMins: [],
      isAnotherEvent: [false],
      taskCriteriaControl: this.taskCriteriaControl,
      searchInitiatorTxt : [null]
    });
  }

  async loadAsync(){
    await this.getPositionsAsync();
    await this.getEventAndScriptAsync();

  }

  async getEventAndScriptAsync(){
    if(this.editEventId){
      await this.simSceariosService.getEventAndScriptAsync(this.inputSimScenariosId,this.editEventId).then(res=>{
        this.editEventDetails = res ;
        let alreadyLinkedCriterias = this.editEventDetails.criterias.map(x=>x.criteriaId);
        let selectedCriterias = this.taskCriteriaList.filter(x=> alreadyLinkedCriterias.includes(x.id));
        this.taskCriteriaControl = new UntypedFormControl(selectedCriterias);
        this.enteredDate = this.editEventDetails.time !=  null ? new Date(this.editEventDetails.time) : null ;
        this.initializeAddEventForm();
        this.setTimeValues();
      })
    }
  }

  changeTimeFormat() {
    this.isTwelveHourFormat = !this.isTwelveHourFormat;
    this.setTimeValues();
  }

  setTimeValues(){
    let hours = this.enteredDate?.getHours();
    let minutes = this.enteredDate?.getMinutes();
    if(hours != null && this.isTwelveHourFormat){
      this.dayPeriod = hours >= 12 ? 'PM' : 'AM';
      hours = hours % 12;
      hours = hours ? hours : 12;
    }
    this.addEventForm.get('eventHours')?.setValue(hours);
    this.addEventForm.get('eventMins')?.setValue(minutes);
  }

  async getPositionsAsync() {
    this.loader = true;
    await this.positionService.getAllWithoutIncludes().then((res) => {
      this.positionsList = res.filter(x=> x.active);
      this.filteredPositions = this.positionsList;
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  setCreateUpdateOptions(){
    this.simScnerioEventAndScript.title = this.addEventForm.get('eventTitle')?.value;
    this.simScnerioEventAndScript.description = this.addEventForm.get('eventDescription')?.value;
    this.simScnerioEventAndScript.initiatorId = this.addEventForm.get('initiator')?.value;
    this.simScnerioEventAndScript.time = this.convertToDateTime();
  }
  async addEventAsync() {
    this.setCreateUpdateOptions();
    this.simScnerioEventAndScript.criterias = [];
    const selectedCriterias = this.addEventForm.get('taskCriteriaControl')?.value ?? [];
    selectedCriterias.forEach((criteria: any) => {
      this.simScnerioEventAndScript.criterias.push({
        id: null,
        criteriaId: criteria.id
      });
    });
    await this.simSceariosService.createEventAndScript(this.inputSimScenariosId, this.simScnerioEventAndScript).then((res) => {
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
    this.simScnerioEventAndScript.criterias = [];
    this.simScnerioEventAndScript.order = this.editEventDetails.order;
    this.simScnerioEventAndScript.id = this.editEventId;
    const selectedCriterias = this.addEventForm.get('taskCriteriaControl')?.value ?? [];
    selectedCriterias.forEach((criteria: any) => {
      this.simScnerioEventAndScript.criterias.push({
        id: this.editEventDetails.criterias.find(x=>x.criteriaId == criteria.id)?.id,
        criteriaId: criteria.id
      });
    });
    await this.simSceariosService.updateEventAndScriptAsync(this.inputSimScenariosId,this.editEventId, this.simScnerioEventAndScript).then((res) => {
      this.alert.successToast('Simulator Scenario Event Updated Successfully');
      this.eventUpdated.emit(res);
      this.closed.emit();
    }).catch(() => {
      this.alert.errorToast('Simulator Scenario Event Not Updated');
    });
  }

  removeCriteria(i: any) {
    const taskCriterias = this.taskCriteriaControl.value as SimulatorScenario_Task_Criteria_VM[];
    this.removeFirst(taskCriterias, i);
    this.taskCriteriaControl.setValue(taskCriterias);
  }

  private removeFirst(array: any[], toRemove: any): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  getdayPeriod(time: string) {
    this.dayPeriod = time;
    this.convertToDateTime()

  }

  convertToDateTime(): Date | null {
    const hoursString = this.addEventForm.get('eventHours')?.value || "0";
    const minutesString = this.addEventForm.get('eventMins')?.value || "0";
    if (hoursString === "0" && minutesString === "0"){
      this.enteredDate = null;
      return null;
    } 
    let hours = parseInt(hoursString, 10) || 0;
    const minutes = parseInt(minutesString, 10) || 0;

    if (this.isTwelveHourFormat) {
        const period = this.dayPeriod?.toUpperCase();
        if (period === 'PM' && hours < 12) {
            hours += 12;
        } else if (period === 'AM' && hours === 12) {
            hours = 0;
        }
    }
    const currentDate = new Date();
    this.enteredDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), hours, minutes);
    const dateTime = new Date(Date.UTC(currentDate.getUTCFullYear(), currentDate.getUTCMonth(), currentDate.getUTCDate(), hours, minutes));
    return dateTime;
  }

  ValidateHrs(event: any, maxLength: number){
    const input = event.target as HTMLInputElement;
    const maxHours = this.isTwelveHourFormat ? 12 : 23;
    if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength);
    }
    const hours = parseInt(input.value, 10);
    if (!isNaN(hours) && hours > maxHours) {
        input.value = '';
    }
    this.addEventForm.get('eventHours')?.setValue(input.value);
    this.convertToDateTime()
  }

  ValidateMins(event: any, maxLength: number){
    const input = event.target as HTMLInputElement;
    const maxMins = 59;
    if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength);
    }
    const minutes = parseInt(input.value, 10);
    if (!isNaN(minutes) && (minutes < 0 || minutes > maxMins)) {
        input.value = '';
    }
    this.addEventForm.get('eventMins')?.setValue(input.value);
    this.convertToDateTime()

  }
  
  initiatorSearch(){
    var searchValue = this.addEventForm.get('searchInitiatorTxt')?.value;
    if (searchValue !== undefined && searchValue !== null) {
      searchValue = String(searchValue).toLowerCase();
    } else {
      searchValue = "";
    }
    this.filteredPositions =  this.positionsList.filter((x) => {
      return x.positionTitle.trim().toLowerCase().includes(searchValue)
  })
  }
  resetSearch(){
    setTimeout(() => {
      this.addEventForm.get('searchInitiatorTxt')?.setValue('');
      this.initiatorSearch();
    }, 500);
  }

  handleKeydown(event: KeyboardEvent) {
    this.selectControl._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  validateNumberInput(event: KeyboardEvent): void {
    const key = event.key;
    if (!/^[0-9]$/.test(key)) {
      event.preventDefault();
    }
  }

}
