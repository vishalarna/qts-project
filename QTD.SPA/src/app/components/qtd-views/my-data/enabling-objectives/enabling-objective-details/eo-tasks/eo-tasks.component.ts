import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewContainerRef, AfterViewInit, OnDestroy, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-tasks',
  templateUrl: './eo-tasks.component.html',
  styleUrls: ['./eo-tasks.component.scss']
})
export class EoTasksComponent implements OnInit, AfterViewInit, OnDestroy {
  spinner = false;
  DataSource: MatTableDataSource<any>;
  OriginalDataSource = new MatTableDataSource<any>();
  @Input() isActive = true;
  @Input() isMeta = false;
  @Input() EOIdsForMeta: any[] = [];
  @Input() regTitle:any;

  unlinkIds: any[] = [];
  selection = new SelectionModel<any>(true, []);
  srcList: TaskWithCountOptions[] = [];
  displayColumns = ['id', 'number', 'description', 'linkCount'];

  subscription = new SubSink();
  eoId = "";
  alreadyLinked: any[] = [];
  eoIds: any[] = [];
  unlinkDescription = "";
  selectedNumber = "";
  selectedId = "";
  selectedTitle = "";

  // @ViewChild(MatSort) sort : MatSort
  @ViewChild(MatPaginator, {static:false}) paginator:MatPaginator;

  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private eoService: EnablingObjectivesService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private taskSortPipe : TaskSortPipePipe,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      this.eoId = String(res.id).split('-')[1];
      
      this.isMeta = await this.eoService.checkisMeta(this.eoId);
      this.refreshData();
    });

    this.subscription.sink = this.dataBroadcastService.refreshMeta.subscribe((_) => {
      
      this.refreshData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    
  }

  sortData(sort: Sort) {
    this.DataSource.data = this.taskSortPipe.transform(this.OriginalDataSource.data,sort.direction,sort.active).data;
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

  openLinkFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async readyTaskLinkData() {
    this.alreadyLinked = [];
    if (this.isMeta) {
      this.srcList = [];
      this.srcList = await this.eoService.getLinkedTasksToMetaEO(this.eoId);
      this.srcList.forEach((data) => {
        this.alreadyLinked.push(data.id);
      });

      this.DataSource = new MatTableDataSource(this.srcList);
    }
    else {
      this.srcList = await this.eoService.getLinkedTasks(this.eoId);
      this.alreadyLinked = [];
      this.srcList.forEach((data) => {
        this.alreadyLinked.push(data.id);
      });
      this.DataSource = new MatTableDataSource(this.srcList);
    }
    this.OriginalDataSource = Object.assign(this.DataSource);
    this.DataSource = this.taskSortPipe.transform(this.OriginalDataSource.data,'asc','number');
    setTimeout(()=>{
      this.DataSource.paginator = this.paginator;
    },1)
    
  }

  refreshData() {
    this.srcList = [];
    this.unlinkIds = [];
    this.selection.clear();
    this.readyTaskLinkData();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 
  async unlinkModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following' + await this.transformTitle('Task') +'s : \n';
    this.eoIds = [];
    if (id) {
      this.eoIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.number + ' - '  + this.srcList.find((x) => x.id == id)?.description + ' \n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.eoIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.number + ' - ' + this.srcList.find((x) => x.id == d)?.description + ' \n';
      });
    }
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Enabling Objective') + 's ' + this.regTitle ;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {
      
    });
  }

  async getData(e: any) {
    
    if (this.eoIds.length > 0) {
      this.spinner = true;
      var options = new EO_LinkOptions();
      options.eoId = this.eoId;
      options.taskIds = this.eoIds;
      var data = JSON.parse(e);
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      await this.eoService.unlinktasks(options).then(async (_) => {
        this.alert.successToast(await this.transformTitle('Task') +"s Unlinked from EO");
        this.refreshData();
        this.dataBroadcastService.refreshStats.next(null);
      }).finally(() => {
        this.spinner = false;
      })
    }
  }

  openLinkedFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

}
