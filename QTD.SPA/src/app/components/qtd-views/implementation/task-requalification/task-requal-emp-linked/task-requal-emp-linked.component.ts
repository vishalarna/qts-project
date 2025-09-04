import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { TaskQualification } from 'src/app/_DtoModels/TaskQualification/TaskQualification';
import { TaskQualificationEmpVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationEmpVM';
import { TaskQualificationTabVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationTabVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-requal-emp-linked',
  templateUrl: './task-requal-emp-linked.component.html',
  styleUrls: ['./task-requal-emp-linked.component.scss']
})
export class TaskRequalEmpLinkedComponent implements OnInit, OnDestroy {
  taskwithNum!: TaskQualificationTabVM;
  empDataForTask: TaskQualificationEmpVM[] = [];
  dataSource = new MatTableDataSource<TaskQualificationEmpVM>();
  originalDataSource = new MatTableDataSource<TaskQualificationEmpVM>();
  displayedColumns = ['empName', 'empReleaseDate', 'evaluatorName', 'dueDate', 'criteriaMet', 'status', 'comments', 'actions'];
  displayedColumnWithoutEMP = ['empName', 'evaluatorName', 'dueDate', 'criteriaMet', 'status', 'comments', 'actions'];
  currDate = Date.now();
  selectedData!: TaskQualificationEmpVM;
  mode: 'add' | 'edit' = 'add';
  subscription = new SubSink();
  taskId = "";
  isLoading = false;
  placeHolder = '/assets/img/ImageNotFound.jpg';
  description = "";
  posId = "";
  statusFilter = "";

  @ViewChild(MatSort,{static:false}) sort!: MatSort;

  taskQualificationData!: TaskQualification;

  filterByEMP: 'Position' | 'Status' | 'All' = 'All';
  hasEMPPortal:boolean = false;

  constructor(
    private router: Router,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private route: ActivatedRoute,
    private taskReQualService: TaskRequalificationService,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private clientSettingService: ApiClientSettingsService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.taskId = res.id;
      this.isLoading = true;
      this.setTaskData();
      // this.readyData();
    })

    this.clientSettingService.GetCurrentLicenseAsync().then((data)=>{
      this.hasEMPPortal = data.hasEmp;
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async setTaskData() {
    this.taskwithNum = await this.taskReQualService.getTaskRequalWithNumber(this.taskId);
    this.readyEmpLinkedToTask();
  }

  async readyEmpLinkedToTask() {
    this.empDataForTask = await this.taskReQualService.getEmpLinkedToTaskRequal(this.taskId);
    this.empDataForTask = this.empDataForTask.map((item)=>{
      var qualDate = ""
      if(item.qualificationDate == null){
        qualDate = null;
      }else{
        var updatedDate = new Date(item.qualificationDate + "Z");
        qualDate = new Date(updatedDate).toLocaleString();
      } 
      return{...item,qualificationDate:qualDate}
    })
    this.dataSource.data = Object.assign(this.empDataForTask);
    
    this.filterData();
    this.isLoading = false;

    setTimeout(()=>{
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    },1);
  }

  filterData() {
    this.dataSource.data = Object.assign(this.empDataForTask.filter((emp) => {
      switch (this.filterByEMP) {
        case 'Position':
          return emp.posIds.includes(this.posId);
        case 'Status':
          return emp.status.trim().toLowerCase().includes(this.statusFilter.trim().toLowerCase());
      }
      return true;
    })
    )
  }

  goBack() {
    history.back();
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async openDeleteDialog(templateRef: any) {
    this.description = `You are selecting to Delete ` + await this.transformTitle('Task') +` Qualification Record ${this.selectedData.taskNumber} for ` + await this.labelPipe.transform('Employee') + ` ${this.selectedData.empName}.`

    this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData(event: any) {
    if (this.selectedData.id) {
      await this.taskReQualService.delete(this.selectedData.id).then(async (_) => {
        this.alert.successToast(await this.transformTitle('Task') +" Qualification Deleted Successfully");
        this.readyEmpLinkedToTask();
      }).finally(() => {

      })
    }
    else {
      this.alert.successToast(await this.transformTitle('Task') +" Qualification Deleted Successfully");
    }
  }

  refreshData(event: any) {
    
    this.readyEmpLinkedToTask();
    if (event.close) {
      this.flyPanelService.close()
    }
  }

  applyFilter(data: any) {
    
    switch (data.for) {
      case "Position":
        this.posId = data.data;
        break;
      case "Status":
        this.statusFilter = data.data;
        break;
    }
    this.filterData();
  }

  clearFilters() {
    this.posId = "";
    this.statusFilter = "";
    this.filterData();
  }
}


export class taskEmpLinkedData {
  id?: any;
  empPicture?: any;
  empName?: string;
  empEmail?: string;
  empReleaseDate?: any;
  empId?: any;
  qualificationDate?: any;
  evaluatorName?: string;
  dueDate?: any;
  criteriaMet?: boolean;
  reQualStatus?: any;
  comments?: string;
}
