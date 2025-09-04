import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { IDPVM } from 'src/app/_DtoModels/IDP/IDPVM';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { Schedule } from '../../../schedulingclasses/scheduling-classes-overview/scheduling-classes-overview.component';
import { DatePipe } from '@angular/common';
import { EmployeeEnrollOptions } from 'src/app/_DtoModels/ILA/EmployeeEnrollOptions';
import { IdpService } from 'src/app/_Services/QTD/idp.service';
import { IDPScheduleUpdateOption } from 'src/app/_DtoModels/IDP/IDPScheduleUpdateOption';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { MatSort } from '@angular/material/sort';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { TrainingStudentCreationOptions } from '@models/SchedulesClassses/training-creation-options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-fly-panel-enroll-employee',
  templateUrl: './fly-panel-enroll-employee.component.html',
  styleUrls: ['./fly-panel-enroll-employee.component.scss']
})
export class FlyPanelEnrollEmployeeComponent implements OnInit {

  @Input() selectedIDPvm: IDPVM;
  @Input() Employeeid: any;
  @Input() EmployeeName:string="";
  @Output() refreshIDPs = new EventEmitter<any>();

  selectedclassId:any;
  selectedclassiTem:Schedule;
  upComingClassesDataSource = new MatTableDataSource<Schedule>();
  pastClassesDataSource = new MatTableDataSource<Schedule>();
  isPastClass:boolean = false;
  @ViewChild('upcomingSort', { static: false }) upcomingSort: MatSort;
  @ViewChild('pastSort', { static: false }) pastSort: MatSort;
  constructor(public flyPanelSrvc: FlyInPanelService,
    private alert : SweetAlertService,
    private router: Router,
    public dialog: MatDialog,
    private ilaService: IlaService,
    private idpService: IdpService,
    public trService: TrainingService,
    private labelPipe:LabelReplacementPipe) { }
  displayedColumns: string[] = ['startDate','endDate','location','id']
  showSpinner:boolean=false;
  datePipe = new DatePipe('en-us');
  ilaName = "";
  dialogDesc = '';
  dialogTitle = '';
  utcNowDate=this.getNowUTC();

  getNowUTC() {
    const now = new Date();
    return new Date(now.getTime() + (now.getTimezoneOffset() * 60000));
  }
  startDate :any;

  ngOnInit(): void {
    this.utcNowDate=this.getNowUTC();
    this.getScheduleClasses();
    this.ilaName=this.selectedIDPvm.ilaTitle;
  }
  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }
  OpenPlannedDateModal (templateRef: any) {

    const dialogRef = this.dialog.open(templateRef, {
      width: '700px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async openDeleteDialog(templateRef: any) {
    this.dialogTitle=`Unenroll ` + await this.labelPipe.transform('Employee') ;
    this.dialogDesc = `Are you sure you want to unenroll?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getScheduleClasses(){
    this.showSpinner=true;
  this.ilaService
  .getScheduleClassesByILA(this.selectedIDPvm.ilaId,this.Employeeid,this.selectedIDPvm.id)
  .then((data)=>{
    this.upComingClassesDataSource.data = data.filter(item => new Date(item.startDate) > this.utcNowDate).sort((a, b) => {
    const dateA = new Date(a.startDate).getTime();
    const dateB = new Date(b.startDate).getTime();
    return dateA - dateB;
    });
    this.pastClassesDataSource.data = data.filter(item => new Date(item.startDate) <= this.utcNowDate && new Date(item.startDate).getFullYear() == this.utcNowDate.getUTCFullYear()).sort((a, b) => {
      const dateA = new Date(a.startDate).getTime();
      const dateB = new Date(b.startDate).getTime();
      return dateB - dateA;
    });
    setTimeout(() => {
      this.upComingClassesDataSource.sort = this.upcomingSort;
      this.pastClassesDataSource.sort = this.pastSort;
    }, 1);
  }).finally(()=>{
    this.showSpinner=false;
  })
 }
 async EnrollEmployee(plannedDate?:any){
  this.showSpinner=true;
  var data=new EmployeeEnrollOptions();
  data.empId=this.Employeeid;
  data.plannedDate=plannedDate;
  data.classScheduleId=this.selectedclassId;
  data.idpId=this.selectedIDPvm.id
  let result=await this.ilaService.EnrollEmployeeIDP(data);
  this.closeFlyPanel();
  this.alert.successToast("Enrolled Successfully");
  this.refreshIDPs.emit();

  this.showSpinner=false;

  //show success message in case of status 200
}

  async RedirectToSchedule(){
    const ila = await this.ilaService.getILAAsync(this.selectedIDPvm.ilaId);
    this.closeFlyPanel();
    const queryParams = {
      providerId: ila.providerId,
      ilaId: this.selectedIDPvm.ilaId,
      employeeId: this.Employeeid,
    };
    this.router.navigate(['/implementation/sc/addTraining'],{ queryParams });
  }
  async SavePlannedDate(e: any) {
    await this.EnrollEmployee(e);

  }

  async UpdateEnrolledDate(e: any) {
    this.showSpinner=true;
    var options=new IDPScheduleUpdateOption();
    options.endDate=e.endDate;
    options.startDate=e.startDate;
    options.classScheduleId=this.selectedclassiTem.classScheduleId;
    options.idpId=this.selectedIDPvm.id;
    await this.idpService.updateIDPDate(options);
    this.alert.successToast("Date Updated Successfully");
    this.refreshIDPs.emit();
    this.showSpinner=false;
    this.closeFlyPanel();
  }

 async UnEnrollEmployee(){
  this.showSpinner=true;
  var options = new TrainingStudentCreationOptions();
  options.employeeIds = [];
  options.employeeIds.push(this.Employeeid);
  let result=await this.trService.unLinkedEmployees(this.selectedclassiTem.classScheduleId,options);
  this.closeFlyPanel();
  this.alert.successToast("Unenrolled Successfully");
  this.refreshIDPs.emit();
  this.showSpinner=false; 
 }
}
