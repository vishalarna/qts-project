import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-regulatory-requirements',
  templateUrl: './task-regulatory-requirements.component.html',
  styleUrls: ['./task-regulatory-requirements.component.scss'],
})
export class TaskRegulatoryRequirementsComponent implements OnInit {
  @Input() taskTitle:any;
  @Input() isMeta:any;
  @Input() isEMPView = false;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: any[] = [];
  subscription = new SubSink();
  taskId = '';
  linkedIds: any = [];
  unlinkId: any;
  RRId: any;
  Title: any;
  RRNumber:any;
  showLinkRRLoader: boolean = false;
  @Input() isActive;
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
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
      this.taskId = String(res.id).split('-')[0];
      this.getLinkedTaskRR();
    });
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

  async getData(e: any) {
    this.showLinkRRLoader=true;
    let option: TaskOptions = new TaskOptions();
    option.regulatoryRequirementIds = [];
    if (this.unlinkId) {
      option.regulatoryRequirementIds?.push(this.unlinkId);
    } else option.regulatoryRequirementIds = this.unlinkIds;
    let data = JSON.parse(e);
    option.changeNotes = data['reason'];
    option.effectiveDate = data['effectiveDate'];

    await this.taskService.UnlinkRR(this.taskId, option).then(async (res) => {
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + 's unlinked successfully');
      this.selection.clear();
      this.unlinkIds = [];
      this.unlinkId = undefined;
      this.getLinkedTaskRR();
      this.dataBroadcastService.refreshTaskStats.next(null);
    }).finally(() => {
      this.showLinkRRLoader = false;
    })
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following' + await this.labelPipe.transform('Regulatory Requirement') + 's\n';

    if (id) {
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
      this.unlinkId = id;
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
    }
    this.unlinkDescription += '\n' + 'from ' + await this.transformTitle('Task') + this.taskTitle;
   // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getLinkedTaskRR() {
    if (!this.isMeta) {
      await this.taskService.getLinkedRRWithMetaEOCount(this.taskId).then((res) => {
        let tempArray: any[] = [];
        this.srcList = [];
        this.linkedIds = [];
        this.srcList = res;
        res.forEach((data) => {
          this.linkedIds.push(data.id);
          tempArray.push({
            id: data.id,
            number: data.number,
            description: data.description,
            linkageCount: data.linkCount,
            active: data.active,
          });
        });
  
        this.DataSource = new MatTableDataSource(tempArray);
      });

    }else{
      await this.taskService.getLinkedRRWithCount(this.taskId).then((res) => {
        let tempArray: any[] = [];
        this.srcList = [];
        this.linkedIds = [];
        this.srcList = res;
        res.forEach((data) => {
          this.linkedIds.push(data.id);
          tempArray.push({
            id: data.id,
            number: data.number,
            description: data.description,
            linkageCount: data.linkCount,
            active: data.active,
          });
        });
  
        this.DataSource = new MatTableDataSource(tempArray);
      });
    }
  }
  openFlyPanel(templateRef: any, rrId?: any) {
    if (rrId) {
      this.RRId = rrId.id;
      this.RRNumber = rrId.number;
      this.Title = this.srcList.find((x) => x.id == rrId.id)?.description ?? '';
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }
}
