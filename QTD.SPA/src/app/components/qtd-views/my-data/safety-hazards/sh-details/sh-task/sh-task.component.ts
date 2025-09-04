import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { SafetyHazardWithLinkCount } from 'src/app/_DtoModels/SaftyHazard/SafetyHazardWithLinkCount';
import { SH_Task_LinkOptions } from 'src/app/_DtoModels/SH_Task_Link/SH_Task_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-task',
  templateUrl: './sh-task.component.html',
  styleUrls: ['./sh-task.component.scss']
})
export class ShTaskComponent implements OnInit, AfterViewInit {
  @Input() isActive: boolean = true;
  @Input() shTitle: any;
  @Output() taskDeleteCheck = new EventEmitter<any>();
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  OriginalDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  subscription = new SubSink();
  unlinkIds: any[] = [];
  addTask: boolean = false;
  unlinkDescription = '';
  shId = "";
  srcList: any[] = [];
  alreadyLinked: any[] = [];
  //@ViewChild(MatSort) sort : MatSort
  /*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) paginator:MatPaginator;
  taskId: any[] = [];
  taskTitle = "";
  selectedTaskId = "";
  taskNumber: any;

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private taskSortPipe:TaskSortPipePipe,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.getTaskLinkages();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  sortData(sort: Sort) {
    this.DataSource.data = this.taskSortPipe.transform(this.OriginalDataSource.data,sort.direction,sort.active).data;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
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

  getTaskLinkages() {
    let tempSrc: any[] = [];
    this.alreadyLinked = [];
    this.shService.getLinkedTasks(this.shId).then((res: SafetyHazardWithLinkCount[]) => {
      res.forEach((data: SafetyHazardWithLinkCount) => {
        tempSrc.push({ number: data.number, description: data.title, linkageCount: data.linkCount, id: data.id, active: data.active });
      });
      this.alreadyLinked = tempSrc.map((data: any) => {
        return data.id;
      });
      if (res.length > 0) {
        this.taskDeleteCheck.emit(true);
      }
      else if (res.length == 0) {
        this.taskDeleteCheck.emit(false);
      }
      Object.assign(this.srcList, tempSrc);
      this.DataSource = new MatTableDataSource(tempSrc);
      this.OriginalDataSource = new MatTableDataSource(tempSrc);
      this.DataSource.data = this.taskSortPipe.transform(this.OriginalDataSource.data,'asc','number').data;
      //this.DataSource.sort = this.tblSort;
      setTimeout(()=>{
        this.DataSource.paginator = this.paginator;
      },1);
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching Linked " + await this.transformTitle('Task') + "s Data ");
    });
  }

  refreshData() {
    this.getTaskLinkages();
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  getData(e: any) {
    
    if (this.taskId.length > 0) {
      var data = JSON.parse(e);
      var options = new SH_Task_LinkOptions();
      options.saftyHazardId = this.shId;
      options.taskIds = this.taskId;
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      this.shService.unlinkTasks(this.shId, options).then(async (res: any) => {
        this.alert.successToast(await this.transformTitle('Task') + "s Unlinked From " + await this.labelPipe.transform('Regulatory Requirement'));
        this.selection.clear();
        this.unlinkIds = [];
        this.srcList = [];
        this.getTaskLinkages();
      })
    }
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Task') + '\n';
    this.taskId = [];
    if (id) {
      this.taskId.push(id);
      this.unlinkIds = this.unlinkIds.filter((myId: any) => {
        return myId !== id;
      });
      this.unlinkDescription +=
        this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + "\n";
    } else {
      this.unlinkIds.forEach((d: any, idx: number) => {
        
        this.taskId.push(d);
        this.unlinkDescription +=
          this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + "\n";
      });
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from '+await this.labelPipe.transform("Safety Hazard")+' ' + this.shTitle;
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

  openLinkedToSHFlyPanel(templateRef: any, row: any) {
    
    this.selectedTaskId = row.id;
    this.taskTitle = row.description;
    this.taskNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
}
