import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  Input,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-safety-hazards',
  templateUrl: './task-safety-hazards.component.html',
  styleUrls: ['./task-safety-hazards.component.scss'],
})
export class TaskSafetyHazardsComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  @Input() taskTitle:any;
  @Input() isMeta:any;
  @Input() isEMPView = false;
  displayColumns: string[] = ['id', 'number', 'title', 'linkCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: SafetyHazardWithLinkCount[] = [];

  title = '';
  id = '';
  shIds: any[] = [];
  SHNumber:any;

  subscription = new SubSink();
  taskId = '';
  alreadyLinkedIds: any[] = [];
  spinner = false;
  @Input() isActive;

  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private taskService: TasksService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router:Router,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      if(this.router.url.includes('task-suggestions')){
        this.taskId = String(res.id).split('-')[1].replace('§_', '').split('.')[0];;
       }
       else{
         this.taskId = String(res.id).split('-')[0];
        }
      this.refreshLinkData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyLinkData() {
    this.alreadyLinkedIds = [];
    if (!this.isMeta) {
      this.srcList = await this.taskService.getLinkedSHWithMetaTaskCount(this.taskId);
      this.srcList.forEach((data) => {
        this.alreadyLinkedIds.push(data.id);
      });
      this.DataSource = new MatTableDataSource(this.srcList);
    }
    else{
      this.srcList = await this.taskService.GetLinkedSHWithCount(this.taskId);
      this.srcList.forEach((data) => {
        this.alreadyLinkedIds.push(data.id);
      });

      this.DataSource = new MatTableDataSource(this.srcList);
    }

  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  getData(e: any) {
    debugger;
    this.spinner = true;
    var data = JSON.parse(e);
    var options = new TaskOptions();
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    options.safetyHazardIds = this.shIds;
    this.taskService
      .UnlinkSaftyHazards(this.taskId, options)
      .then(async (_) => {
        this.alert.successToast('Selected ' + await this.labelPipe.transform('Safety Hazard') + 's Unlinked from ' + await this.labelPipe.transform('Task'));
        this.refreshLinkData();
      })
      .finally(() => {
        this.spinner = false;
      });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = `You are selecting to unlink the following ${await this.labelPipe.transform("Safety Hazard")}s\n`;
    this.shIds = [];
    if (id) {
      this.shIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.number + ' - ' + this.srcList.find((x) => x.id == id)?.title + '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {


        this.shIds.push(d);

        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.number + ' - ' + this.srcList.find((x) => x.id == d)?.title + '\n';
      });

    }

    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Task') + this.taskTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openLinkFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openLinkedFlypanel(templateRef: any, row:any) {
    this.SHNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }


  refreshLinkData() {
    this.srcList = [];
    this.unlinkIds = [];
    this.selection.clear();
    this.readyLinkData();
    this.dataBroadcastService.refreshTaskStats.next(null);
  }
}
