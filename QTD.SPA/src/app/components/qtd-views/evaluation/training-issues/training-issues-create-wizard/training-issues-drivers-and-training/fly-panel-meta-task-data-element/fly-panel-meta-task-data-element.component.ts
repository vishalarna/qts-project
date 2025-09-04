import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { TaskTree } from '@models/Task/TaskTree';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { DutyAreaTreeVM } from '@models/TreeVMs/TaskTreeVM';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-meta-task-data-element',
  templateUrl: './fly-panel-meta-task-data-element.component.html',
  styleUrls: ['./fly-panel-meta-task-data-element.component.scss']
})
export class FlyPanelMetaTaskDataElementComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  treeControl = new NestedTreeControl<TaskTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TaskTree>();
  originalDataSource: TaskTree[] = [];
  treeCheckListSelection = new SelectionModel<TaskTree>(false);
  hasChild = (_: number, node: TaskTree) => !!node.children && node.children.length > 0;
  filterSearchString: string = '';
  showActive: boolean = true;
  linkedId: string;
  loader: boolean = false;
 
  constructor(
    public flyPanelService: FlyInPanelService,
    private dutyAreaService: DutyAreaService,
  ) { }
 
  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.readyTasksTreeDataAsync();
  }
 
  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterMetaTask();
  }
 
  clearSearchString() {
    this.filterSearchString = '';
    this.filterMetaTask();
  }
 
  filterActive(filterType: boolean) {
    this.showActive = filterType;
    this.filterMetaTask();
  }
 
  async readyTasksTreeDataAsync() {
    this.loader = true;
    await this.dutyAreaService.getMinimizedDataForTree().then((res: DutyAreaTreeVM[]) => {
      this.makeTaskTreeDataSource(res);
    }).finally(() => {
      this.loader = false;
    });
  }
 
  makeTaskTreeDataSource(dutyArea: DutyAreaTreeVM[]) {
    var treeData = []
    dutyArea.forEach((da, i) => {
      treeData.push({
        description: `${da.letter} ${da.number} - ${da.title}`,
        id: da.id,
        children: [],
        active: da.active,
        level: 'dutyArea',
      })
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children?.push({
          description: `${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id: sda.id,
          children: [],
          active: sda.active,
          level: 'subDutyArea',
        })
        sda.tasks.forEach((task) => {
          if (task.isMeta) {
            treeData[i]?.children[j]?.children?.push({
              description: `${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id: task.id,
              isTask: true,
              active: task.active,
            })
          }
        })
      })
    })
    this.dataSource.data = treeData;
    this.originalDataSource = Object.assign(treeData);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalDataSource[key], undefined);
    });
    this.treeControl.dataNodes = Object.assign(this.originalDataSource);
    this.filterMetaTask();
 
  }
 
  filterMetaTask() {

    const filterNode = (node) => {
      if (node.isTask) {
        return node.description.toLowerCase().includes(this.filterSearchString.toLowerCase()) && node.active === this.showActive ? { ...node, children: [] } : null;
      }
  
      const filteredChildren = node.children?.map(filterNode).filter((child) => child !== null);
  
      if (filteredChildren && filteredChildren.length > 0) {
        return {
          ...node,
          children: filteredChildren
        };
      } else {
        return null;
      }
    };
  
    const filteredData = this.originalDataSource.map(filterNode).filter((element) => element !== null);
  
    this.dataSource.data = filteredData;
    this.treeControl.dataNodes = filteredData;
  
    this.dataSource.data.forEach((data) => {
      this.setParent(data, undefined);
    });
  
    if (this.filterSearchString.length > 0) {
      this.treeControl.expandAll();
    } else {
      this.treeControl.collapseAll();
    }

    this.expandAndSelectNodeById(this.linkedId)
  }

  expandAndSelectNodeById(linkedId: string) {
    const findAndExpand = (node: any): boolean => {
      if (node.id === linkedId) {
        this.linkedId = node.id;
        return true;
      }
  
      if (node.children) {
        for (let child of node.children) {
          if (findAndExpand(child)) {
            this.treeControl.expand(node);
            return true;
          }
        }
      }
  
      return false;
    };
  
    this.dataSource.data.forEach((data) => {
      findAndExpand(data);
    });
  }
 
 
  private setParent(node: TaskTree, parent: TaskTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }
 
  onChangeMetaTask(selected: boolean, node: TaskTree) {
    this.treeCheckListSelection.clear();
    if (selected) {
      this.treeCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }
 
  isDutyOrSubDutySelected(node: TaskTree) {
    if(node.level =="dutyArea"){
      return this.checkDutyArea(node);
    }
    else if(node.level == "subDutyArea"){
      return this.checkSubDutyArea(node);
    }
      return false;
  }
 
  checkDutyArea(node: TaskTree){
    return node.children?.some(x => this.checkSubDutyArea(x));
  }
 
  checkSubDutyArea(node: TaskTree){
   return node.children?.some(x => this.isMetaTaskSelected(x));
  }
 
  isMetaTaskSelected(node: TaskTree) {
    return this.linkedId != null ? this.linkedId == node.id : false;
  }
 
  linkMetaTask() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

}
