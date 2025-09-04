import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnInit, Output, Input, EventEmitter, OnDestroy, AfterViewInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Procedure_Task_LinkOptions } from 'src/app/_DtoModels/Procedure_Task_Link/Procedure_Task_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-add-procedure-tasks-link',
  templateUrl: './fly-panel-procedure-tasks-link.component.html',
  styleUrls: ['./fly-panel-procedure-tasks-link.component.scss'],
})
export class FlyPanelProcedureTasksLinkComponent implements OnInit, OnDestroy, AfterViewInit {
  showLoader = true;
  filterTaskString: string;
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
    private dutyAreaService: DutyAreaService,
    private procService: ProceduresService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private route: ActivatedRoute,
    private labelPipe: LabelReplacementPipe
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

  clearFilter(){
    this.filterTaskString = '';
    this.dataSource.data = this.originalSource.data;
  }

  async readyTasksTreeData() {
    this.showLoader = true;
    await this.dutyAreaService
      .getWithSubdutyAreas()
      .then((res) => {
        
        this.makeTaskTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      }).finally(()=>{
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
    if (this.filterTaskString?.length > 0) {
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

  LinkToProcedure() {
    this.showLinkTaskLoader = true;
    var options = new Procedure_Task_LinkOptions();
    options.procedureId = this.id;
    options.taskIds = this.linkedIds;
    this.procService.LinkTasks(this.id, options).then(async (res: any) => {
      this.alert.successToast("Linked All Selected " + await this.transformTitle('Task') + "s");
      this.showLinkTaskLoader = false;
      this.dataBroadcastService.updateProcTaskLink.next(null);
    }).finally(() => {
      this.closed.emit('fp-proc-task-link-closed');
      this.showLinkTaskLoader = false;
    })
  }

  refreshTreeData() {
    this.linkedIds = [];
    this.taskCheckListSelection = new SelectionModel<TaskTree>(true);
    this.readyTasksTreeData();
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
