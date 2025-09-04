import { SimulatorScenarioTaskObjectives_LinkOptions } from './../../../../../../../_DtoModels/SimulatorScenarioTaskObjectives_Link/SimulatorScenarioTaskObjectives_LinkOptions';
import { SimulatorScenarioPosition_LinkOptions } from './../../../../../../../_DtoModels/SimulatorScenarioPosition_Link/SimulatorScenarioPosition_LinkOptions';
import { SimulatorScenarioService } from './../../../../../../../_Services/QTD/simulator-scenario.service';
import { SimulatorScenarioCreateOptions } from './../../../../../../../_DtoModels/SimulatorScenario/SimulatorScenarioCreateOptions';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { Store } from '@ngrx/store';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SimulatorScenario_EnablingObjectives_LinkOptions } from 'src/app/_DtoModels/SimulatorScenario_EnablingObjectives_Link/SimulatorScenario_EnablingObjectives_LinkOptions';

@Component({
  selector: 'app-new-simulation',
  templateUrl: './new-simulation.component.html',
  styleUrls: ['./new-simulation.component.scss'],
})
export class NewSimulationComponent implements OnInit {
  @Output() new_event = new EventEmitter<any>();
  @Output() new_text = new EventEmitter<any>();

  ILAName: string = 'Intro To QTD';
  DataSource: MatTableDataSource<any>;
  EODataSource: MatTableDataSource<any>;
  displayColumns: string[] = ['number', 'type', 'description'];
  senario_positions: any[] = [];
  positionsControl = new UntypedFormControl([]);
  positions: Position[] = [];
  positionList: any[] = [];
  Title_text: string;
  View_Scenario_text: string = '2022 Annual System Restoration';
  createNewSimulation: UntypedFormGroup = new UntypedFormGroup({});
  taskid:any;
  isTaskCheck:boolean;
  eoid:any;

  public Editor = ckcustomBuild;
  constructor(
    private positionService: PositionsService,
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private saveStore: Store<{ saveIla: any }>,
    private simScenarioService : SimulatorScenarioService,
    private alert: SweetAlertService,
  ) {}

  ngOnInit(): void {

    this.saveStore.select('saveIla').pipe().subscribe((res) => {
      if (res['saveData']['result'] !== undefined) {

        this.ILAName = res['saveData']['result']['name'];

      }
    });
    this.readyNewSimulatorForm();


     let tempSrc: any[] = [
       /* {
        number: '1.1.1.3',
        type: 'Task',
        description:
          'Describe the purpose and process for monitoring and responding to substation alarms',
      },
      {
        number: '1.1.1.3',
        type: 'Task',
        description:
          'Describe automated systems used for controlling substation equipment',
      },
      {
        number: '1.1.1.3',
        type: 'Task',
        description:
          'List typical subsstation alarms and the required response for each',
      },  */
    ];
    this.DataSource = new MatTableDataSource(tempSrc);

    //for dynamic position dropdown
    this.positionService.getAllWithoutIncludes().then((i) => {
      this.positions = i;
    });
    this.positionList.push(this.positionsControl);
  }

  onReady(editor: any) {
    //
  }

  readyNewSimulatorForm(){
    this.createNewSimulation.addControl('ScenarioTitle', new UntypedFormControl('', Validators.required));
    this.createNewSimulation.addControl('ScenarioDescription', new UntypedFormControl('', Validators.required));
  }

  OnTitleChange() {

  }

  removePosition(i: any) {
    
    const pos = this.positionsControl.value as Position[];
    this.removeFirst(pos, i);
    this.positionsControl.setValue(pos);
  }

  private removeFirst(array: Position[], toRemove: Position): void {
    
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  OnClick(selected: boolean) {
    
    
    if (selected) {
      this.positionList.push(this.positionsControl);
    }

  }

  recieveTaskMessage(event:any){
    
    this.taskid=event;

    let tempSrc:any=[];
    this.taskid.forEach((i:any) => {
      tempSrc.push({
        number: /* `${i.majorVersion}.${i.minorVersion}.${i.number} `*/ '1.1.' + (i.number),
        type : 'Task',
        description : i.description
      })
    });
    this.EODataSource = new MatTableDataSource(tempSrc);
  }

  recieveEOMessage(event:any){
    this.eoid=event;

    let tempSrc:any=[];
    this.eoid.forEach((i:any) => {
      tempSrc.push({
        number: /* `${i.majorVersion}.${i.minorVersion}.${i.number}` */ '1.1.' + (i.number),
        type : 'EO',
        description : i.description
      })
    });
    this.DataSource = new MatTableDataSource(tempSrc);
  }

  async onSaveClick() {
    //api call for SimulatorScenario
    var options : SimulatorScenarioCreateOptions = new SimulatorScenarioCreateOptions();
    options.simScenarioDiffID = "xY";
    options.simScenarioTitle = this.createNewSimulation.get("ScenarioTitle")?.value;
    options.simScenarioDesc = this.createNewSimulation.get("ScenarioDescription")?.value;
     options.simScenarioDurationHours = 0;
    options.simScenarioDurationMins =  0;



     await this.simScenarioService.create(options).then((res)=>{

      this.linkPosition(res.id);
      this.LinkTaskObjectives(res.id);
      this.linkEnablingObjectives(res.id);
      this.new_event.emit(false);
      this.new_text.emit(this.createNewSimulation.get("ScenarioTitle")?.value);
    }).catch((err)=>{
      this.alert.errorToast('Title Already exists')

    });

  }

  async linkPosition(id:any){
    let positionArray:any=[];
    this.positionsControl.value.forEach((element:any) => {

      positionArray.push(element.id);
    });

    var options : SimulatorScenarioPosition_LinkOptions = new SimulatorScenarioPosition_LinkOptions();
    options.simulatorScenarioID = id;
    options.positionIds = positionArray;

    await this.simScenarioService.linkPositions(id, options).then((res)=>{
    }).catch((err)=>{

    });
  }

  async LinkTaskObjectives(id:any){
    var linkOptions : SimulatorScenarioTaskObjectives_LinkOptions = new SimulatorScenarioTaskObjectives_LinkOptions();

    let linkTaskIds:any = [];
    this.taskid.forEach((i:any) => {
      linkTaskIds.push(i.id);
    });

    linkOptions.simulatorScenarioID= id;
    linkOptions.taskIds = linkTaskIds;


     await this.simScenarioService.linkTaskObjectives(id, linkOptions).then((res: any) => {
    }).catch((err: any) => {
      console.error(err);
    });
  }

  async linkEnablingObjectives(id:any){
    var linkOptions : SimulatorScenario_EnablingObjectives_LinkOptions = new SimulatorScenario_EnablingObjectives_LinkOptions();

    let linkEOIds:any = [];
    this.eoid.forEach((i:any) => {
      linkEOIds.push(i.id);
    });

    linkOptions.simulatorScenarioID= id;
    linkOptions.enablingObjectiveIds = linkEOIds;


     await this.simScenarioService.linkEnablingObjectives(id, linkOptions).then((res: any) => {
    }).catch((err: any) => {
      console.error(err);
    });
  }


  openFlyPanel(templateRef: any,name:string) {
    switch(name){
      case 'Objectives':
        this.isTaskCheck=false;
        break;

      case 'Task':
        this.isTaskCheck = true;
        break;
    }
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }
}
