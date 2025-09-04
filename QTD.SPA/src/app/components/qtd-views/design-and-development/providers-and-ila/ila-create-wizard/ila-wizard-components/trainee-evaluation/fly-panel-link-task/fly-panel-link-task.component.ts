import { SimulatorScenarioTaskObjectives_LinkOptions } from './../../../../../../../../_DtoModels/SimulatorScenarioTaskObjectives_Link/SimulatorScenarioTaskObjectives_LinkOptions';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { SelectionModel } from '@angular/cdk/collections';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';

@Component({
  selector: 'app-fly-panel-link-task',
  templateUrl: './fly-panel-link-task.component.html',
  styleUrls: ['./fly-panel-link-task.component.scss']
})
export class FlyPanelLinkTaskComponent implements OnInit {
  @Output() taskId = new EventEmitter<any>();
  @Output() eoId = new EventEmitter<any>();
  @Input() isTaskCheck:boolean;

  treeControl = new NestedTreeControl<Tasks>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<Tasks>();
  enablingObjectiveDataSource = new MatTreeNestedDataSource<Tasks>();
  taskCheckListSelection = new SelectionModel<Tasks>(true);
  EOCheckListSelection = new SelectionModel<Tasks>(true);
  EOTreeControl = new NestedTreeControl<Tasks>((node: any) => node.children);
  filterTaskString:string;
  taskIds:any=[];
  eoIds:any=[];

  hasChild = (_: number, node: Tasks) =>
    !!node.children && node.children.length > 0;


  constructor(public flyPanelSrvc: FlyInPanelService,
    private dutyAreaService: DutyAreaService,
    public _eoCatSrvc: EnablingObjectivesCategoryService,) { }

  ngOnInit(): void {
    if(this.isTaskCheck){
      this.readyTasksTreeData();
    }
    else if(!this.isTaskCheck){
      this.readyEnablingObjectiveTreeData();
    }
    
    //this.dataSource.data = TREE_DATA;
  }

  async readyTasksTreeData() {
    await this.dutyAreaService.getWithSubdutyAreas().then((res: any) => {
      this.makeTaskTreeDataSource(res);
      
    }).catch((err: any) => {
      console.error(err);
    })
  }

  makeTaskTreeDataSource(res: any) {
    var treeData: any = [{}];
    for (var data in res) {
      
      treeData[data] = { description: res[data]['description'], children: res[data]['subdutyAreas'], checkbox: true };
      
      for (var data1 in treeData[data]['children']) {
        treeData[data]['children'][data1] = { description: res[data]['subdutyAreas'][data1]['description'], children: res[data]['subdutyAreas'][data1]['tasks'], checkbox: true };
      }
    }
    this.dataSource.data = treeData;
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    }
    else {
      this.taskCheckListSelection.deselect(node);
    }
  }

  filtered(node:any){
    
    return node.description.includes(this.filterTaskString);
  }

  async linkTaskScenario(){
    var selectedTasks: any = this.taskCheckListSelection.selected;
    var previousTaskIds: any = this.taskIds;
    this.taskIds = [];
    for (var data in selectedTasks) {
      this.taskIds.push(selectedTasks[data])
    }
    this.taskIds = this.taskIds.filter((x: any) => !previousTaskIds.includes(x));
    
    this.taskId.emit(this.taskIds);
    this.flyPanelSrvc.close();
  }

  async linkEnablingObjectives() {
    var selectedEO: any = this.EOCheckListSelection.selected;
    var previousEOIds: any = this.eoIds;
    this.eoIds = [];
    for (var data in selectedEO) {
      this.eoIds.push(selectedEO[data]);
    }
    this.eoIds = this.eoIds.filter((x: any) => !previousEOIds.includes(x));
    
    this.eoId.emit(this.eoIds);
    this.flyPanelSrvc.close();
  }

  onEOChange(event: any, node: any) {
    if (event.checked) {
    this.EOCheckListSelection.select(node);
    }
    else{
      this.EOCheckListSelection.deselect(node);
    }
  }

  async readyEnablingObjectiveTreeData() {
    await this._eoCatSrvc.getAllWithSubCategories().then((res: any) => {
      this.readyEOTreeData(res);
    }).catch((err) => {
      console.error(err);
    })
  }

  // Modifying The Enabling Objectives data received to be used without changes to the UI (Trying to make this less messy)
  readyEOTreeData(eo: any) {
    var treeDataEO: any = [{}];
    for (var data in eo) {
      treeDataEO[data] = {
        description: eo[data]['description']
        , children: eo[data]['enablingObjective_SubCategories']
        , checkbox: true
      };
      for (var data1 in treeDataEO[data]['children']) {
        treeDataEO[data]['children'][data1] = {
          description: eo[data]['enablingObjective_SubCategories'][data1]['description']
          , children: eo[data]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics']
          , checkbox: true
        };
        for (var data2 in treeDataEO[data]['children'][data1]['children']) {
          treeDataEO[data]['children'][data1]['children'][data2] = {
            description: eo[data]['enablingObjective_SubCategories'][data1]['children'][data2]['description']
            , children: eo[data]['enablingObjective_SubCategories'][data1]['children'][data2]['enablingObjectives']
            , checkbox: true
          }
        }
      }
    }
    this.enablingObjectiveDataSource.data = treeDataEO;
  }
}

class Tasks {
  name: string;
  children?: Tasks[];
  checkbox?: boolean = false;
}

