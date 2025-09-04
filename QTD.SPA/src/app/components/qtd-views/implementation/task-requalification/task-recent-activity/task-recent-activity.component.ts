import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskQualificationEmpVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationEmpVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-recent-activity',
  templateUrl: './task-recent-activity.component.html',
  styleUrls: ['./task-recent-activity.component.scss']
})
export class TaskRecentActivityComponent implements OnInit {
  displayedColumns = ['empName', 'taskNumber', 'evaluatorName', 'dueDate', 'criteriaMet', 'status', 'requiredRequals', 'comments', 'actions'];
  dataSource = new MatTableDataSource<TaskQualificationEmpVM>();
  @ViewChild('paginator') paginator!:MatPaginator;
  @ViewChild(MatSort) sort !: MatSort;
  mode: 'add' | 'edit' = 'edit';

  selectedData!: any;
  spinner = false;

  recentQuals:TaskQualificationEmpVM[] = [];
  placeHolder = '/assets/img/ImageNotFound.jpg';

  description = "";
  header = "Delete Task Qualification";

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    public dialog : MatDialog,
    private router: Router,
    private route: ActivatedRoute,
    private taskReQualService: TaskRequalificationService,
    private alert : SweetAlertService,
    private dataBroadcastService : DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData() {
    this.spinner = true;
    this.recentQuals = await this.taskReQualService.getRecentTaskQuals();
    this.recentQuals = this.recentQuals.map((item)=>{
      var qualDate = ""
      if(item.qualificationDate == null){
        qualDate = null;
      }else{
        var updatedDate = new Date(item.qualificationDate + "Z");
        qualDate = new Date(updatedDate).toLocaleString();
      } 
      return{...item,qualificationDate:qualDate}
    })
    this.dataSource.data = this.recentQuals;
    this.spinner = false;
    setTimeout(()=>{
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    },1)
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  openFlyPanel(templateRef: any, row: TaskQualificationEmpVM) {
    this.selectedData = row;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async openDialog(templateRef: any, row: TaskQualificationEmpVM) {
    this.selectedData = row;
    this.description = `Are you sure you want to delete the ` + await this.transformTitle('Task') +` Qualification Record ${row.taskNumber} for ${row.empName}.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData($event:any){
    await this.taskReQualService.delete(this.selectedData.id).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Task') +" Qualification Deleted Successfully");
      this.readyData();
      this.refreshStats();
    }).finally(() => {

    })
  }

  refreshStats(){
    this.dataBroadcastService.refreshTQStats.next(null);
  }
}

export class taskRecentActivity {
  id?: any;
  empPicture?: any;
  empFirstName?: string;
  empLastName?: string;
  empEmail?: string;
  empId?: any;
  taskNumber?: string;
  qualificationDate?: any;
  evaluatorName?: string;
  dueDate?: any;
  criteriaMet?: boolean;
  reQualStatus?: any;
  comments?: string;
  totalRequired?:string;
}
