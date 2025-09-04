import { SelectionModel } from '@angular/cdk/collections';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Route, Router } from '@angular/router';
import saveAs from 'file-saver';
import { asBlob } from 'html-docx-js-typescript';
import jsPDF from 'jspdf';
import { GetTaskWithAllLinkData } from 'src/app/_DtoModels/Task/GetTaskWithAllLinkData';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskMetaLinkVM } from 'src/app/_DtoModels/Task/TaskMetaLinkVM';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { TaskStatsCount } from 'src/app/_DtoModels/Task/TaskStatsCount';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { TaskCollaborateModalComponent } from '../task-collaborate-modal/task-collaborate-modal.component';
import { TaskRequalVM } from 'src/app/_DtoModels/Task/TaskRequalVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.scss'],
})
export class TaskDetailComponent implements OnInit, OnDestroy, AfterViewInit {
  displayColumns: string[] = ['order', 'id', 'number', 'description', 'isRR'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  spinner = false;
  @ViewChild(MatSort) sort : MatSort/*  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator,{static:false}) metaPaginator!:MatPaginator;

  @ViewChild('metaTaskTbl') table: MatTable<any>;
  isInvalidDragEvent: boolean = false;
  linkedMetaTaskIds: any[] = [];
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: TaskMetaLinkVM[] = [];
  unlinkId: any;
  isActive = true;
  dialogTitle = '';
  dialogDesc = '';
  task: Task;
  subscription = new SubSink();
  brodcastSubscription = new SubSink();
  taskId = '';
  editView = false;
  number: any;
  makeCopy: boolean;
  stats: TaskStatsCount = new TaskStatsCount();
  hasLinks = 0;
  lastModified: { name; dt_stamp };
  isReordered: boolean = false;
  dutyAreaDisable:boolean;
  subDutyAreaDisable:boolean;
  checkChange:boolean=true;
  dataSourceCheck:boolean=false;
  showMeta = false;
  canNotMakeTaskInactive = false;
  requalInfo!:TaskRequalVM;

  regex = /<img (.*?)>/;

  editLoader = false;
  allTaskData:GetTaskWithAllLinkData = new GetTaskWithAllLinkData();

  @ViewChild('pdf') pdf: HTMLElement;

  constructor(
    public dialog: MatDialog,
    private taskService: TasksService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    public dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private route: Router,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      this.showMeta = false;
      this.taskId = String(res.id).split('-')[0];
      this.number = String(res.id).split('-')[1].replace('_', ' ');
      this.getTaskData().then(async (_) => {
        this.canNotMakeTaskInactive = await this.taskService.canMakeInactive(this.taskId);
        if (this.task.isMeta) {
          this.getLinkedTask();
        }
        this.getStats();
        this.getlastModifiedBy();
      }).finally(()=>{
        this.showMeta = true;
      });
    });

    this.brodcastSubscription.sink =
      this.dataBroadcastService.refreshTaskStats.subscribe((res) => {
        this.getStats();
      });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  async getLinkedTask() {
    let tempSrc: any[] = [];
    this.linkedMetaTaskIds = [];
    this.isReordered = false;
    await this.taskService
      .getLinkedMetaTask(this.taskId)
      .then((res) => {
        this.srcList = res;

        for (const t of res) {
          tempSrc.push({
            id: t.id,
            number: t.number,
            description: t.description,
            isRR: t.isRR,
            active:t.active
          });

          this.linkedMetaTaskIds.push(t.id);
        }
        this.hasLinks = tempSrc.length;
        this.DataSource = new MatTableDataSource(tempSrc);
        this.dataBroadcastService.refreshMeta.next(null);
        this.DataSource.paginator = this.metaPaginator;
        this.getStats();
      })
      .catch(() => {
        this.DataSource = new MatTableDataSource();
      });
  }

  ngAfterViewInit(): void {}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async changeView(){
    this.editView = true;
    await this.taskService.GetAllTaskData(this.taskId).then((res:GetTaskWithAllLinkData)=>{
      this.allTaskData = res;
      this.editLoader = false;
    }).finally(()=>{
    })
  }

  async getTaskData() {
    this.task = await this.taskService.get(this.taskId);

    this.dutyAreaDisable = this.task.subdutyArea.dutyArea.active;
    this.subDutyAreaDisable = this.task.subdutyArea.active;
    this.isActive = this.task.active;

    this.requalInfo = await this.taskService.getRequalInfo(this.taskId);
  }

  // After Delete dialog is closed data will arrive here
  getDeleteData(e: any) {
    let data = JSON.parse(e);
    var options: TaskOptions = new TaskOptions();
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    options.taskIds.push(this.task.id);
    options.actionType = 'delete';

    this.taskService.delete(options).then(async (res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(await this.transformTitle('Task') +` is deleted`);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.route.navigate(['my-data/tasks/overview']);
    });
  }

  // After active/Inactive dialog is closed data will arrive here
  toggleActive(e: any) {
    let data = JSON.parse(e);
    var options: TaskOptions = new TaskOptions();
    options.actionType = this.isActive ? 'inactive' : 'active';
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    options.taskIds.push(this.task.id);
    this.taskService.delete(options).then(async (res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(await this.transformTitle('Task') +` is ` + options.actionType);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    });
  }

  async openActiveDialog(templateRef: any) {

    if(this.task.isMeta){
      this.dialogTitle = 'Make Meta ' + await this.transformTitle('Task')  +  (!this.isActive ? 'Active' : 'Inactive');
    }
    else{
      this.dialogTitle = 'Make ' + await this.transformTitle('Task') + (!this.isActive ? 'Active' : 'Inactive');
    }
    this.dialogDesc = `You are selecting to make ` + await this.transformTitle('Task') +` "${this.number} - ${
      this.task.description
    }" ${!this.isActive ? 'Active' : 'Inactive'}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async openDeleteDialog(templateRef: any) {
    this.dialogTitle = 'Delete ' + await this.transformTitle('Task');

    this.dialogDesc = `You are selecting to delete ` + await this.transformTitle('Task') +` ${this.number} - ${this.task.description}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openEditOrCopy(templateRef: any, copy: boolean,name:string) {
    if(name === 'change'){

    }
    this.makeCopy = copy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openflyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async getStats() {
    this.task.isMeta ? this.getMetaStats():this.getNonMetaStats();
  }

  async getNonMetaStats(){
    await this.taskService.getLinkedStats(this.taskId).then((res) => {
      this.stats = res;
      this.hasLinks = Object.values(this.stats).reduce((a,b) => a+b,0);

    });
  }

  async getMetaStats(){
    await this.taskService.getLinkedMetaStats(this.taskId).then((res) => {
      this.stats = res;
      this.hasLinks = Object.values(this.stats).reduce((a, b) => a + b, 0);
    });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink\n\n';
    if (id) {
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.number + ' - ' + this.srcList.find((x) => x.id == id)?.description;
      this.unlinkId = id;
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.number + ' - ' + this.srcList.find((x) => x.id == d)?.description + '\n';
      });
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription +='\n' + 'from Meta ' + await this.transformTitle('Task')  + this.task.description;

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

  async getData(e: any) {
    this.spinner = true;
    let linkedIds: any[] = [];
    if (this.unlinkId) linkedIds.push(this.unlinkId);
    else linkedIds = this.unlinkIds;
    let data = JSON.parse(e);
    let options: TaskOptions = {
      changeNotes: data['reason'],
      effectiveDate: data['effectiveDate'],
      taskIds: linkedIds,
    };

    await this.taskService
      .UnlinkTaskToMetaTask(this.taskId, options)
      .then(async (res) => {
        this.alert.successToast(await this.transformTitle('Task') + '(s) unlinked successfully');
        this.unlinkIds = [];
        this.unlinkId = undefined;
        this.selection.clear();
        this.getLinkedTask();
        this.getStats();
        this.dataBroadcastService.refreshMeta.next(null);
      }).finally(()=>{
        this.spinner = false;
      });
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  collaborate() {
    const dialog_ref = this.dialog.open(TaskCollaborateModalComponent, {
      width: '60%',
      data: this.taskId,
    });
    dialog_ref.disableClose = true;
    dialog_ref.afterClosed().subscribe({
      next: (data) => {

      },
    });
  }

  async getlastModifiedBy() {
    await this.taskService.getTaskHistory(this.taskId).then((res) => {
      let data = res[0];

      this.lastModified = {
        name: data?.createdBy ?? "NO CREATOR",
        dt_stamp: data?.createdDate ?? Date.now(),
      };

    });
  }

  dropTable(event: any) {
    const prevIndex = this.DataSource.data.findIndex(
      (d) => d === event.item.data
    );
    moveItemInArray(this.DataSource.data, prevIndex, event.currentIndex);
    this.DataSource = new MatTableDataSource(this.DataSource.data);

    this.isReordered = true;
  }

  async linkTasksToMetaTask() {
    let linkedIds: any[] = [];
    for (const item of this.DataSource.data) {
      linkedIds.push(item.id);
    }
    let option: TaskOptions = {
      taskIds: linkedIds,
    };
    await this.taskService
      .LinkTaskToMetaTask(this.taskId, option)
      .then(async (res) => {
        this.alert.successToast(await this.transformTitle('Task') + 's successfully reordered');
        this.getLinkedTask();
      });
  }

  async refreshTask(){
    this.flyPanelService.close();
    this.getTaskData().then((_) => {
      if (this.task.isMeta) {
        this.getLinkedTask();
      }
      this.getStats();
      this.getlastModifiedBy();
    });;
    this.getStats();
    this.dataBroadcastService.refreshMeta.next(null);
    this.dataBroadcastService.navigateOnChange.next(null);
  }

  /// This function will be used to convert html to pdf
  async downloadPdf() {
    let doc = new jsPDF('landscape', 'pt', 'a4');
    doc.setFontSize(10);
    doc.text(`${this.number} ${this.task.description}`, 20, 20);
    doc.html(this.pdf['nativeElement'], {
      autoPaging: 'text',
      width: 100,
      margin: [12, 8, 15, 8],
      html2canvas: {
        scale: 0.9
      },
    }).then((res: any) => {
      doc.save('test.pdf');
    })
  }

  async downloadDocs() {

    const opt = {
      margin: {
        top: 100
      },
      orientation: 'landscape' as const // type error: because typescript automatically widen this type to 'string' but not 'Orient' - 'string literal type'
    }
    var doc = await asBlob(String(this.pdf['nativeElement']['innerHTML']), opt) as Blob;
    saveAs(doc, "test.docx");
  }
}
