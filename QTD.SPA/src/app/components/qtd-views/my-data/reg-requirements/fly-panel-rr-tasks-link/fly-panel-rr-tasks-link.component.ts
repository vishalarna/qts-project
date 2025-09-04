import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { RR_Task_LinkOptions } from 'src/app/_DtoModels/RR_Task_Link/RR_Task_LinkOptions';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-rr-tasks-link',
  templateUrl: './fly-panel-rr-tasks-link.component.html',
  styleUrls: ['./fly-panel-rr-tasks-link.component.scss'],
})
export class FlyPanelRRTasksLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterTaskString: string;
  linkTasks: boolean = true;
  addTask: boolean = false;
  showActive: boolean = true;
  rrId = "";
  subscription = new SubSink();
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[];
  showOnlySelected = false;
  showLoader: boolean = true;
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
    private rrService: RegulatoryRequirementService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyTasksTreeData();
  }

  ngAfterViewInit(): void {

    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.rrId = res.id;
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
    this.dataSource.data =  this.originalSource.data;
  }

  async readyTasksTreeData() {
    debugger
    this.showLoader=true;
    await this.dutyAreaService
      .getMinimizedDataForTree()
      .then((res: DutyAreaTreeVM[]) => {

        this.dutyAreas = res;
        this.makeTaskTreeDataSource(res);
      }).catch((err: any) => {
        console.error(err);
      })
      .finally(()=>{
    this.showLoader=false;

      });
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(dutyArea: DutyAreaTreeVM[]) {
    var treeData: TaskTree[] = [];

    dutyArea.forEach((da,i)=>{
      treeData.push({
        id:da.id,
        description:da.letter + da.number + ' - ' + da.title,
        children:[],
        checkbox:true,
        selected:false,
        indeterminate:false,
      });
      da.subdutyAreas.forEach((sda,j)=>{
        treeData[i].children.push({
          id:sda.id,
          children:[],
          description:da.letter + da.number + '.' + sda.subNumber + ' - ' + sda.title,
          checkbox:true,
          selected:false,
          indeterminate:false,
        });
        sda.tasks.forEach((task)=>{
          treeData[i].children[j].children.push({
            id:task.id,
            checkbox: !this.alreadyLinked.includes(task.id),
            children:[],
            selected:false,
            description:da.letter + da.number + '.' + sda.subNumber + '.' + task.number + ' - ' + task.description,
            active:task.active,
          })
        })
      })
    })
    // for (var data in dutyArea) {
    //   treeData[data] = {
    //     id: dutyArea[data]['id'],
    //     description: dutyArea[data]['number'] + ' - '+   dutyArea[data]['title'],
    //     children: dutyArea[data]['subdutyAreas'],
    //     checkbox: true,
    //     selected: false,
    //   };
    //   for (var data1 in treeData[data]['children']) {
    //     treeData[data]['children'][data1] = {
    //       id: dutyArea[data]['subdutyAreas'][data1]['id'],
    //       description:  dutyArea[data]['number'] + '.' + dutyArea[data]['subdutyAreas'][data1]['number'] + ' - ' + dutyArea[data]['subdutyAreas'][data1]['title'],
    //       children: dutyArea[data]['subdutyAreas'][data1]['tasks'],
    //       checkbox: true,
    //     };
    //     for (var data2 in treeData[data]['children'][data1]['children']) {
    //       treeData[data]['children'][data1]['children'][data2]['checkbox'] = !this.alreadyLinked.includes(treeData[data]['children'][data1]['children'][data2]['id']);
    //       treeData[data]['children'][data1]['children'][data2]['description'] = dutyArea[data]['number'] + '.' + dutyArea[data]['subdutyAreas'][data1]['number'] + '.' + treeData[data]['children'][data1]['children'][data2]['number'] + ' - ' + treeData[data]['children'][data1]['children'][data2]['description'];

    //     }
    //     childIndex++;
    //   }
    //   childIndex = 1;
    // }
    this.dataSource.data = Object.assign(treeData);
    this.originalSource.data = Object.assign(this.dataSource.data);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalSource.data[key], undefined);
    });

    this.filterActive(true);

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

  itemToggle(checked: boolean, node: TaskTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined && node.children.length > 0) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (node.selected && node.checkbox) {
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

  linkTasksToRR() {
    debugger;
    this.linkedIds= [...new Set(this.linkedIds)];
    var options = new RR_Task_LinkOptions();
    options.regRequirementId = this.rrId;
    options.taskIds = [];
    options.taskIds = this.linkedIds;

    this.rrService.LinkTasks(this.rrId, options).then(async (res: any) => {
      this.refresh.emit();
      //this.refreshLinkedTasks();
      this.alert.successToast(await this.transformTitle('Task') +"s Linked To " + await this.labelPipe.transform('Regulatory Requirement'));
      this.closed.emit('fp-rr-task-link-closed');
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

  refreshTasksTree(){
    this.taskCheckListSelection.clear();
    this.linkedIds = [];
    this.readyTasksTreeData();
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filterTaskString, 'i').test(node.description) === false;
  }

  public hideParentNode(node: any){
    return this.treeControl
        .getDescendants(node)
        .filter(node => node.children == null || node.children.length === 0)
        .every(node => this.hideLeafNode(node));
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
}
