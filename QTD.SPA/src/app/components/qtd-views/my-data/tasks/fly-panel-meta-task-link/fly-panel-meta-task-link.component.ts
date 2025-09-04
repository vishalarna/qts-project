import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-meta-task-link',
  templateUrl: './fly-panel-meta-task-link.component.html',
  styleUrls: ['./fly-panel-meta-task-link.component.scss'],
})
export class FlyPanelMetaTaskLinkComponent implements OnInit {
  filterTaskString: string;
  linkTasks: boolean = true;
  addTask: boolean = false;
  showActive: boolean = true;
  taskId = '';
  subscription = new SubSink();
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  spinner = false;

  dutyAreas: DutyArea[] = [];

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
    private taskSrvc: TasksService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.readyTasksTreeData();
  }

  ngAfterViewInit(): void {

    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.taskId = String(res.id).split('-')[0];
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyTasksTreeData() {
    await this.dutyAreaService.getWithSubdutyAreas().then((res: DutyArea[]) => {
      Object.assign(this.dutyAreas, res);
      Object.freeze(res);

      this.makeTaskTreeDataSource(res);
    });
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(dutyArea: DutyArea[]) {

    var treeData: any = [];
    // for (var data in dutyArea) {
    //   treeData[data] = Object.assign({},{
    //     id: dutyArea[data]['id'],
    //     description: dutyArea[data]['letter'] + " " + dutyArea[data]['number'] + " - " +  dutyArea[data]['title'],
    //     children: dutyArea[data]['subdutyAreas'],
    //     checkbox: true,
    //     selected: false,
    //   });
    //   for (var data1 in treeData[data]['children']) {
    //
    //     treeData[data]['children'][data1] = Object.assign({},{
    //       id: dutyArea[data]['subdutyAreas'][data1]['id'],
    //       description: dutyArea[data]['letter'] + " " + dutyArea[data]['number'] + " " + dutyArea[data]['subdutyAreas'][data1]['number'] + " " + dutyArea[data]['subdutyAreas'][data1]['title'],
    //       children: Object.assign([],dutyArea[data]['subdutyAreas'][data1]['tasks'].filter(
    //         (x) => x.isMeta == false
    //       )),
    //       checkbox: true,
    //     });
    //     for (var data2 in treeData[data]['children'][data1]['children']) {
    //       treeData[data]['children'][data1]['children'][data2]['description'] = dutyArea[data]['letter'] + " " + dutyArea[data]['number'] + " " + dutyArea[data]['subdutyAreas'][data1]['number'] + " " + dutyArea[data]['subdutyAreas'][data1]['task']['number'];
    //       treeData[data]['children'][data1]['children'][data2]['checkbox'] =
    //         !this.alreadyLinked.includes(
    //           treeData[data]['children'][data1]['children'][data2]['id']
    //         );
    //     }
    //   }
    // }

    dutyArea.forEach((da,i)=>{
      treeData.push({
        description:`${da.letter} ${da.number} - ${da.title}`,
        id:da.id,
        children:[],
        checkbox:true,
        selected: false,
      })
      da.subdutyAreas.forEach((sda,j)=>{
        treeData[i].children?.push({
          description:`${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id:sda.id,
          children:[],
          checkbox:true,
          selected:false,
        })
        sda.tasks.forEach((task)=>{
          if(!task.isMeta && task.active){
            treeData[i]?.children[j]?.children?.push({
              description:`${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id:task.id,
              checkbox:!this.alreadyLinked.includes(task.id),
              selected:false,
              isTask:true,
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

  filterData(data: any, toFilter: any) {
    if (this.filterTaskString.length > 0) {
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
      this.dataSource.data = temparr;
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
    } else {
      this.dataSource.data = this.originalSource.data;
    }
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
              }),
            };
          }),
        };
      }),
    ];
    this.showActive = makeActive;

    this.dataSource.data = temparr;
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
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  async linkTasksToMetaTask() {
    this.spinner = true;
    let option: TaskOptions = {
      taskIds: this.linkedIds,
    };
    await this.taskSrvc.LinkTaskToMetaTask(this.taskId, option).then(async (res) => {
      this.alert.successToast(await this.transformTitle('Task') + 's successfully linked to Meta');
      this.refresh.emit();
      this.closed.emit();
    }).finally(()=>{
      this.spinner = false;
    });
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
  isTask:boolean = false;
}
