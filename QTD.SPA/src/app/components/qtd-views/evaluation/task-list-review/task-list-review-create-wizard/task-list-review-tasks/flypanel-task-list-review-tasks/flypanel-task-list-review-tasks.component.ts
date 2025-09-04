import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { PositionIdsModel } from '@models/Position/PositionIdsModel';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-task-list-review-tasks',
  templateUrl: './flypanel-task-list-review-tasks.component.html',
  styleUrls: ['./flypanel-task-list-review-tasks.component.scss'],
})
export class FlypanelTaskListReviewTasksComponent implements OnInit {
  @Input() alreadyLinked: string[] = [];
  @Output() taskIdsToLink = new EventEmitter<string[]>();
  @Input() positionIds: string[] = [];
  searchText: string;
  taskData: any[];
  taskDataSource = new MatTreeNestedDataSource<any>();
  originalSource = new MatTreeNestedDataSource<any>();
  taskCheckListSelection = new SelectionModel<any>(true);
  treeControl = new NestedTreeControl<any>((node: any) => node.children);
  hasChild = (_: number, node: any) =>
    !!node.children && node.children.length > 0;
  displayedColumns: string[];
  isTaskLoading: boolean;
  showOnlySelected: boolean;
  linkedIds: any[] = [];
  selectedFilter:string;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private _taskPipe: TaskSortPipePipe,
    private dutyAreaService: DutyAreaService,
  ) {}

  ngOnInit(): void {
    this.searchText = '';
    this.displayedColumns = ['id', 'taskNumber', 'description'];
    this.isTaskLoading = false;
    this.readyTasksTreeData();
    this.linkedIds = [];
    this.showOnlySelected = false;
    this.selectedFilter = 'all';
  }

  async readyTasksTreeData() {
    this.isTaskLoading = true;
    var option = new PositionIdsModel();
    option.positionIds = [...this.positionIds];
    await this.dutyAreaService
      .getTaskTreeDataByPositionAsync(option)
      .then((res) => {
        this.makeTaskTreeDataSource(res);
      })
      .finally(() => {
        this.isTaskLoading = false;
      });
  }

  makeTaskTreeDataSource(res: any[]) {
    var treeData = [];
    res.forEach((da, i) => {
      treeData.push({
        description: `${da.letter} ${da.number} - ${da.title}`,
        id: da.id,
        children: [],
        checkbox: true,
        selected: false,
      });
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children?.push({
          description: `${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id: sda.id,
          children: [],
          checkbox: true,
          selected: false,
        });
        sda.tasks.forEach((task) => {
          if (task.active) {
            treeData[i]?.children[j]?.children?.push({
              description: `${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id: task.id,
              checkbox: !this.alreadyLinked.includes(task.id),
              selected: this.alreadyLinked.includes(task.id),
              isTask: true,
              isMeta: task.isMeta,
              isReliablity: task.isReliability,
            });
          }
        });
      });
    });
    this.taskDataSource.data = treeData;
    this.originalSource.data = this.taskDataSource.data;
    Object.keys(this.taskDataSource.data).forEach((key: any) => {
      this.setParent(this.taskDataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
    });
    this.updateParentSelection(treeData);
    this.isTaskLoading = false;
  }

  updateParentSelection(nodes: any[]) {
    nodes.forEach(node => {
        if (node.children && node.children.length > 0) {
            
            this.updateParentSelection(node.children);

            const allChildrenSelectedOrLinked = node.children.every(
                (child: any) => child.selected || this.alreadyLinked.includes(child.id)
            );
            const anyChildSelectedOrLinked = node.children.some(
                (child: any) => child.selected || this.alreadyLinked.includes(child.id) || child.indeterminate
            );

            node.selected = allChildrenSelectedOrLinked;
            node.indeterminate = !allChildrenSelectedOrLinked && anyChildSelectedOrLinked;
        }
    });
}

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    } else {
      this.taskCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: any) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      
      if (node.selected) {
        this.linkedIds.push(node.id);
      } else {
        const index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  private checkAllParents(node: any) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  private setParent(node: any, parent: any | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.searchText, 'i').test(node.description) === false || (!(node?.isTask ?? false));
  }

  public hideParentNode(node: any) {
    return this.treeControl
      .getDescendants(node)
      .filter((node) => node.children == null || node.children.length === 0)
      .every((node) => this.hideLeafNode(node));
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

  applyFilterAndSearch(filter: string) {
    switch (filter) {
      case 'rr':
        this.setFilteredDataSource(item=>item.isReliablity);
        break;
      case 'non-rr':
        this.setFilteredDataSource(item=>!item.isReliablity);
        break;
      case 'meta':
        this.setFilteredDataSource(item=>item.isMeta);
        break;
      default:
        this.setFilteredDataSource(null);
        break;
    }
  }

  addTaskToTaskListReview() {
    this.taskIdsToLink.emit(this.linkedIds);
    this.flyPanelSrvc.close();
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
  
    temparr.forEach(node => {
      this.setParent(node, undefined);
      this.updateNodeSelectionState(node); 
    });
    this.taskDataSource.data = temparr;
    this.treeControl.dataNodes = this.taskDataSource.data;
    this.searchText.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  private updateNodeSelectionState(node: any): void {
    if (node.children && node.children.length > 0) {
      node.children.forEach(child => this.updateNodeSelectionState(child));
  
      const allChildrenSelected = node.children.every(child => child.selected);
      const someChildrenSelected = node.children.some(child => child.selected || child.indeterminate);
  
      node.selected = allChildrenSelected;
      node.indeterminate = !allChildrenSelected && someChildrenSelected;
    } else {
      node.selected = this.linkedIds.includes(node.id);
      node.indeterminate = false;
    }
  }

  isAllSelected() {
    const selectableTasks = this.getAllSelectableLeafNodes();
    return selectableTasks.length > 0 && selectableTasks.every(row => this.taskCheckListSelection.isSelected(row));
  }

  private getAllSelectableLeafNodes(): any[] {
    const result: any[] = [];
  
    const traverse = (node: any) => {
      if (node.children && node.children.length > 0) {
        node.children.forEach(child => traverse(child));
      } else if (node.checkbox) {
        result.push(node);
      }
    };
  
    this.taskDataSource.data.forEach(node => traverse(node));
    return result;
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
  
    this.taskDataSource.data.forEach(node => this.updateNodeSelectionState(node));
  }

}
