import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-positions',
  templateUrl: './task-positions.component.html',
  styleUrls: ['./task-positions.component.scss'],
})
export class TaskPositionsComponent implements OnInit {
  @Input() taskTitle:any;
  @Input() isMeta:any;
  @Input() isEMPView = false;
  displayColumns: string[] = ['id','abbreviation', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  unlinkDescription = '';
  srcList: Position[] = [];
  subscription = new SubSink();
  taskId = '';
  linkedPosIds: any[] = [];
  unlinkId: any;
  posId: any;
  Title: string;
  posAbbrevation:any;
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  @Input() isActive;

  constructor(
    public dialog: MatDialog,
    private taskService: TasksService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    public dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private router: Router,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      if(this.router.url.includes('task-suggestions')){
        this.taskId = String(res.id).split('-')[1].replace('§_', '').split('.')[0];;
       }
       else{
         this.taskId = String(res.id).split('-')[0];
        }
      this.getTaskLinkedPositions();
    });

  /*   this.dataBroadcastService.updateProcTaskLink.subscribe(() => {
      this.getTaskLinkedPositions();
    }); */
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
    let option: TaskOptions = new TaskOptions();
    option.positionIds = [];
    if (this.unlinkId) {
      option.positionIds?.push(this.unlinkId);
    } else option.positionIds = this.unlinkIds;
    let data = JSON.parse(e);
    option.changeNotes = data['reason'];
    option.effectiveDate = data['effectiveDate'];

    await this.taskService.Unlinkpositions(this.taskId, option).then(async (res) => {
      this.alert.successToast(await this.transformTitle('Position')+ 's unlinked successfully');
      this.selection.clear();
      this.unlinkIds = [];
      this.unlinkId = undefined;
      this.getTaskLinkedPositions();
      this.dataBroadcastService.refreshTaskStats.next(null);
    });
  }

 async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following '+ await this.transformTitle('Position') + 's\n';
    if (id) {
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.positionAbbreviation + ' - ' + this.srcList.find((x) => x.id == id)?.positionTitle + '\n';
      this.unlinkId = id;
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.positionAbbreviation + ' - ' + this.srcList.find((x) => x.id == d)?.positionTitle + '\n';
      });
    }
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Task') + this.taskTitle;
    //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getTaskLinkedPositions() {
    this.taskService.getLinkedpositions(this.taskId).then((res) => {
      this.linkedPosIds = [];
      this.srcList = [];
      this.unlinkIds = [];
      let tempArray: any[] = [];
      res.forEach((p) => {
        this.linkedPosIds.push(p.position.id);
        this.srcList.push(p.position);
        tempArray.push({
          id: p.position.id,
          description: p.position.positionTitle,
          abbreviation: p.position.positionAbbreviation != null || ''
            ? p.position.positionAbbreviation : '',
          /*  p.position.positionAbbreviation != null || ''
             ? p.position.positionAbbreviation +
               ' - ' +
               p.position.positionTitle
             : p.position.positionTitle, */
          linkageCount: p.count,
          active: p.position.active,
        });
      });
      this.DataSource = new MatTableDataSource(tempArray);
    });
  }

  openFlypanel(templateRef: any, posId?: any) {
    if (posId) {
      this.posId = posId.id;
      this.posAbbrevation = posId.abbreviation;
      this.Title = this.srcList.find((x) => x.id == posId.id)?.positionTitle ?? '';
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
