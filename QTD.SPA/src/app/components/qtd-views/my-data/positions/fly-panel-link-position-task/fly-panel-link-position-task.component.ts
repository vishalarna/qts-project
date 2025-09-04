import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { AnyPtrRecord } from 'dns';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Position_Task_LinkOptions } from 'src/app/_DtoModels/Position_Task_Link/Position_Task_Link';
import { SH_Task_LinkOptions } from 'src/app/_DtoModels/SH_Task_Link/SH_Task_LinkOptions';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-link-position-task',
  templateUrl: './fly-panel-link-position-task.component.html',
  styleUrls: ['./fly-panel-link-position-task.component.scss']
})
export class FlyPanelLinkPositionTaskComponent implements OnInit, AfterViewInit, OnDestroy {
  filterTaskString: string;
  linkTasks: boolean = true;
  addTask: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<TaskTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TaskTree>();
  originalSource = new MatTreeNestedDataSource<TaskTree>();
  hasChild = (_: number, node: TaskTree) =>
    !!node.children && node.children.length > 0;

  taskCheckListSelection = new SelectionModel<TaskTree>(true);
  showLinkTaskLoader: boolean = false;
  posId = "";
  subscription = new SubSink();
  isTaskLoading = false;
  isTaskAvailable = false;
  showOnlySelected = false;

  constructor(private dutyAreaService: DutyAreaService,
    private posService: PositionsService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.posId = res.id;
      this.readyTasksTreeData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyTasksTreeData() {

    this.isTaskLoading = true;
    await this.dutyAreaService
      .getMinimizedDataForTree()
      .then((res) => {
        this.makeTaskTreeDataSource(res);
        this.isTaskLoading = false;
      })
      .finally(() => {
        this.isTaskLoading = false;
      });
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(res: DutyAreaTreeVM[]) {
    var treeData = [];
    if(res.length > 0){
      this.isTaskAvailable =true;
    }
    res.forEach((da, i) => {
      treeData.push({
        description: `${da.letter} ${da.number} - ${da.title}`,
        id: da.id,
        children: [],
        checkbox: true,
        selected: false,
      })
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children?.push({
          description: `${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id: sda.id,
          children: [],
          checkbox: true,
          selected: false,
        })
        sda.tasks.forEach((task) => {
          if (task.active) {
            treeData[i]?.children[j]?.children?.push({
              description: `${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id: task.id,
              checkbox: !this.alreadyLinked.includes(task.id),
              selected: false,
              isTask: true,
            })
          }
        })
      })
    })
    this.dataSource.data = treeData;
    this.originalSource.data = this.dataSource.data;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
    });

    //this.filterData(true);

    this.isTaskLoading = false;
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    } else {
      this.taskCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  noDataCheck=false;
  filterData(toFilter: any) {
    debugger;
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return {
              ...e,
              children: e.children?.filter((c) => {
                return c.description
                  .toLowerCase()
                  .match(String(this.filterTaskString).toLowerCase());
              }),
            };
          }),
        };
      }),
    ];
    let hasData = temparr.some((element) => element.children?.some((e) => e.children?.length > 0));

  if (hasData) {
    this.dataSource.data = temparr;

    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node) => {
      node.children?.forEach((ila) => {
        this.checkAllParents(ila);
      });
    });

    this.treeControl.dataNodes = this.dataSource.data;
    this.filterTaskString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    this.noDataCheck = false;
  } else {
    this.dataSource.data = [];
    this.noDataCheck = true;
  }
}
  itemToggle(checked: boolean, node: TaskTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (node.selected) {
        this.linkedIds.push(node.id)
      }
      else {
        const index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      };
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
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

  refreshTasksTree() {
    this.taskCheckListSelection.clear();
    this.linkedIds = [];
    this.readyTasksTreeData();
  }

  filterActive(makeActive: boolean) {
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.filter((c) => {
                return c.active == makeActive;
              })
            }
          }
          ),
        };
      }),
    ];
    this.showActive = makeActive;

    this.dataSource.data = temparr;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  linkTasksToPosition() {
    this.showLinkTaskLoader = true;
    var options = new Position_Task_LinkOptions();
    options.positionId = this.posId;
    options.taskIds = [];
    options.taskIds = this.linkedIds;
    this.posService.LinkTasks(this.posId, options).then(async (res: any) => {
      this.refresh.emit();
      this.refreshLinkedTasks();
      this.alert.successToast(await this.transformTitle('Task') +"(s) Linked To "+ await this.transformTitle('Position'));
      this.closed.emit('fp-pos-task-link-closed');
    }).finally(()=>{
      this.showLinkTaskLoader = false;
    });
  }

  refreshLinkedTasks() {
    this.taskCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinked.push(id);
    });
    this.linkedIds = [];
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filterTaskString, 'i').test(node.description) === false;
  }

  public hideParentNode(node: any) {
    return this.treeControl
      .getDescendants(node)
      .filter(node => node.children == null || node.children.length === 0)
      .every(node => this.hideLeafNode(node));
  }

  clearSearch:string;
  clearFilter(){
  this.filterTaskString = '';
  this.readyTasksTreeData();

  }
}

class TaskTree {
  id: any;
  description: string;
  children?: TaskTree[];
  active?: boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
}
