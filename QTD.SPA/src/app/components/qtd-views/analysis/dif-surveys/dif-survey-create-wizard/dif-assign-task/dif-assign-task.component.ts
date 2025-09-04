import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { DIFSurveyTaskVM } from '@models/DIFSurvey/DIFSurveyTaskVM';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { ApiDifSurveyTaskService } from 'src/app/_Services/QTD/DifSurveyTask/api.difsurvey-task.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-dif-assign-task',
  templateUrl: './dif-assign-task.component.html',
  styleUrls: ['./dif-assign-task.component.scss']
})
export class DifAssignTaskComponent implements OnInit {
  @Input() inputDifSurveyVM: DIFSurveyVM;
  dataSource: MatTableDataSource<DIFSurveyTaskVM>;
  displayedColumns:string[] = ["taskNumber","taskDescription","action"]
  unlinkTaskId:string='';
  unlinkDescription:string;
  @ViewChild('sortDataSource') set sortDataSource(sorting: MatSort) {
    if (sorting) this.dataSource.sort = sorting;
  }

  constructor(
    public dialog: MatDialog,
    public flyPanelService:FlyInPanelService,
    public difSurveyTaskService :ApiDifSurveyTaskService,
    private alert: SweetAlertService,
    private _taskPipe: TaskSortPipePipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<DIFSurveyTaskVM>(this.inputDifSurveyVM?.tasks);
    this.dataSource.data = this.dataSource.data.sort((a,b)=> a.taskNumber.localeCompare(b.taskNumber, undefined, { numeric: true }));
}


  unlinkItemsModal(templateRef: any, row?:DIFSurveyTaskVM) {
    this.unlinkDescription = `Are you sure you want to unlink Task <b>${row.taskNumber} - ${row.taskDescription}</b> ?`;
    this.unlinkTaskId = row.id
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  openAddTaskFlyPanel(templateRef: TemplateRef<any>){
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }
  updateTaskList(event:DIFSurveyTaskVM[]){
    this.inputDifSurveyVM.tasks=event;
    this.dataSource = new MatTableDataSource<DIFSurveyTaskVM>(this.inputDifSurveyVM?.tasks);
    this.dataSource.data = this.dataSource.data.sort((a,b)=> a.taskNumber.localeCompare(b.taskNumber, undefined, { numeric: true }));
    this.flyPanelService.close();
  }
  async unlinkTaskAsync(){
    await this.difSurveyTaskService.unlinkTasksAsync(this.unlinkTaskId).then(res=>{
      if(res?.status == 200){
        this.inputDifSurveyVM.tasks = this.inputDifSurveyVM.tasks.filter(x=>x.id != this.unlinkTaskId);
        this.dataSource = new MatTableDataSource<DIFSurveyTaskVM>(this.inputDifSurveyVM?.tasks);
        this.dataSource.data = this.dataSource.data.sort((a,b)=> a.taskNumber.localeCompare(b.taskNumber, undefined, { numeric: true }));
        this.alert.successToast("Task Successfully Removed");
      }
    })
  }

  sortDataSourceData(sort : Sort){
    this.dataSource = this._taskPipe.transform(
      this.dataSource.data,
      sort.direction,
      sort.active
      );  
  }
}
