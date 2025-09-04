import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { SH_Task_LinkOptions } from 'src/app/_DtoModels/SH_Task_Link/SH_Task_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-sh-tasks-link',
  templateUrl: './fly-panel-sh-tasks-link.component.html',
  styleUrls: ['./fly-panel-sh-tasks-link.component.scss']
})
export class FlyPanelShTasksLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterTaskString: string;
  linkTasks: boolean = true;
  showLoader: boolean = true;
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
  shId = "";
  subscription = new SubSink();
  myData:any[]=[];

  constructor(private dutyAreaService: DutyAreaService,
              private shService: SafetyHazardsService,
              private alert: SweetAlertService,
              private route: ActivatedRoute,
              private labelPipe: LabelReplacementPipe) {}

  ngOnInit(): void {
    this.readyTasksTreeData();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  clearFilter(){
    this.filterTaskString = '';
    this.readyTasksTreeData();
  }

  async readyTasksTreeData() {
    this.showLoader=true;
    await this.dutyAreaService
      .getWithSubdutyAreas()
      .then((res) => {

        this.makeTaskTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      })
      .finally(()=>{
    this.showLoader=false;

      });
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(res: DutyArea[]) {
    var treeData = []
    res.forEach((da,i)=>{
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
    this.myData = Object.assign(treeData);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
      this.setParent(this.myData[key], undefined);
    });

    this.filterData();
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    } else {
      this.taskCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  filterData() {
    if (this.filterTaskString?.length ?? 0 > 0) {

      let temparr = [
        ...this.originalSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.map((e) => {

              return {
                ...e,
                children: e.children?.filter((c) => {
                  return c.description.toLowerCase().match(String(this.filterTaskString).toLowerCase());
                })
              }
            }
            ),
          };
        }),
      ];

      this.dataSource.data = temparr;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });

      this.dataSource.data.forEach((node) => {
        node.children?.forEach((child) => {
          this.checkAllParents(child);
        })
      });
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterTaskString?.length ?? 0 > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    } else {
      this.dataSource.data = this.originalSource.data;
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
        if(index > -1){
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

  refreshTasksTree(){
    this.taskCheckListSelection.clear();
    this.linkedIds = [];
    this.readyTasksTreeData();
  }

  toggleFilter(isActive: boolean) {
    var temp:any[] = Object.assign([],this.myData);
    this.dataSource.data = [
      ...temp.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.filter((f) => f.active == isActive),


              }
            })
          }
        })
        ];

    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node:TaskTree)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })
    this.showActive = isActive;
  }

  filterActive(makeActive:boolean){
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

  linkTasksToSH() {
    this.showLinkTaskLoader=true;
    var options = new SH_Task_LinkOptions();
    options.saftyHazardId = this.shId;
    options.taskIds = [];
    options.taskIds = this.linkedIds;

    this.shService.LinkTasks(this.shId, options).then(async (res: any) => {
      this.refresh.emit();
      //this.refreshLinkedTasks();
      this.alert.successToast(await this.transformTitle('Task') + "s Linked To " + await this.labelPipe.transform('Regulatory Requirement'));
    this.showLinkTaskLoader=false;

      this.closed.emit('fp-rr-task-link-closed');
    });
  }
}

class TaskTree {
  id: any;
  description: string;
  children?: TaskTree[];
  active?:boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
}
