import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-eo-task-link',
  templateUrl: './flypanel-eo-task-link.component.html',
  styleUrls: ['./flypanel-eo-task-link.component.scss']
})
export class FlypanelEoTaskLinkComponent implements OnInit, OnDestroy, AfterViewInit {
  filterTaskString: string = "";
  linkTasks: boolean = true;
  addTask: boolean = false;
  showActive: boolean = true;
  eoId = "";
  subscription = new SubSink();
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];

  dutyAreas: DutyAreaTreeVM[] = [];

  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<TaskTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TaskTree>();
  originalSource = new MatTreeNestedDataSource<TaskTree>();
  hasChild = (_: number, node: TaskTree) =>
    !!node.children && node.children.length > 0;

  taskCheckListSelection = new SelectionModel<TaskTree>(true);
  showLinkTaskLoader: boolean = false;

  constructor(
    private dutyAreaService: DutyAreaService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private eoService: EnablingObjectivesService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyTasksTreeData();
  }

  ngAfterViewInit(): void {

    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.eoId = String(res.id).split('-')[1];
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  clearFilter() {
    this.filterTaskString = '';
    this.filterData("");
  }

  dataLoader = false;

  async readyTasksTreeData() {
    this.dataLoader = true;
    await this.dutyAreaService
      .getMinimizedDataForTree()
      .then((res: DutyAreaTreeVM[]) => {

        this.dutyAreas = res;
        this.makeTaskTreeDataSource(res);
      }).finally(() => {
        this.dataLoader = false;
      });
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(dutyArea: DutyAreaTreeVM[]) {
    var treeData = []
    dutyArea.forEach((da, i) => {
      treeData.push({
        description: `${da.letter} ${da.number} - ${da.title}`,
        id: da.id,
        children: [],
        checkbox: true,
        selected: false,
        active: da.active,
      })
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children?.push({
          description: `${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id: sda.id,
          children: [],
          checkbox: true,
          selected: false,
          active: sda.active,
        })
        sda.tasks.forEach((task) => {
          if (!task.isMeta && task.active) {
            treeData[i]?.children[j]?.children?.push({
              description: `${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id: task.id,
              checkbox: !this.alreadyLinked.includes(task.id),
              selected: false,
              isTask: true,
              active: task.active,
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

    this.filterData(true);

  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    } else {
      this.taskCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }
  filtered(node: any) {

    return node.description.includes(this.filterTaskString);
  }

  filterData(toFilter: any) {
    this.dataLoader = true;
    this.dataSource.data = [
      ...this.originalSource.data.filter((f) => {
        return f.active === true || f.children?.some((s) => {
          return s.active === true || s.children?.some((x) => {
            return x.active === true;
          });
        })
      })?.map((n) => {
        return {
          ...n,
          children: n.children?.filter((f) => {
            return f.active === true || f.children?.some((k) => {
              return k.active === true;
            })
          })?.map((s) => {
            return {
              ...s,
              children: s.children?.filter((c) => {
                return (
                  c.description.toLowerCase()
                    .trim()
                    .includes(String(this.filterTaskString).toLowerCase().trim())
                );
              }),
            };
          }),
        };
      }),
    ];

    this.dataSource.data = this.dataSource.data.filter((x) => { return x.children !== null && x.children !== undefined && x.children.length > 0 && (x.children.some((y) => { return y?.children !== null && y?.children !== undefined && y?.children.length > 0 })) })
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    // this.dataSource.data = this.dataSource.data.map((da)=>{
    //   da.children.map((sda)=>{
    //     sda.children.map((task)=>{
    //       task.checked = this.linkedIds.includes(task.id);
    //       return task;
    //     });
    //     return sda;
    //   });
    //   return da;
    // });
    //

    this.dataSource.data.forEach((node) => {
      node.children?.forEach((ila) => {
        this.checkAllParents(ila);
      });
    });

    this.treeControl.dataNodes = this.dataSource.data;
    this.filterTaskString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    this.dataLoader = false;
  }

  itemToggle(checked: boolean, node: TaskTree) {

    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (node.selected && node.checkbox) {
        this.linkedIds.push(node.id);
      } else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
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
      node.parent.selected = descendants.every((child) => (child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.selected));
      this.checkAllParents(node.parent);
    }
  }
  refreshLinkedTasks() {
    this.taskCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinked.push(id);
    });

    this.makeTaskTreeDataSource(this.dutyAreas);
    this.linkedIds = [];
  }

  refreshTasksTree() {
    this.taskCheckListSelection.clear();
    this.linkedIds = [];
    this.readyTasksTreeData();
  }

  linkTasksToEO() {
    this.showLinkTaskLoader = true;
    var options = new EO_LinkOptions();
    options.taskIds = this.linkedIds;
    options.eoId = this.eoId;
    this.eoService.linkTask(options).then(async (_) => {
      this.alert.successToast("Selected "+ await this.transformTitle('Task')  +"s linked to EO");
      this.refresh.emit();
      this.dataBroadcastService.refreshStats.next(null);
    }).finally(() => {
      this.showLinkTaskLoader = false;
    })
  }
}

class TaskTree {
  id: any;
  description: string;
  children?: TaskTree[];
  checkbox?: boolean;
  checked?: boolean;
  active?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
}
