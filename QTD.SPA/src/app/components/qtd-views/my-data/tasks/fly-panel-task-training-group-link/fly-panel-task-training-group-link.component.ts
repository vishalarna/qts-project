import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Task_TrainingGroupOptions } from 'src/app/_DtoModels/Task_TrainingGroup/Task_TrainingGroupOptions';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { TrainingGroup_Category } from 'src/app/_DtoModels/TrainingGroup_Category/TrainingGroup_Category';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { TrainingGroupService } from 'src/app/_Services/QTD/training-group.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-task-training-group-link',
  templateUrl: './fly-panel-task-training-group-link.component.html',
  styleUrls: ['./fly-panel-task-training-group-link.component.scss']
})
export class FlyPanelTaskTrainingGroupLinkComponent implements OnInit {
  filterToolString = "";
  showActive: boolean = true;
  linkedIds: any[] = [];
  spinner = false;

  treeControl = new NestedTreeControl<TrainingGroupTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TrainingGroupTree>();
  originalDataSource = new MatTreeNestedDataSource<TrainingGroupTree>();
  TaskTreeCheckListSelection = new SelectionModel<TrainingGroupTree>(true);

  hasChild = (_: number, node: TrainingGroupTree) =>
    !!node.children && node.children.length > 0;

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  @Input() taskId = "";
  constructor(
    private trainingGroupService: TrainingGroupService,
    private TaskService: TasksService,
    private alert: SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData() {
    this.TaskTreeCheckListSelection.clear();
    this.linkedIds = [];
    var data = await this.trainingGroupService.getGroupInCategories();
    this.readyTrainingGroupTree(data);
  }

  clearFilter(){
    this.filterToolString = null;
    this.readyData();
  }

  readyTrainingGroupTree(res : TrainingGroup_Category[]){
    var treeData: any[] = [];
    res.forEach((trainingCat: TrainingGroup_Category, index: any) => {
      treeData.push({ description: trainingCat.title, checkbox: true });
      treeData[index]["children"] = [];
      trainingCat.trainingGroups.forEach((group: TrainingGroup) => {
        treeData[index]["children"].push({ description: group.groupName, active: group.active, checkbox: !this.alreadyLinked.includes(group.id), id: group.id });
      })
    });
    
    this.dataSource.data = [];
    this.dataSource.data = Object.assign([],treeData);
    this.originalDataSource.data = Object.assign([],this.dataSource.data);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalDataSource.data[key], undefined);
    });
    
    this.filterActive(true);
  }

  filterData(data: any, toFilter: any) {
    if (this.filterToolString.length > 0) {
      let temparr = [
        ...this.originalDataSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {
              return (c.description.toLowerCase().match(String(this.filterToolString).toLowerCase()))
                && c.active == this.showActive;
            }
            ),
          };
        }),
      ];
      this.dataSource.data = temparr;

      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });
  
      this.dataSource.data.forEach((node: any) => {
        node.children?.forEach((child) => {
          this.checkAllParents(child);
        })
      });
  
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterToolString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();

     /*  this.dataSource.data = temparr;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });

      this.dataSource.data.forEach((node) => {
        node.children?.forEach((child) => {
          this.checkAllParents(child);
        })
      }) */
    } else {
      this.dataSource.data = this.originalDataSource.data;
      this.filterActive(this.showActive);
    }
  }

  private setParent(node: TrainingGroupTree, parent: TrainingGroupTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: TrainingGroupTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  filterActive(makeActive: boolean) {
    
    let temparr = [
      ...this.originalDataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.active == makeActive && (this.filterToolString.length > 0
              ? c.description.trim().toLowerCase().includes(this.filterToolString.trim().toLowerCase()) : true);
          }
          ),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSource.data = temparr;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    })
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.TaskTreeCheckListSelection.select(node);
    } else {
      this.TaskTreeCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: TrainingGroupTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (checked && node.checkbox) {
        this.linkedIds.push(node.id);
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  async linkTrainingGroups(){
    this.spinner = true;
    var options = new Task_TrainingGroupOptions();
    options.taskId = this.taskId;
    options.trainingGroupIds = this.linkedIds;
    await this.TaskService.linkTrainingGroups(options).then((_)=>{
      this.alert.successToast("Linked Selected Training Groups");
      this.refresh.emit();
    }).finally(()=>{
      this.spinner = false;
    })
  }

}

class TrainingGroupTree {
  id: any;
  description: string;
  children?: TrainingGroupTree[];
  checkbox?: boolean;
  active?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TrainingGroupTree;
}
