import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { Position } from '@models/Position/Position';
import { SimulatorScenario_Script_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Script_VM';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SimulatorScenarioService } from 'src/app/_Services/QTD/simulator-scenario.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-script',
  templateUrl: './fly-panel-add-script.component.html',
  styleUrls: ['./fly-panel-add-script.component.scss']
})
export class FlyPanelAddScriptComponent  implements OnInit  {
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM = new SimulatorScenario_VM();
  @Output() closed = new EventEmitter<Event>();
  @Output() newScriptAdded = new EventEmitter<SimulatorScenario_Script_VM>();
  @Output() scriptUpdated = new EventEmitter<SimulatorScenario_Script_VM>();
  @Input() mode: string;
  @Input() editEventId : string;
  @Input() inputSimScenariosId: string;
  @Input() scriptId:string;
  simScenarioScript: SimulatorScenario_Script_VM;
  addScriptForm: UntypedFormGroup;
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
  editScriptDetails:SimulatorScenario_Script_VM;
  get taskCriteriaList(): SimulatorScenario_Task_Criteria_VM[] {
   return (this.inputSimulatorScenario_VM?.taskCriterias ?? []);
  }
  constructor(
    private formBuilder: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private positionService: PositionsService,
    private alert: SweetAlertService,
    private simScenarioService: SimulatorScenarioService
  ) { }

  ngOnInit() {
    this.simScenarioScript = new SimulatorScenario_Script_VM();
    this.initializeAddScriptForm();
    this.loadAsync();
  }


  initializeAddScriptForm() {
    this.addScriptForm = this.formBuilder.group({
      scriptTitle: [this.editScriptDetails?.title,Validators.required],
      scriptDescription: [this.editScriptDetails?.description],
      initiator: [this.editScriptDetails?.initiatorId, Validators.required],
      scriptHours: [],
      scriptMins: [],
      isAnotherScript: [false],
      taskCriteriaControl: this.taskCriteriaControl,
      searchInitiatorTxt : [null]
    });
  }

  async loadAsync(){
    await this.getPositionsAsync();
    await this.getScriptAsync();
  }

  async getScriptAsync(){
    if(this.scriptId != null && this.scriptId){
      await this.simScenarioService.getScriptAsync(this.scriptId,this.editEventId).then(res=>{
        this.editScriptDetails = res ;
        let alreadyLinkedCriterias = this.editScriptDetails.criterias.map(x=>x.criteriaId);
        let selectedCriterias = this.taskCriteriaList.filter(x=> alreadyLinkedCriterias.includes(x.id));
        this.taskCriteriaControl = new UntypedFormControl(selectedCriterias);
        this.enteredDate = this.editScriptDetails.time !=  null ? new Date(this.editScriptDetails.time) : null ;
        this.initializeAddScriptForm();
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
    this.addScriptForm.get('scriptHours')?.setValue(hours);
    this.addScriptForm.get('scriptMins')?.setValue(minutes);
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
    this.simScenarioScript.title = this.addScriptForm.get('scriptTitle')?.value;
    this.simScenarioScript.description = this.addScriptForm.get('scriptDescription')?.value;
    this.simScenarioScript.initiatorId = this.addScriptForm.get('initiator')?.value;
    this.simScenarioScript.time = this.convertToDateTime();
    this.simScenarioScript.eventId =this.editEventId;
  }
  async addScriptAsync() {
    this.setCreateUpdateOptions();
    this.simScenarioScript.criterias = [];
    const selectedCriterias = this.addScriptForm.get('taskCriteriaControl')?.value ?? [];
    selectedCriterias.forEach((criteria: any) => {
      this.simScenarioScript.criterias.push({
        id: null,
        criteriaId: criteria.id
      });
    });
    await this.simScenarioService.createScriptAsync(this.simScenarioScript).then((res) => {
      this.alert.successToast('Simulator Scenario Script Added Successfully');
     this.newScriptAdded.emit(res);
      if (this.addScriptForm.get('isAnotherScript')?.value == true) {
        this.addScriptForm.reset();
      }
      else {
        this.addScriptForm.reset();
        this.closed.emit();
      }

    }).catch(() => {
      this.alert.errorToast('Simulator Scenario Script Not Added');
    });
  }

  async updateScriptAync(){
    this.setCreateUpdateOptions();
    this.simScenarioScript.criterias = [];
    this.simScenarioScript.eventId = this.editEventId;
    const selectedCriterias = this.addScriptForm.get('taskCriteriaControl')?.value ?? [];
    selectedCriterias.forEach((criteria: any) => {
      this.simScenarioScript.criterias.push({
        id: this.editScriptDetails.criterias.find(x=>x.criteriaId == criteria.id)?.id,
        criteriaId: criteria.id
      });
    });
    await this.simScenarioService.updateScriptAsync(this.scriptId,this.editEventId, this.simScenarioScript).then((res) => {
      this.alert.successToast('Simulator Scenario Script Updated Successfully');
      this.scriptUpdated.emit(res);
      this.closed.emit();
    }).catch(() => {
      this.alert.errorToast('Simulator Scenario Script Not Updated');
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
    const hoursString = this.addScriptForm.get('scriptHours')?.value || "0";
    const minutesString = this.addScriptForm.get('scriptMins')?.value || "0";
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
    this.addScriptForm.get('scriptHours')?.setValue(input.value);
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
    this.addScriptForm.get('eventMins')?.setValue(input.value);
    this.convertToDateTime()

  }
  
  initiatorSearch(){
    var searchValue = this.addScriptForm.get('searchInitiatorTxt')?.value;
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
      this.addScriptForm.get('searchInitiatorTxt')?.setValue('');
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
