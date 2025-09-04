import {SelectionModel} from '@angular/cdk/collections';
import {TemplatePortal} from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  PipeTransform,
  ViewChild,
  ViewContainerRef
} from '@angular/core';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {MatLegacyPaginator as MatPaginator} from '@angular/material/legacy-paginator';
import {MatSort, Sort, SortDirection} from '@angular/material/sort';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {ActivatedRoute} from '@angular/router';
import {Position_Task_LinkOptions} from 'src/app/_DtoModels/Position_Task_Link/Position_Task_Link';
import {TaskWithCountR5R6Options} from 'src/app/_DtoModels/Task/TaskWithCountR5R6Options';
import {PositionsService} from 'src/app/_Services/QTD/positions.service';
import {DataBroadcastService} from 'src/app/_Shared/services/DataBroadcast.service';
import {FlyInPanelService} from 'src/app/_Shared/services/flyInPanel.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';
import {MatSortModule} from '@angular/material/sort';
import {SubSink} from 'subsink';
import {TaskSortPipePipe} from 'src/app/_Pipes/task-sort-pipe.pipe';
import {PositionTaskService} from 'src/app/_Services/QTD/Position_Task/api.positiontask.service';
import {UpdateMarkAsR6Model} from 'src/app/_DtoModels/Position_Task/UpdateMarkAsR6Model';
import {LinkR5UpdateTasksModel} from 'src/app/_DtoModels/Position_Task/LinkR5UpdateTasksModel';
import { UpdateUnmarkAsR6Model } from 'src/app/_DtoModels/Position_Task/UpdateUnmarkAsR6Model';
import { Position_HistoryCreateOptions } from 'src/app/_DtoModels/PositionHistory/PositionHistoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

export interface PeriodicElement {
  name: string;
  position: any;
  // options: string;
  weight: number;
  symbol: any;

}

@Component({
  selector: 'app-position-tasks',
  templateUrl: './position-tasks.component.html',
  styleUrls: ['./position-tasks.component.scss']
})
export class PositionTasksComponent implements OnInit, AfterViewInit, OnDestroy {
  displayedColumns: string[] = ['select', 'position', 'name', 'weight', 'symbol'];
  dataSource = new MatTableDataSource<any>();
  OriginaldataSource = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  sortCol: 'number' | 'description' | 'tg' | 'links' = 'number';
  sortOrder: 'asc' | 'desc' = 'asc';
  selectedTasks: any[] = [];
  taskToUnlink: any = null;

  constructor(public flypanelSrvc: FlyInPanelService,
              private vcf: ViewContainerRef,
              public dialog: MatDialog,
              private posService: PositionsService,
              private route: ActivatedRoute,
              private alert: SweetAlertService,
              private dataBroadcastService: DataBroadcastService,
              public taskSortPipe: TaskSortPipePipe,
              private posTaskService: PositionTaskService,
              private labelPipe: LabelReplacementPipe
  ) {
  }

