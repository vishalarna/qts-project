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
  selector: 'app-fly-panel-task-data-element',
  templateUrl: './fly-panel-task-data-element.component.html',
  styleUrls: ['./fly-panel-task-data-element.component.scss']
})
export class FlyPanelTaskDataElementComponent implements OnInit {
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
    this.filterTask();
  }
 
  clearSearchString() {
    this.filterSearchString = '';
    this.filterTask();
  }
 
  filterActive(filterType: boolean) {
    this.showActive = filterType;
    this.filterTask();
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
          if (!task.isMeta) {
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
    this.filterTask();
 
  }
 
  filterTask() {
    this.loader = true;
    let tempData = this.originalDataSource.filter(f => this.filterNode(f))
        .map(n => ({
            ...n,
            children: n.children
                ?.filter(s => this.filterNode(s))
                ?.map(s => ({
                    ...s,
                    children: s.children
                        ?.filter(c => this.filterNode(c) && this.matchesSearch(c))
                }))
        }));
 
    this.dataSource.data = tempData.filter(x => x.children?.length > 0 && x.children.some(y => y.children?.length > 0));
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterSearchString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
 
    this.dataSource.data.forEach(data =>
        data.children.forEach(elm =>
            elm.children.forEach(child => {
                if (this.inputTrainingIssueDataElementVM?.dataElementId == child.id) {
                    this.treeControl.expand(data);
                    this.treeControl.expand(elm);
                }
            })
        )
    );
    this.loader = false;
  }
 
  filterNode(node) {
      return node.active === this.showActive ||
          (node.children && node.children.some(child =>
              child.active === this.showActive ||
              (child.children && child.children.some(subChild =>
                  subChild.active === this.showActive
              ))
          ));
  }
 
  matchesSearch(node) {
      return node.description.toLowerCase().includes(this.filterSearchString.toLowerCase().trim());
  }
 
  private setParent(node: TaskTree, parent: TaskTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }
 
  onChangeTask(selected: boolean, node: TaskTree) {
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
   return node.children?.some(x => this.isTaskSelected(x));
  }
 
  isTaskSelected(node: TaskTree) {
    return this.linkedId != null ? this.linkedId == node.id : false;
  }
 
  linkTask() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }
}