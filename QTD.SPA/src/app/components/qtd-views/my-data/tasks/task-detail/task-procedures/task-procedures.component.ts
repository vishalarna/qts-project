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
import { ProcedureWithLinkCount } from 'src/app/_DtoModels/Procedure/ProcedureWithLinkCount';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-procedures',
  templateUrl: './task-procedures.component.html',
  styleUrls: ['./task-procedures.component.scss'],
})
export class TaskProceduresComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  @Input() taskTitle:any;
  @Input() isMeta:any;
  @Input() isEMPView = false;
  displayColumns: string[] = ['id', 'number', 'description', 'linkCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: ProcedureWithLinkCount[] = [];
  procIds: any[] = [];
  taskId = '';
  subscription = new SubSink();
  linkedIds: any[] = [];
  procedureNumber:any;

  title = '';
  procId = '';
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
    private route: ActivatedRoute,
    private taskService: TasksService,
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.filter.length;
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
    var data = JSON.parse(e);
    var options = new TaskOptions();
    options.procedureIds = this.procIds;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.taskService.UnlinkProcedures(this.taskId, options).then(async (_) => {
      this.alert.successToast(
        'Successfully Unlinked selected ' + await this.transformTitle('Procedure') + 's from ' + await this.labelPipe.transform('Task')
      );
      this.refreshLinkData();
    });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {

    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Procedure') + 's\n';
    this.procIds = [];
    if (id) {
      this.procIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.number + ' - ' + this.srcList.find((x) => x.id == id)?.description + '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.procIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.number + ' - ' + this.srcList.find((x) => x.id == d)?.description + '\n';
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

  async getLinkedData() {
    this.linkedIds = [];
    if (!this.isMeta) {
      this.srcList = await this.taskService.getLinkedProceduresWithMetaTaskCount(this.taskId);
      this.srcList.forEach((data) => {
        this.linkedIds.push(data.id);
      });
      this.DataSource = new MatTableDataSource(this.srcList);
    }
    else{
      this.srcList = await this.taskService.getLinkedProcedureWithCount(
        this.taskId
      );
      this.srcList.forEach((link) => {
        this.linkedIds.push(link.id);
      });
      this.DataSource = new MatTableDataSource(this.srcList);
    }

  }

  openFlyPanel(templateRef: any,row:any) {
    this.procedureNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openLinkFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshLinkData() {
    this.selection.clear();
    this.procIds = [];
    this.unlinkIds = [];
    this.getLinkedData();
    this.dataBroadcastService.refreshTaskStats.next(null);
  }

}
