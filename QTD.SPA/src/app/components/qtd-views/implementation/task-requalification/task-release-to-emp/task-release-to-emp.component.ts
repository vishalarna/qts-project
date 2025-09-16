import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ReleasedTQAndSQUpdateOptions } from '@models/TaskQualification/TQAndSQReleasedUpdateOptions';
import { TQReleasedToEMPVM } from 'src/app/_DtoModels/TaskQualification/TQReleasedToEMPVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-release-to-emp',
  templateUrl: './task-release-to-emp.component.html',
  styleUrls: ['./task-release-to-emp.component.scss'],
})
export class TaskReleaseToEmpComponent implements OnInit {
  filterString = "";
  dataSource = new MatTableDataSource<TQReleasedToEMPVM>();
  originalData = new MatTableDataSource<TQReleasedToEMPVM>();
  displayedColumns = ['cb', 'empName', 'taskNumber', 'releaseDate', 'dueDate', 'evaluatorName', 'requiredTaskQuals', 'actions','empCommaSepName'];
  selection = new SelectionModel<TQReleasedToEMPVM>(true, []);
  description = "";
  selectedData!: TQReleasedToEMPVM;
  multiSelectData: TQReleasedToEMPVM[] = [];
  releasedTQ: TQReleasedToEMPVM[] = [];
  tqLoader = false;

  @ViewChild('paginator') paginator!: MatPaginator;
  //@ViewChild(MatSort) matSort!: MatSort;

  constructor(
    public dialog: MatDialog,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private tqService: TaskRequalificationService,
    private alert: SweetAlertService,
    private taskSortPipe:TaskSortPipePipe,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData() {
    this.tqLoader = true;
    this.selection.clear();
    this.releasedTQ = await this.tqService.GetTQReleasedToEMP();

    this.tqLoader = false;
    this.dataSource.data = Object.assign(this.releasedTQ);
    this.originalData.data = Object.assign(this.releasedTQ);
    this.sortChanged({active:'taskNumber',direction:'asc'});
    setTimeout(() => {
      this.dataSource.paginator = this.paginator;
      //this.dataSource.sort = this.matSort;
    },1)
  }

  clearFilter(){
    this.filterString = '';
    this.dataSource.filter = null;
  }

  checkChange(event: any, row: TQReleasedToEMPVM) {
    if (event.checked) {
      this.selection.select(row)
    }
    else {
      this.selection.deselect(row);
    }
  }
  
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => {
           this.selection.select(row);
        });
  }

  inputChange() {
  var filter = this.filterString.trim().toLowerCase();

  this.dataSource.data = this.originalData.data.filter((item) => {
    return (
      item.empCommaSepName?.trim().toLowerCase().includes(filter) ||
      item.evaluatorName?.trim().toLowerCase().includes(filter) ||
      item.taskNumber?.toString().toLowerCase().includes(filter)
    );
  });
}

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  recallTaskQual() {
    var options = new ReleasedTQAndSQUpdateOptions();
    options.action = "recall";
    options.tQIds.push(this.selectedData.id);

    this.tqService.updateReleasedTQAndSQ(options).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Task') +" Qualification Recalled From EMP");
      this.readyData();
    })
  }

  async openRecallDialog(templateRef: any, data: TQReleasedToEMPVM) {
    this.selectedData = data;
    this.description = `You are selecting to Recall ` + await this.transformTitle('Task') +` # ${this.selectedData.taskNumber} from the ` + await this.labelPipe.transform('Employee') + ` ${this.selectedData.empFName} ${this.selectedData.empLName} and ` + await this.transformTitle('Task') +` Evaluator's ${this.selectedData.evaluatorName.length > 0 ? this.selectedData.evaluatorName.substring(0, this.selectedData.evaluatorName.length - 2) : "N/A"}. This will remove the selected ` + await this.transformTitle('Task') +` from the applicable EMP dashboard.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openUpdateDateDialog(templateRef: any, data: TQReleasedToEMPVM) {
    this.selectedData = data;

    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async updateDate(e: any) {
    var data = JSON.parse(e);
    var options = new ReleasedTQAndSQUpdateOptions();
    options.releaseDate = data['releaseDate'];
    options.dueDate = data['dueDate'];
    options.action = "Date";
    options.tQIds.push(this.selectedData.id);

    await this.tqService.updateReleasedTQAndSQ(options).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Task') + " Qualification Release And Due Dates Updated");
      this.readyData();
    })
  }

  openUpdateReassignDialog(templateRef: any, data: TQReleasedToEMPVM) {
    this.selectedData = data;

    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async reassignEvaluators(e: any) {
    var data = JSON.parse(e);
    var options = new ReleasedTQAndSQUpdateOptions();
    options.releaseDate = data['releaseDate'];
    options.dueDate = data['dueDate'];
    options.evaluatorIds = data['evaluatorIds'];
    options.action = "Reassign";
    options.checkStarted = data['checkStarted'];
    options.removeSignOffs = this.selectedData.wasTQStarted;
    options.tQIds.push(this.selectedData.id);

    await this.tqService.updateReleasedTQAndSQ(options).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Task') +" Qualification Reassigned");
      this.readyData();
    })
  }

  recallTaskQualDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async performRecall() {
    var options = new ReleasedTQAndSQUpdateOptions();
    options.action = 'recall';
    options.tQIds = this.multiSelectData.map((data: TQReleasedToEMPVM) => { return data.id });
    await this.tqService.updateReleasedTQAndSQ(options).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Task') +" Qualifications successfully recalled");
      this.readyData();
    })
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  sortChanged(event:any){

    if(event.active === 'taskNumber' && event.direction !== ''){
      this.dataSource = this.taskSortPipe.transform(this.originalData.data,event.direction,'number');
    }
    else if(event.direction !== ''){
      this.dataSource = this.taskSortPipe.transform(this.originalData.data,event.direction,event.active);
    }
    else{
      this.dataSource.data = Object.assign(this.originalData.data);
    }

    this.dataSource.paginator = this.paginator;
    this.inputChange();
  }
}

export class ReleaseToEMPData {
  id?: any;
  empName?: string;
  taskNumber?: string;
  releaseDate?: any;
  dueDate?: any;
  evalName?: string;
  totalQuals?: any;
}
