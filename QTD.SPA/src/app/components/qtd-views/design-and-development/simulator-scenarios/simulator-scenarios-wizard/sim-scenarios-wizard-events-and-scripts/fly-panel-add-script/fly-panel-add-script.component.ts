import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SimulatorScenario_Script_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Script_VM';
import { SimulatorScenario_Task_Criteria_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_Criteria_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { PositionTaskService } from 'src/app/_Services/QTD/Position_Task/api.positiontask.service';
import { SimulatorScenarioService } from 'src/app/_Services/QTD/simulator-scenario.service';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
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
  taskCrtiterias: SimulatorScenario_Task_Criteria_VM[] = [];
  criteriaIds: string[] = [];
  dayPeriod: string = 'AM';
  loader: boolean = false;
  isTwelveHourFormat: boolean = true;
  enteredDate: Date | null = null;
  @ViewChild('selectCriteria') selectCriteria:any;
  @ViewChild('selectControl', { static: false }) selectControl!: MatSelect;
  editScriptDetails:SimulatorScenario_Script_VM;
  simulatorScenario_PositionsList:SimulatorScenario_Position_VM[];
  filteredSimulatorScenario_Positions: SimulatorScenario_Position_VM[];
  get taskCriteriaList(): SimulatorScenario_Task_Criteria_VM[] {
   return (this.inputSimulatorScenario_VM?.taskCriterias ?? []);
  }
  filteredTaskCriteriaList: SimulatorScenario_Task_Criteria_VM[] = [];

  constructor(
    private formBuilder: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private positionTaskService: PositionTaskService,
    private alert: SweetAlertService,
    private simScenarioService: SimulatorScenarioService,
     private simSceariosService: SimulatorScenariosService,
  ) { }

  ngOnInit() {
    this.simScenarioScript = new SimulatorScenario_Script_VM();
    this.initializeAddScriptForm();
    this.loadAsync();
  }


  initializeAddScriptForm() {
    let initiatorValue: string | null = this.editScriptDetails?.initiatorId ?? null;
    if (this.editScriptDetails?.initiatorOther) {
      initiatorValue = 'Other';
       this.taskCriteriaControl.disable();
       this.filteredTaskCriteriaList = [];
    } else if (this.editScriptDetails?.initiatorInstructor) {
      initiatorValue = 'Instructor';
      this.taskCriteriaControl.disable();
      this.filteredTaskCriteriaList = [];
    }
    this.addScriptForm = this.formBuilder.group({
      scriptTitle: [this.editScriptDetails?.title,Validators.required],
      scriptDescription: [this.editScriptDetails?.description],
      initiator: [initiatorValue, Validators.required],
      scriptHours: [],
      scriptMins: [],
      isAnotherScript: [false],
      taskCriteriaControl: this.taskCriteriaControl,
      searchInitiatorTxt : [null]
    });
  }

  async loadAsync(){
    await this.loadSimulatorScenarioPositions();
    await this.getScriptAsync();
  }

  async getScriptAsync(){
    if(this.scriptId != null && this.scriptId){
      await this.simScenarioService.getScriptAsync(this.scriptId,this.editEventId).then(async res=>{
        this.editScriptDetails = res ;
        const selectedSimScenarioPosId = this.editScriptDetails.initiatorId;
        const selectedPosition = this.filteredSimulatorScenario_Positions.find(p => p.id === selectedSimScenarioPosId);
        if (selectedPosition) {
          await this.filterCriteriaByPosition(selectedPosition.positionId);
          const linkedCriteriaId = this.editScriptDetails.criterias[0]?.criteriaId;
          const selectedCriteria = this.filteredTaskCriteriaList.find(x => x.id === linkedCriteriaId) ?? null;
          this.taskCriteriaControl = new UntypedFormControl(selectedCriteria);
        }
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

  async loadSimulatorScenarioPositions(){
    await this.simSceariosService.getAllSimulatorScenario_PositionAsync(this.inputSimScenariosId).then((res)=>{
       this.simulatorScenario_PositionsList = res;
       this.filteredSimulatorScenario_Positions=this.simulatorScenario_PositionsList;
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  onInitiatorChange(selectedSimScenarioPosId: string) {
    if (selectedSimScenarioPosId === 'Other' || selectedSimScenarioPosId === 'Instructor') {
    this.taskCriteriaControl.disable();
    this.filteredTaskCriteriaList = [];
    return;
    }
    const selectedPosition = this.filteredSimulatorScenario_Positions.find(p => p.id === selectedSimScenarioPosId);
    if (!selectedPosition) {
      return;
    }
    this.taskCriteriaControl.enable();
    this.filterCriteriaByPosition(selectedPosition.positionId);
  }

 async filterCriteriaByPosition(positionId: string) {
    try {
      const res: any = await this.positionTaskService.getPositionTaskByPositionIdAsync(positionId);
      const allowedTaskIds = res.result.map((pt: any) => pt.taskId);
      this.filteredTaskCriteriaList = this.taskCriteriaList.filter(tc => allowedTaskIds.includes(tc.taskId));
    } catch (err) {
      console.error('Error fetching Position tasks:', err);
    }
  }

  setCreateUpdateOptions(){
    const selectedInitiator = this.addScriptForm.get('initiator')?.value;
    this.simScenarioScript.initiatorOther = false;
    this.simScenarioScript.initiatorInstructor = false;

    if (selectedInitiator === 'Other') {
      this.simScenarioScript.initiatorOther = true;
      this.simScenarioScript.initiatorId = null; 
    } 
    else if (selectedInitiator === 'Instructor') {
      this.simScenarioScript.initiatorInstructor = true;
      this.simScenarioScript.initiatorId = null;
    } 
    else {
      this.simScenarioScript.initiatorId = selectedInitiator;
    }
    this.simScenarioScript.title = this.addScriptForm.get('scriptTitle')?.value;
    this.simScenarioScript.description = this.addScriptForm.get('scriptDescription')?.value;
    this.simScenarioScript.time = this.convertToDateTime();
    this.simScenarioScript.eventId =this.editEventId;
  }

  async addScriptAsync() {
    this.setCreateUpdateOptions();
    this.simScenarioScript.criterias = [];
    const selectedCriterias = this.addScriptForm.get('taskCriteriaControl')?.value ?? [];
     if (selectedCriterias.id != null) {
      this.simScenarioScript.criterias.push({
        id: null,
        criteriaId: selectedCriterias.id
      });
    }
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
    const selectedCriteria = this.addScriptForm.get('taskCriteriaControl')?.value;
    if (selectedCriteria.id != null) {
      this.simScenarioScript.criterias.push({
        id: this.editScriptDetails?.criterias.find(x => x.criteriaId === selectedCriteria.id)?.id ?? null,
        criteriaId: selectedCriteria.id
      });
    }
    await this.simScenarioService.updateScriptAsync(this.scriptId,this.editEventId, this.simScenarioScript).then((res) => {
      this.alert.successToast('Simulator Scenario Script Updated Successfully');
      this.scriptUpdated.emit(res);
      this.closed.emit();
    }).catch(() => {
      this.alert.errorToast('Simulator Scenario Script Not Updated');
    });
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
    this.filteredSimulatorScenario_Positions =  this.simulatorScenario_PositionsList.filter((x) => {
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
