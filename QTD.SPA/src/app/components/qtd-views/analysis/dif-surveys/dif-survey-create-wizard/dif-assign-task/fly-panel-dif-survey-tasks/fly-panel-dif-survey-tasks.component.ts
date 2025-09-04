import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DIFSurveyTaskLinkOptions } from '@models/DIFSurvey/DIFSurveyTaskLinkOptions';
import { DIFSurveyTaskVM } from '@models/DIFSurvey/DIFSurveyTaskVM';
import { DutyAreaTreeVM } from '@models/TreeVMs/TaskTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiDifSurveyTaskService } from 'src/app/_Services/QTD/DifSurveyTask/api.difsurvey-task.service';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-dif-survey-tasks',
  templateUrl: './fly-panel-dif-survey-tasks.component.html',
  styleUrls: ['./fly-panel-dif-survey-tasks.component.scss'],
})
export class FlyPanelDifSurveyTasksComponent implements OnInit {
  @Input() alreadyLinkedTasks : DIFSurveyTaskVM[];
  @Input() difSurveyId : string;
  @Input() positionId : string;
  @Output() updatedTasks = new EventEmitter<DIFSurveyTaskVM[]>();
  searchText: string;
  selectedFilter: string;
  selectedTaskIds:string[]=[];
  dutyAreas: DutyAreaTreeVM[] = [];
  treeControl = new NestedTreeControl<TaskTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TaskTree>();
  originalSource = new MatTreeNestedDataSource<TaskTree>();
  hasChild = (_: number, node: TaskTree) =>
      !!node.children && node.children.length > 0;
  