  taskgroup: string[] = [
    "Group A",
    "Group B",
    "Group C",
    "None",
  ];
  unlinkDescription = '';
  unlinkR6Description = '';
  r5TasksToUnlink: any[] = [];
  linkedIds: any[] = [];
  subscription = new SubSink();
  id = '';
  srcList: any[] = [];
  unlinkIds: any[] = [];
  taskId: any[] = [];
  posTaskIdToUnmarkR6: string;
  posTaskIdToUnlinkR5Tasks: string;
  @Input() posTitle: string;
  @Input() isActive: any;
  displayColumns: string[] = ['id', 'number', 'description', 'trainingGroupLinkCount', 'impactedReliability', 'linkageCount', 'isRR', "action"];
  @Output() positionDeleteCheck = new EventEmitter<any>();
  title = '';
  taskIdToShow = '';
  isLoading: boolean = false;
  selectedR6ImpactedReason: string;
  selectedR6ImpactedEffectiveDate: Date;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    // To Get ID from route parameter
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
      this.getTaskLinkages();
    });

    // To refresh tasks when we link from fly panel.
    this.subscription.sink =
      this.dataBroadcastService.updatePosTaskLink.subscribe((res: any) => {
        this.getTaskLinkages();
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  someMethod(value: any, element: any) {


    element.symbol = value;
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following '+ await this.transformTitle('Task') + 's \n\n';
    this.taskId = [];
    if (id) {
      this.taskId.push(id);
      this.unlinkDescription +=
        this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach(async (d, i) => {
        this.taskId.push(d);
        this.unlinkDescription += await this.transformTitle('Task') +
          this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
    }
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Position') + this.posTitle + '.';

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {

    });
  }

  async unlinkItemModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink '+ await this.transformTitle('Task');
    this.taskId = [];
    if (id) {
      this.taskId.push(id);
      this.unlinkDescription +=' '+
        this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description;
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.taskId.push(d);
        this.unlinkDescription +=
          this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
    }
    this.unlinkDescription += ' from ' +  await this.transformTitle('Position') +' '+ this.posTitle + '. ';

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {

    });
  }

  async unmarkR6Tasks(templateRef: any, taskId?: string, positionTaskId?: string) {
    this.posTaskIdToUnmarkR6 = positionTaskId;
    this.unlinkR6Description = 'You are selecting the following' + await this.transformTitle('Task')  +'s to unmark as R6 Impact:\n\n';
    this.unlinkR6Description +=
      this.srcList.find((x) => x.id == taskId).number + ' - ' + this.srcList.find((x) => x.id == taskId).description + '\n';
    this.unlinkR6Description += '\nfrom '+ await this.transformTitle('Position') + this.posTitle + '. ';
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {
    });
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

  removeFromLinked() {

    this.linkedIds = this.linkedIds.filter((id: any) => {
      return !this.taskId.includes(id);
    });

  }

  async getTaskLinkages() {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    this.isLoading = true;
    await this.posService
      .getLinkedTasks(this.id)
      .then((res: TaskWithCountR5R6Options[]) => {
        res.forEach((data: TaskWithCountR5R6Options) => {
          this.linkedIds.push(data.id);
          if (data.trainingGroupLinkCount == 0) {
            tempSrc.push({
              number: data.number,
              description: data.description,
              linkageCount: data.linkCount,
              trainingGroupLinkCount: 'None',
              id: data.id,
              active: data.active,
              isRR: data.isRR ?? false,
              isR5Impacted: data.isR5Impacted,
              isR6Impacted: data.isR6Impacted,
              r6ImpactedReason: data.r6ImpactedReason,
              r6ImpactedEffectiveDate: data.r6ImpactedEffectiveDate,
              positionTaskId: data.positionTaskId,
              r5ImpactedTasks: data.r5ImpactedTasks
            });
          } else {
            tempSrc.push({
              number: data.number,
              description: data.description,
              linkageCount: data.linkCount,
              trainingGroupLinkCount: data.trainingGroupLinkCount,
              id: data.id,
              active: data.active,
              isRR: data.isRR ?? false,
              isR5Impacted: data.isR5Impacted,
              isR6Impacted: data.isR6Impacted,
              r6ImpactedReason: data.r6ImpactedReason,
              r6ImpactedEffectiveDate: data.r6ImpactedEffectiveDate,
              positionTaskId: data.positionTaskId,
              r5ImpactedTasks: data.r5ImpactedTasks
            });
          }
        })
        ;
        this.srcList = tempSrc;
        this.OriginaldataSource.data = Object.assign([], tempSrc);
        var originalData = Object.assign([], this.OriginaldataSource.data);
        this.dataSource = Object.assign(new MatTableDataSource(), this.taskSortPipe.transform(originalData, 'asc', 'number'));
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Fetching linked' + await this.transformTitle('Task') +'s');
      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openPositionLinkedViewFlyPanel(templateRef: any, data: any) {

    this.title = data.description;
    this.taskIdToShow = data.id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openTrainingGroupLinkedViewFlyPanel(templateRef: any, data: any) {

    this.title = data.letter + data.number + ' ' + data.description;
    this.taskIdToShow = data.id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshLinkData() {
    this.selection.clear();
    this.unlinkIds = [];
    this.getTaskLinkages();
    this.positionDeleteCheck.emit(true);
  }

  async getData(e: any) {
    if (this.taskId.length > 0) {
      var options = new Position_Task_LinkOptions();
      options.positionId = this.id;
      options.taskIds = this.taskId;
      var data = JSON.parse(e);
      options.changeEffectiveDate = data['effectiveDate'];
      options.changeNotes = data['reason'];
      this.isLoading = true;
      await this.posService
        .UnlinkTasks(this.id, options)
        .then(async (res: any) => {
          this.selection.clear();
          this.unlinkIds = [];
          this.removeFromLinked();
          this.getTaskLinkages();
          this.alert.successToast(
            'Successfully Unlinked '+ await this.transformTitle('Task') + '(s) from '+  await this.transformTitle('Position')
          );
        }).finally(() => {
          this.isLoading = false;
        })
    } 
  }

  sortDataTest() {
    var data = Object.assign([], this.OriginaldataSource.data);
    if (this.sort.direction === "") {
      this.dataSource = Object.assign(new MatTableDataSource(), this.OriginaldataSource);
    } else {
      this.dataSource = Object.assign(new MatTableDataSource(), this.taskSortPipe.transform(data, this.sort.direction, this.sort.active));
    }
  }

  handleTaskLinkages(templateRef: any, id?: string) {
    this.selectedTasks = [];
    if (id) {
      this.selectedTasks.push(this.srcList.find((x) => x.id == id));
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.selectedTasks.push(this.srcList.find((x) => x.id == d));
      });
    }
    this.openFlypanel(templateRef);
  }

  handleR5TaskUnlink(templateRef: any, id: string) {
    this.taskToUnlink = this.srcList.find((x) => x.id == id);
    this.openFlypanel(templateRef);
  }

  handleR6Information(templateRef: any, id: string){
    let selectedR6ImpactedTask = this.srcList.find((x) => x.id == id);
    this.selectedR6ImpactedReason = selectedR6ImpactedTask.r6ImpactedReason;
    this.selectedR6ImpactedEffectiveDate = selectedR6ImpactedTask.r6ImpactedEffectiveDate;
    this.openFlypanel(templateRef);
  }

  async getDataR6Unmark(e: any) {
    let response = JSON.parse(e);
    let effectiveDate = response.effectiveDate;
    let reason = response.reason;
    let options = new UpdateUnmarkAsR6Model();
    options.position_HistoryCreateOptions = new Position_HistoryCreateOptions();
    options.position_HistoryCreateOptions.positionId = this.id;
    options.position_HistoryCreateOptions.effectiveDate = effectiveDate;
    options.position_HistoryCreateOptions.changeNotes = reason;

    this.isLoading = true;
    await this.posTaskService.updateUnmarkAsR6Async(this.posTaskIdToUnmarkR6, options)
      .then(async (res: any) => {
        this.selection.clear();
        this.unlinkIds = [];
        this.removeFromLinked();
        this.getTaskLinkages();
        this.alert.successToast(
          "Successfully unmarked " +  await this.transformTitle('Position') + "s " + await this.transformTitle('Task') + " as R6 Impact"
        );
      }).finally(() => {
        this.refreshLinkData();
        this.isLoading = false;
      })
  }

  unlinkR5Tasks(templateRef: any, positionTaskId?: string) {
    this.posTaskIdToUnlinkR5Tasks = positionTaskId;
    this.r5TasksToUnlink = this.srcList.filter(r => r.positionTaskId == positionTaskId)[0]?.r5ImpactedTasks;
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {
    });
  }

  async getDataR5Unlink(e: any) {
    let response = JSON.parse(e);
    let effectiveDate = response.effectiveDate;
    let reason = response.reason;
    var options = new LinkR5UpdateTasksModel();
    options.unlinkAll = true;
    options.link_TaskIds = [];
    options.position_HistoryCreateOptions = new Position_HistoryCreateOptions();
    options.position_HistoryCreateOptions.positionId = this.id;
    options.position_HistoryCreateOptions.effectiveDate = effectiveDate;
    options.position_HistoryCreateOptions.changeNotes = reason;

    this.isLoading = true;
    await this.posTaskService
      .updateR5TasksAsync(this.posTaskIdToUnlinkR5Tasks, options)
      .then(async (res: any) => {
        this.selection.clear();
        this.unlinkIds = [];
        this.removeFromLinked();
        this.getTaskLinkages();
        this.alert.successToast(
          "Successfully unlinked R5 Impact " + await this.transformTitle('Task') + "(s) from " +  await this.transformTitle('Position') + "s "+ await this.transformTitle('Task')
        );
      })
      .finally(() => {
        this.isLoading = false;
      })
  }
}
