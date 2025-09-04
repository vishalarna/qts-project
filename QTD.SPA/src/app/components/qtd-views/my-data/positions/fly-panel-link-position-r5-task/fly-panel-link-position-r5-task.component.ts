import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Position } from '@models/Position/Position';
import { Position_HistoryCreateOptions } from 'src/app/_DtoModels/PositionHistory/PositionHistoryCreateOptions';
import { DeleteR5TaskModel } from 'src/app/_DtoModels/Position_Task/DeleteR5TaskModel';
import { LinkR5UpdateTasksModel } from 'src/app/_DtoModels/Position_Task/LinkR5UpdateTasksModel';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { PositionTaskService } from 'src/app/_Services/QTD/Position_Task/api.positiontask.service';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import * as lodash from 'lodash';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-link-position-r5-task',
  templateUrl: './fly-panel-link-position-r5-task.component.html',
  styleUrls: ['./fly-panel-link-position-r5-task.component.scss'],
})
export class FlyPanelLinkPositionR5TaskComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  @Input() selectedTasks: any[] = [];
  selectedTaskData = new MatTableDataSource<any>();
  filterTaskString: string ="";
  linkTasks: boolean = true;
  addTask: boolean = false;
  showActive: boolean = true;
  isFlyPanelOpen: Boolean = false;
  linkR5UpdateTasksModel: LinkR5UpdateTasksModel;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() unlinkAll = new EventEmitter<string>();
  linkedIds: any[] = [];
  displayColumns: string[] = ['number', 'description'];
  impactedDisplayColumns: string[] = ['number', 'description', 'action'];
  treeControl = new NestedTreeControl<TaskTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<TaskTree>();
  originalSource = new MatTreeNestedDataSource<TaskTree>();
  hasChild = (_: number, node: TaskTree) =>
    !!node.children && node.children.length > 0;

  taskCheckListSelection = new SelectionModel<TaskTree>(true);
  showLinkTaskLoader: boolean = false;
  posId = '';
  subscription = new SubSink();
  isLoading = false;
  isTaskLoading = false;
  isTaskAvailable = false;
  showOnlySelected = false;
  modalHeader: string = '';
  modalReason: string = '';
  modalDescription: string = '';

  impactedTaskData = new MatTableDataSource<any>();
  alreadyLinkedIds: number[] = [];
  unlinkR5Description = '';
  r5TaskToUnlink: any;
  posTaskIdToUnlinkR5Tasks: any;
  dutyAreaId: string ;
  subDutyAreaId: string ;
  dutyAreaList: DutyAreaTreeVM[];
  dutyAreaListCopy: TaskTree[];
  tasks: string[];
  position:Position = new Position();
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private dutyAreaService: DutyAreaService,
    private posService: PositionsService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private posTaskService: PositionTaskService,
    public taskSortPipe: TaskSortPipePipe,
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {
    this.initializeR5UpdateModel();
    this.selectedTaskData = new MatTableDataSource(this.selectedTasks);
    if (this.selectedTasks.length == 1) {
      this.impactedTaskData = new MatTableDataSource(
        this.selectedTasks[0].r5ImpactedTasks
      );
      this.selectedTasks[0].r5ImpactedTasks.map((impactedTask) => {
        this.alreadyLinkedIds.push(impactedTask.impactedTaskId);
      });
    }
  }

  initializeR5UpdateModel() {
    this.linkR5UpdateTasksModel = new LinkR5UpdateTasksModel();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.posId = res.id;
      this.readyTasksTreeData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  } 

  async readyTasksTreeData() {
    this.isTaskLoading = true;
    await this.dutyAreaService
      .getMinimizedDataForTree()
      .then((res) => {
        this.dutyAreaList = res as DutyAreaTreeVM[];
        this.originalSource.data = this.makeTaskTreeDataSource(this.dutyAreaList);
        this.dataSource.data = this.originalSource.data;
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
        });
        this.treeControl.dataNodes = this.dataSource.data;
        this.isTaskLoading = false;
      })
      .finally(() => {
        this.isTaskLoading = false;
      });
  }
  handleDutyAreaId(dutyAreaId: any): void {
    this.dutyAreaId = dutyAreaId;
  }

  handleSubDutyAreaId(subDutyAreaId: any): void {
    this.subDutyAreaId = subDutyAreaId;
  }

  handleSelectedPosition(position: any): void {
    this.position=position;
    this.tasks = this.extractUniqueTaskIds(position);
  }

  extractUniqueTaskIds(position: any): string[] {
    const uniqueTaskIdsSet = new Set<string>();
    if (position && position.position_Tasks) {
      position.position_Tasks.forEach((task) => {
        if (task && task.taskId) {
          uniqueTaskIdsSet.add(task.taskId);
        }
      });
    }
    return Array.from(uniqueTaskIdsSet);
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(listData:DutyAreaTreeVM[]) {
    
    var treeData = [];
    if (listData.length > 0) {
      this.isTaskAvailable = true;
    }

    listData.forEach((da, i) => {
      let dutyAreaNode = {
        description: `${da.letter} ${da.number} - ${da.title}`,
        id: da.id,
        children: [],
        checkbox: true,
        selected: false,
      };
      
      da.subdutyAreas.forEach((sda, j) => {
        let subDutyAreaNode = {
          description: `${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id: sda.id,
          children: [],
          checkbox: true,
          selected: false,
        };
        sda.tasks.forEach((task) => {
          if (task.active && task.isReliability) {
            subDutyAreaNode.children.push({
              description: `${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id: task.id,
              checkbox: !this.alreadyLinkedIds.includes(task.id),
              selected: false,
              isTask: true,
            });
          }
        });

        if (subDutyAreaNode.children.length > 0) {
          dutyAreaNode.children.push(subDutyAreaNode);
        }
      });
      if (dutyAreaNode.children.length > 0) {
        treeData.push(dutyAreaNode);
      }
    });
    this.isTaskLoading = false;
    return treeData;
  }

  filterTaskTreeData()
  {
    this.dutyAreaListCopy = lodash.cloneDeep(this.originalSource.data);
    if (this.position?.id !=null) {
      this.dutyAreaListCopy = this.dutyAreaListCopy.map((node) => {
        const dutyArea = { ...node };
        if (dutyArea.children) {
          dutyArea.children = dutyArea.children.map((sub) => {
            const subDutyArea = { ...sub };
            if (subDutyArea.children) {
              subDutyArea.children = subDutyArea.children.filter((task) =>
                this.tasks.includes(task.id)
              );
            }
            return subDutyArea;
          });
          dutyArea.children=dutyArea.children.filter(x=>x.children.length>0);
       }
        return dutyArea;
      }).filter(x=>x.children.length>0);
    }
    if (this.dutyAreaId != null) {
      this.dutyAreaListCopy = this.dutyAreaListCopy.filter(
        (item) => item.id == this.dutyAreaId
      );
      if (this.subDutyAreaId != null && this.dutyAreaListCopy.length>0) {
        this.dutyAreaListCopy[0].children =[...
          this.dutyAreaListCopy[0].children.filter(
            (item) => item.id == this.subDutyAreaId
            )
          ]
            
      }
    }
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
    if(this.filterTaskString.length >0 ){

      this.dutyAreaListCopy= [
        ...this.dutyAreaListCopy.map((element) => {
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
  }
  this.dutyAreaListCopy.forEach(x=>x.children.forEach(y=>y.children.forEach(z=>z.selected= this.isTaskChecked(z))))
  this.dataSource.data= this.dutyAreaListCopy;
  
  Object.keys(this.dataSource.data).forEach((key: any) => {
    this.setParent(this.dataSource.data[key], undefined);
  });
  this.treeControl.dataNodes = this.dataSource.data;
  this.dataSource.data.forEach((node) => {
    node.children?.forEach((ila) => {
      this.checkAllParents(ila);
    });
  });
    this.filterTaskString?.length ?? 0 > 0
      ? this.treeControl.expandAll()
      : this.treeControl.collapseAll();
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
  isTaskChecked(node:TaskTree){
    let index = this.linkedIds.findIndex(x=>x == node.id);
    return index != -1;
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
              }),
            };
          }),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSource.data = temparr;
  }

  refreshLinkedTasks() {
    this.alreadyLinkedIds = [];
    this.taskCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinkedIds.push(id);
    });
    this.refreshTasksTree();
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filterTaskString, 'i').test(node.description) === false;
  }

  public hideParentNode(node: any) {
    return this.treeControl
      .getDescendants(node)
      .filter((node) => node.children == null || node.children.length === 0)
      .every((node) => this.hideLeafNode(node));
  }

  clearFilter() {
    this.filterTaskString = '';
    this.filterTaskTreeData();
  }

  async getDataAsync(e: any) {
    let response = JSON.parse(e);
    let effectiveDate = response.effectiveDate;
    let reason = response.reason;
    this.linkR5UpdateTasksModel.unlinkAll = false;
    this.linkR5UpdateTasksModel.position_HistoryCreateOptions =
      new Position_HistoryCreateOptions();
    this.linkR5UpdateTasksModel.position_HistoryCreateOptions.positionId =
      this.posId;
    this.linkR5UpdateTasksModel.position_HistoryCreateOptions.effectiveDate =
      effectiveDate;
    this.linkR5UpdateTasksModel.position_HistoryCreateOptions.changeNotes =
      reason;

    this.isLoading = true;
    await Promise.all(
      this.selectedTasks.map(async (task) => {
        await this.posTaskService.updateR5TasksAsync(
          task.positionTaskId,
          this.linkR5UpdateTasksModel
        );
      })
    ).finally(() => {
      this.isLoading = false;
    });

    // If all updates are successful, show the success toast
    this.refresh.emit();
    this.refreshLinkedTasks();
    this.alert.successToast('Successfully marked '+ await this.transformTitle('Task')  +'(s) as R5 Impacted');
    this.closed.emit('fp-pos-r5-task-link-closed');
  }

  async openR5MarkDialogue(templateRef: any) {
    this.linkR5UpdateTasksModel.link_TaskIds = [];
    this.modalHeader = 'Link R-R ' + await this.transformTitle('Task') + 's';
    this.modalReason =
      'Please provide Effective Date and Reason for this change';
    this.modalDescription =
      'You are selecting to mark the following ' + await this.transformTitle('Task') +'s as R5 impacted\n\n';
    if (this.selectedTasks.length == 1) {
      this.impactedTaskData = new MatTableDataSource(
        this.selectedTasks[0].r5ImpactedTasks
      );
      this.alreadyLinkedIds.map((id) => {
        this.linkedIds.push(id);
      });
    }
    this.linkedIds.forEach((d) => {
      this.linkR5UpdateTasksModel.link_TaskIds.push(d);
    });
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  sortTable() {
    var data = this.impactedTaskData.data;
    if (this.sort.direction === '') {
      this.impactedTaskData = this.impactedTaskData;
    } else {
      this.impactedTaskData = this.taskSortPipe.transform(
        data,
        this.sort.direction,
        this.sort.active
      );
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async unlinkR5Task(r5ImpactedTask: any, templateRef: any) {
    this.r5TaskToUnlink = r5ImpactedTask;
    this.unlinkR5Description =
      'You are selecting to unlink the following R-R ' + await this.transformTitle('Task') +':\n\n';
    this.unlinkR5Description +=
      r5ImpactedTask.impactedTaskFullNumber +
      ' - ' +
      r5ImpactedTask.impactedTaskDescription;
    this.unlinkR5Description += '\n\nThis will remove the R5 impact linkage.';
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {});
  }
  async getDataR5Unlink(e: any) {
    let response = JSON.parse(e);
    let effectiveDate = response.effectiveDate;
    let reason = response.reason;
    let options = new DeleteR5TaskModel();
    options.position_HistoryCreateOptions = new Position_HistoryCreateOptions();
    options.position_HistoryCreateOptions.positionId = this.posId;
    options.position_HistoryCreateOptions.effectiveDate = effectiveDate;
    options.position_HistoryCreateOptions.changeNotes = reason;

    this.isLoading = true;
    await this.posTaskService
      .deleteR5TaskAsync(
        this.r5TaskToUnlink.positionTaskId,
        this.r5TaskToUnlink.id,
        options
      )
      .then(async (res: any) => {
        this.refresh.emit();
        this.linkedIds = this.alreadyLinkedIds.filter(
          (id) => id != this.r5TaskToUnlink.impactedTaskId
        );
        const index = this.impactedTaskData.data.findIndex(
          (item) => item.id === this.r5TaskToUnlink.id
        );
        if (index !== -1) {
          this.impactedTaskData.data.splice(index, 1);
          this.impactedTaskData.data = [...this.impactedTaskData.data]; // Assign a new data array
        }
        this.refreshLinkedTasks();
        this.alert.successToast(
          "Successfully unlinked R5 Impact " + await this.transformTitle('Task') +" from " + await this.transformTitle('Position') + " s " +await this.transformTitle('Task')
        );
      })
      .finally(() => {
        this.isLoading = false;
      });
  }
  unlinkR5Tasks(templateRef: any, positionTaskId?: string) {
    this.posTaskIdToUnlinkR5Tasks = positionTaskId;
    this.r5TaskToUnlink = this.selectedTasks.filter(
      (r) => r.positionTaskId == positionTaskId
    )[0]?.r5ImpactedTasks;
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {});
  }

  async getDataR5Unlinks(e: any) {
    let response = JSON.parse(e);
    let effectiveDate = response.effectiveDate;
    let reason = response.reason;
    var options = new LinkR5UpdateTasksModel();
    options.unlinkAll = true;
    options.link_TaskIds = [];
    options.position_HistoryCreateOptions = new Position_HistoryCreateOptions();
    options.position_HistoryCreateOptions.positionId = this.posId;
    options.position_HistoryCreateOptions.effectiveDate = effectiveDate;
    options.position_HistoryCreateOptions.changeNotes = reason;

    this.isLoading = true;
    await this.posTaskService
      .updateR5TasksAsync(this.posTaskIdToUnlinkR5Tasks, options)
      .then(async (res: any) => {
        this.refresh.emit();
        this.closed.emit();
        this.alert.successToast(
          "Successfully unlinked R5 Impact "+ await this.transformTitle('Task') +" (s) from "+ await this.transformTitle('Position') +"s " + await this.transformTitle('Task')
        );
      })
      .finally(() => {
        this.isLoading = false;
      });
  }
  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
  handleFlyPanelClicked(isClicked: boolean): void {
    this.openFlyPanelLink();
  }

  openFlyPanelLink() {
    this.isFlyPanelOpen = !this.isFlyPanelOpen;
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
