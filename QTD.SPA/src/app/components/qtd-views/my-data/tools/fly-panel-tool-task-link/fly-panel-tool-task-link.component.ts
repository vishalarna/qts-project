import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output, OnDestroy, AfterViewInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Link_Tool_Options } from 'src/app/_DtoModels/Tool/Link_Tool_Options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-tool-task-link',
  templateUrl: './fly-panel-tool-task-link.component.html',
  styleUrls: ['./fly-panel-tool-task-link.component.scss']
})
export class FlyPanelToolTaskLinkComponent implements OnInit, OnDestroy, AfterViewInit {
  filterTaskString: string = "";
  linkTasks: boolean = true;
  addTask: boolean = false;
  showActive: boolean = true;
  subscription = new SubSink();
  @Output() closed = new EventEmitter<any>();
  linkedIds: any[] = [];
  srcList: TaskTree[] = [];
  @Input() alreadyLinked: any[];
  @Input() id: any;

  treeControl = new NestedTreeControl<TaskTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TaskTree>();
  originalSource = new MatTreeNestedDataSource<TaskTree>();
  hasChild = (_: number, node: TaskTree) =>
    !!node.children && node.children.length > 0;

  taskCheckListSelection = new SelectionModel<TaskTree>(true);
  showLinkTaskLoader: boolean = false;

  constructor(
    public flyinService:FlyInPanelService,
    private dutyAreaService: DutyAreaService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private route: ActivatedRoute,
    private toolService: ToolsService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyTasksTreeData();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
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
    await this.dutyAreaService
      .getWithSubdutyAreas()
      .then((res) => {

        this.makeTaskTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

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
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
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
  filtered(node: any) {
    return node.description.includes(this.filterTaskString);
  }

  filterData() {
    if (this.filterTaskString.length > 0) {
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
      this.filterTaskString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    } else {
      this.dataSource.data = this.originalSource.data;
    }

    // this.dataSource.data = this.originalSource.data.filter((data: TaskTree) => {
    //   return data.description.toLowerCase().includes(this.filterTaskString.toLowerCase());
    // })
    //
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

  itemToggle(checked: boolean, node: TaskTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (node.selected) {
        this.linkedIds.push(node.id);
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if(index > -1){
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
      node.parent.selected = descendants.every((child) => (child.checkbox && child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.checkbox && child.selected));

      this.checkAllParents(node.parent);
    }
  }

  refreshTreeData() {
    this.linkedIds = [];
    this.taskCheckListSelection = new SelectionModel<TaskTree>(true);
    this.readyTasksTreeData();
  }

  linkTasksToTools() {
    var options = new Link_Tool_Options();
    options.toolIds = [];
    options.toolIds.push(this.id);
    options.linkedIds = [];
    options.linkedIds = this.linkedIds;

    this.toolService.LinkTasks(options).then(async (res: any) => {
      
/*       this.refresh.emit();
 */      //this.refreshLinkedTasks();
      this.alert.successToast(await this.transformTitle('Task') + "s Linked To " + await this.labelPipe.transform('Tool') + "s");
      this.closed.emit('fp-rr-task-link-closed');
    });
  }
}

class TaskTree {
  id: any;
  description: string;
  children?: TaskTree[];
  checkbox?: boolean;
  active?: boolean;
  selected?: boolean;
  checked?:boolean;
  indeterminate?: boolean;
  parent?: TaskTree;
}

