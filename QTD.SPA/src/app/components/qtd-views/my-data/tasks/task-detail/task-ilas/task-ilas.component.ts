import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-ilas',
  templateUrl: './task-ilas.component.html',
  styleUrls: ['./task-ilas.component.scss']
})
export class TaskIlasComponent implements OnInit, OnDestroy, AfterViewInit {
  displayColumns: string[] = ['id', 'number', 'description', 'linkCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: ILAWithCountOptions[] = [];
  @Input() taskTitle:any;
  @Input() isMeta:any;
  @Input() isEMPView = false;

  subscription = new SubSink();
  taskId = "";
  alreadyLinkedIds : any[] = [];
  ilaIds :any[] = [];
  spinner = false;
  title = "";
  ilaId = "";
  ilaNumber:any;
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
    private taskService : TasksService,
    private route : ActivatedRoute,
    private alert : SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.taskId = String(res.id).split('-')[0];
      this.refreshILAData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyILALinkedData(){
    this.alreadyLinkedIds = [];

    if (!this.isMeta) {
      this.srcList = await this.taskService.getLinkedILAWithMetaTaskCount(this.taskId);
      this.srcList.forEach((data) => {
        this.alreadyLinkedIds.push(data.id);
      });
      this.DataSource = new MatTableDataSource(this.srcList);
    }
    else{
      this.srcList = await this.taskService.getLinkedILAWithCount(this.taskId);
      this.srcList.forEach((ila)=>{
        this.alreadyLinkedIds.push(ila.id);
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
  
  getData(e: any) {
    this.spinner = true;
    var data = JSON.parse(e);
    var options = new TaskOptions();
    options.ilaIds = this.ilaIds;
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    this.taskService.unlinkILA(this.taskId,options).then(async (_)=>{
      this.alert.successToast("Unlinked Selected "+ await this.labelPipe.transform('ILA') + "s from " +  await this.transformTitle('Task'));
      this.refreshILAData();
    }).finally(()=>{
      this.spinner = false;
    })
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.labelPipe.transform('ILA') + 's\n';
    this.ilaIds = [];
    if (id) {
      this.ilaIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.number + ' - ' + this.srcList.find((x) => x.id == id)?.description  + '\n';
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.ilaIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.number + ' - ' + this.srcList.find((x) => x.id == d)?.description + '\n';
      });
    }
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Task')  + this.taskTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openFlyPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openLinkFlyPanel(templateRef:any,row:any){
    this.ilaNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshILAData(){
    this.selection.clear();
    this.ilaIds = [];
    this.unlinkIds = [];
    this.readyILALinkedData();
    this.dataBroadcastService.refreshTaskStats.next(null);
  }

}