  taskCheckListSelection = new SelectionModel<TaskTree>(true);
  showLoader:boolean = false;
  showOnlySelected = false;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private difSurveyTaskService: ApiDifSurveyTaskService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    private dutyAreaService:DutyAreaService
  ) {}

  ngOnInit(): void {
    this.searchText = '';
    this.readyTasksTreeData();
    this.selectedFilter = 'allActive';
  }

  async readyTasksTreeData() {
      this.showLoader=true;
      await this.dutyAreaService.getMinimizedDataForTree().then((res: DutyAreaTreeVM[]) => {
          this.dutyAreas = res;
          this.makeTaskTreeDataSource(res);
        }).catch((err: any) => {
          console.error(err);
        })
        .finally(()=>{
          this.showLoader=false;
          });
    }

  filterBy(s: string) {
    this.selectedFilter = s;
    this.searchTask({ target: { value: this.searchText } });
  }

  searchTask(event: any) {
    const searchTerm = event.target.value.toLowerCase();
    this.searchText = searchTerm;
    if (this.selectedFilter) {
      this.applyFilterAndSearch(this.selectedFilter);
    } else {
      this.setFilteredDataSource(null);
    }
  }

  setFilteredDataSource(condition: ((item: any) => boolean) | null) {
    function filterTree(node) {
      if (!node.children || node.children.length === 0) return node;
  
      node.children = node.children
        .map(filterTree)
        .filter(child => child.children?.length > 0 || (child.children?.length === 0 && !child.children));
  
      if (node.children.length === 0 && node.children !== undefined) {
        return null;
      }
      return node;
    }
  
    let temparr = this.originalSource.data.map((element) => {
      let filteredChildren = element.children?.map((e) => {
        let filteredGrandchildren = e.children?.filter((c) => {
          const matchesSearch = c.description.toLowerCase().includes(String(this.searchText).toLowerCase());
          const matchesCondition = condition ? condition(c) : true;
          return matchesSearch && matchesCondition;
        }) || [];
        return {
          ...e,
          children: filteredGrandchildren
        };
      }).filter(e => e.children && e.children.length > 0) || [];
  
      return {
        ...element,
        children: filteredChildren
      };
    }).filter(element => element.children && element.children.length > 0);
  
    this.dataSource.data = temparr;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });
  
    this.dataSource.data.forEach((node) => {
      this.updateNodeSelectionState(node);
    });
    this.treeControl.dataNodes = this.dataSource.data;
    this.searchText.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }
  
  applyFilterAndSearch(filter: string) {
    switch (filter) {
      case 'tasksWithSurveyPosition':
        this.setFilteredDataSource(item=>item.taskPositionIds?.includes(this.positionId));
        break;
      case 'metaTasksWithSurveyPosition':
        this.setFilteredDataSource(item=>item.taskPositionIds?.includes(this.positionId) && item.isMeta);
        break;
      case 'rrTasksWithSurveyPosition':
        this.setFilteredDataSource(item=>item.taskPositionIds?.includes(this.positionId) && item.isReliability);
        break;
      case 'nonRRTasksWithSurveyPosition':
        this.setFilteredDataSource(item=>item.taskPositionIds?.includes(this.positionId) && !item.isReliability);
        break;
      default:
        this.setFilteredDataSource(null);
        break;
    }
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    } else {
      this.taskCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: TaskTree) {
    node.selected = node.checkbox ? checked : false;
  
    if (node.children && node.children.length > 0) {
      node.children.forEach(child => {
        this.itemToggle(checked, child);
      });
    } else {
      if (node.selected && node.checkbox) {
        if (!this.selectedTaskIds.includes(node.id)) {
          this.selectedTaskIds.push(node.id);
        }
      } else {
        const index = this.selectedTaskIds.indexOf(node.id);
        if (index > -1) {
          this.selectedTaskIds.splice(index, 1);
        }
      }
    }
  
    this.selectedTaskIds = [...new Set(this.selectedTaskIds)];
    this.checkAllParents(node);
  }
  

  async addTaskToDifSurveyAsync(){
    var linkOptions= new DIFSurveyTaskLinkOptions();
    linkOptions.difSurveyId = this.difSurveyId;
    linkOptions.taskIds = Array.from(new Set(this.selectedTaskIds));
    await this.difSurveyTaskService.linkTasksAsync(linkOptions).then(async res=>{
      this.updatedTasks.emit(res);
      this.alert.successToast(await this.labelPipe.transform('Task') +"(s) Successfully Added");
    })
  }

  makeTaskTreeDataSource(dutyArea: DutyAreaTreeVM[]) {
      var treeData: TaskTree[] = [];
      dutyArea.filter(x=>x.active).forEach((da,i)=>{
        treeData.push({
          id:da.id,
          description:da.letter + da.number + ' - ' + da.title,
          children:[],
          checkbox:true,
          selected:false,
          indeterminate:false,
        });
        da.subdutyAreas.filter(x=>x.active).forEach((sda,j)=>{
          treeData[i].children.push({
            id:sda.id,
            children:[],
            description:da.letter + da.number + '.' + sda.subNumber + ' - ' + sda.title,
            checkbox:true,
            selected:false,
            indeterminate:false,
          });
          sda.tasks.filter(x=>x.active).forEach((task)=>{
            treeData[i].children[j].children.push({
              id:task.id,
              checkbox: !this.alreadyLinkedTasks.map(t=>t.taskId).includes(task.id),
              children:[],
              selected:false,
              description:da.letter + da.number + '.' + sda.subNumber + '.' + task.number + ' - ' + task.description,
              active:task.active,
              isMeta: task.isMeta,
              isReliability:task.isReliability,
              taskPositionIds: task.position_Tasks.map(pt=>pt.positionId)
            })
          })
        })
      })
      this.dataSource.data = Object.assign(treeData);
      this.originalSource.data = Object.assign(this.dataSource.data);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
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
  
    private checkAllParents(node: TaskTree) {
      if (node.parent) {
        const descendants = this.treeControl.getDescendants(node.parent);
        node.parent.selected = descendants.every((child) => child.selected);
        node.parent.indeterminate = descendants.some((child) => child.selected);
        this.checkAllParents(node.parent);
      }
    }

    public hideLeafNode(node: any) {
      return this.showOnlySelected && !node.selected
        ? true
        : new RegExp(this.searchText, 'i').test(node.description) === false;
    }
  
    public hideParentNode(node: any) {
      if (!node.children || node.children.length === 0) return true;
      return this.treeControl
        .getDescendants(node)
        .filter(child => !child.children || child.children.length === 0)
        .every(child => this.hideLeafNode(child));
    }

    masterToggle() {
      const selectableTasks = this.getAllSelectableLeafNodes();
    
      const allSelected = selectableTasks.every(row => this.taskCheckListSelection.isSelected(row));
    
      if (allSelected) {
        selectableTasks.forEach(row => {
          this.taskCheckListSelection.deselect(row);
          this.itemToggle(false, row);
        });
      } else {
        selectableTasks.forEach(row => {
          this.taskCheckListSelection.select(row);
          this.itemToggle(true, row);
        });
      }
    
      this.dataSource.data.forEach(node => this.updateNodeSelectionState(node));
    }
    
    
    isAllSelected() {
      const selectableTasks = this.getAllSelectableLeafNodes();
      return selectableTasks.length > 0 && selectableTasks.every(row => this.taskCheckListSelection.isSelected(row));
    }

    private getAllSelectableLeafNodes(): TaskTree[] {
      const result: TaskTree[] = [];
    
      const traverse = (node: TaskTree) => {
        if (node.children && node.children.length > 0) {
          node.children.forEach(child => traverse(child));
        } else if (node.checkbox) {
          result.push(node);
        }
      };
    
      this.dataSource.data.forEach(node => traverse(node));
      return result;
    }
    

    private updateNodeSelectionState(node: TaskTree): void {
      if (node.children && node.children.length > 0) {
        node.children.forEach(child => this.updateNodeSelectionState(child));
    
        const allSelected = node.children.every(child => child.selected);
        const someSelected = node.children.some(child => child.selected || child.indeterminate);
    
        node.selected = allSelected;
        node.indeterminate = !allSelected && someSelected;
      } else {
        node.selected = this.selectedTaskIds.includes(node.id);
        node.indeterminate = false;
      }
    }
    
    
}


class TaskTree {
  id: any;
  description: string;
  children!: TaskTree[];
  checkbox?: boolean;
  checked?: boolean;
  active?:boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
  isMeta?:boolean;
  isReliability?:boolean;
  taskPositionIds?:any[];
}