import { SimulatorScenarioILA_LinkOptions } from './../../../../../../../../_DtoModels/SimulatorScenarioILA_Link/SimulatorScenarioILA_LinkOptions';
import { SimulatorScenarioService } from 'src/app/_Services/QTD/simulator-scenario.service';
import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { select, Store } from '@ngrx/store';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-scenario',
  templateUrl: './fly-panel-scenario.component.html',
  styleUrls: ['./fly-panel-scenario.component.scss']
})
export class FlyPanelScenarioComponent implements OnInit {
  @Input() View_Scenario_text:any;
  createNewSimulation: UntypedFormGroup = new UntypedFormGroup({});

  ILAName:string = 'Intro To QTD';
  DataSource: MatTableDataSource<any>;
  TaskDataSource: MatTableDataSource<any>;
  displayColumns: string[] = ['number', 'type', 'description'];
  senario_positions:any[]=[];
  positionsControl = new UntypedFormControl([]);
  positions: Position[] = [];
  positionList: any[] = [];
  EOArray:any=[];
  TaskArray:any=[];
  PositionArray:any=[];
  ilaId:any;
  public Editor = ckcustomBuild;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private positionService: PositionsService,
    private simScenarioService : SimulatorScenarioService,
    private saveStore: Store<{ saveIla: any }>,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe) { } 

  ngOnInit(): void {
    this.saveStore.select('saveIla').pipe().subscribe((res) => {
      if (res['saveData']['result'] !== undefined) {

        this.ILAName = res['saveData']['result']['name'];
        this.ilaId = res['saveData']['result']['id']

      }
    });

    this.readyNewSimulatorForm();

    if(this.View_Scenario_text){
      this.createNewSimulation.patchValue({
        ScenarioTitle: this.View_Scenario_text.text,
        ScenarioDescription: this.View_Scenario_text.description,
        ILANames : this.ILAName
      });
      this.getLinkedTask();
      this.getLinkedPositions();
      this.getLinkedEO();

    }



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
      }, */
    ];
  //  this.DataSource = new MatTableDataSource(tempSrc);


   // this.readyPositionDropdown();
  }

  readyNewSimulatorForm(){
    this.createNewSimulation.addControl('ScenarioTitle', new UntypedFormControl('', Validators.required));
    this.createNewSimulation.addControl('ScenarioDescription', new UntypedFormControl('', Validators.required));
    this.createNewSimulation.addControl('ILANames', new UntypedFormControl(''));
  }

  async readyPositionDropdown(){
    await this.positionService.getAllWithoutIncludes().then((i) => {
			this.positions = i;
		  });
		this.positionList.push(this.positionsControl);
  }

  async getLinkedTask(){
    
    let tempSrc:any=[];

    await this.simScenarioService.getTaskbyId(this.View_Scenario_text.id).then((res)=>{
      
      tempSrc = res;
      tempSrc.forEach((i:any)=>{
        this.TaskArray.push({
          number: /* `${i.majorVersion}.${i.minorVersion}.${i.number}` */ '1.1.' + (i.number),
          type : 'Task',
          description : i.description
        })
      })
    }).catch((err)=>{

    });
    this.TaskDataSource = new MatTableDataSource(this.TaskArray);

  }

  async getLinkedEO(){
    let tempSrc:any=[];
    await this.simScenarioService.getEOById(this.View_Scenario_text.id).then((res)=>{
      tempSrc = res;
      tempSrc.forEach((i:any)=>{
        this.EOArray.push({
          number: `${i.majorVersion}.${i.minorVersion}.${i.number}`,
          type : 'EO',
          description : i.description
        })
      })
    }).catch((err)=>{

    });
    this.DataSource = new MatTableDataSource(this.EOArray);

  }

  async getLinkedPositions(){
    let tempSrc:any=[];
   await this.simScenarioService.getPositionById(this.View_Scenario_text.id).then((res)=>{

      tempSrc = res;
      tempSrc.forEach((i:any) => {
          this.PositionArray.push({
            id:i.id,
            name :i.name
          });

      });
    }).catch((err)=>{

    });


   /*  this.PositionArray.forEach((element:any) => {
      this.positionList.push({id:element.id,
      name : element.name})
    });
     */
  }

  onReady(editor: any) {
    //
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
    
    
    if(selected){
      this.positionList.push(this.positionsControl);
    }

  }

  OnLinkILA(){
    var options : SimulatorScenarioILA_LinkOptions = new SimulatorScenarioILA_LinkOptions();
    options.simulatorScenarioID = this.View_Scenario_text.id;
    options.iLAID = this.ilaId;
    this.simScenarioService.linkILA(this.View_Scenario_text.id,options).then(async (res)=>{
      this.alert.successToast("Linked to " + await this.labelPipe.transform('ILA') +" Successfully");
    }).catch((err)=>{

    })
    this.flyPanelSrvc.close();
  }

}
