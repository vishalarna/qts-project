import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, Input, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeesLinkedToSchedule } from 'src/app/_DtoModels/SchedulesClassses/EmployeesLinkedToSchedule';
import { ClassRoasterUpdateOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { RosterOverviewVM } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterOverviewVM';
import { TrainingStudentCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { RostersService } from 'src/app/_Services/QTD/rosters.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-rosters-overview',
  templateUrl: './rosters-overview.component.html',
  styleUrls: ['./rosters-overview.component.scss']
})
export class RostersOverviewComponent implements OnInit, OnDestroy {
  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: false,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'right',
      },
    },
  };
  passCount:number=0;
  failCount:number=0;
  notCompCount:number=0;
  @ViewChild(MatSort) sort : MatSort;

  public pieChartLabels = [[this.passCount+'% Pass'], [this.failCount+'% Fail'],this.notCompCount+'% Not Completed'];
  public pieChartLegend = true;
  public pieChartPlugins = [];
  validGrades = ["P","F","W","O"];
  public pieChartData: ChartData<'pie'> = {
    labels: this.pieChartLabels,
    datasets: [
      {
        label: 'Title label',
        data: [0, 0, 0],
        backgroundColor: ['rgb(220 245 223)', 'rgb(253 194 194)', '#fff'],
        borderColor: ['rgb(220 245 223)', 'rgb(253 194 194)', '#828886'],
        hoverBackgroundColor: [
          'rgb(220 245 223)',
          'rgb(253 194 194)',
          '#828886',
        ],
        hoverBorderColor: [
          'rgba(0, 160, 0, 1)',
          'rgba(240, 160, 0, 1)',
          'rgba(220, 0, 0, 1)',
        ],
      },
    ],
  };

  // Bar
  public barChartLegend = false;
  public barChartPlugins = [];
  public barChartType = 'horizontalBar';
  public barChartData: ChartConfiguration<'bar'>['data'] = {
    labels: ['90-100%', '80-89%', '70-79%', '0-69%', 'Incomplete'],
    datasets: [
      {
        data: [0, 0, 0, 0, 0],
        label: 'Series A',
        backgroundColor: [
          '#5c9b31',
          '#7dac5d',
          '#c1e8a7',
          '#fdc2c2',
          '#7a807e',
        ],
      },
    ],
  };

  public barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: false,

    indexAxis: 'y',
    scales: {
      x: { display: false },
      y: {
        display: true,
      },
    },
  };

  hideChart = true;

  dataSourceOverview = new MatTableDataSource<any>();

  displayOverviewColumns: string[] = [
    'id',
    'employeeName',
    'preTestStatus',
    'cbtStatus',
    'testStatus',
    'retakeCount',
    'evaluationCompletedDate',
    'score',
    'grade',
    'gradeNotes',
    'actions',
  ];

  empLinkedData:RosterOverviewVM[] = [];
  @Input() classId = "";
  @Input() endDate:any = "";
  @Input() startDate:any = "";
  @Input() trainingTitle = "";
  @Input() IsSelfPaced:any = false;
  @Input() ilaId:any = false;
  editType: 'score' | 'grade' | 'none' | 'completionDate' | 'notes' = 'none';
  editId:any = "";
  selectedEMPId:any = "";
  selection = new SelectionModel<any>(true, []);

  tblGradeError = true;

  valueToSave!:any;
  spinner = false;

  gradeError = true;
  subscription = new SubSink();
  description = "";
  datePipe = new DatePipe('en-us');
  isSaving = false;
  unlinkIds: any[] = [];

  currrentEmployee:any
  overviewEmployeeDetails:any[] = [];
  constructor(
    public flyPanelSrvc:FlyInPanelService,
    public trService : TrainingService,
    private alert : SweetAlertService,
    private dataBroadcastService : DataBroadcastService,
    public dialog : MatDialog,
    private vcf:ViewContainerRef,
    private fb: UntypedFormBuilder,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshRosterData.subscribe((_)=>{
      this.readyData();
    })
    this.readyData();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  doUnEnrollFor:'table'|'button';

  removeEMPDialog(templateRef: any, employee: any, name:any) {
    if(name === 'unenroll'){
      this.doUnEnrollFor = 'table';
      this.selectedEMPId = employee.classEmployeeId;
      this.description = `You are selecting to remove ${employee.employeeName} from training ${this.trainingTitle}, ${this.startDate} to ${this.endDate}. This action will permanently erase any student records for this Training.`
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }else{
      this.doUnEnrollFor = 'button';
      var emps = this.empLinkedData.filter(x => this.unlinkIds.includes(x.classEmployeeId));
      var description:string = "";
      emps.forEach((x)=>{
        description = description + x.employeeName + " ,";
      })
      description = description.slice(0,-1);
      this.description = `You are selecting to remove ${description} from training ${this.trainingTitle}, ${this.startDate} to ${this.endDate}. This action will permanently erase any student records for this Training.`
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }

  }

  openBulkUpdateDialog(templateRef:any){
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSourceOverview.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.classEmployeeId);
    });
  }

  async readyData(){
    this.selection.clear();
    this.unlinkIds = [];
    this.startDate = this.datePipe.transform(this.startDate, 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(this.endDate, 'yyyy-MM-dd');
    this.editId = '';
    this.editType = 'none';
    this.valueToSave = null;
    this.empLinkedData = await this.trService.getOverViewData(this.classId);
    this.overviewEmployeeDetails = this.empLinkedData.map(item=>{return {employeeId:item.classEmployeeId,employeeName:item.employeeName,classScheduleEmployeeId:item.classScheduleEmployeeId}});
    this.empLinkedData = this.empLinkedData.map((item=>{
      var compDate = ""
      if(item.evaluationCompletedDate == null){
        compDate = null;
      }else{
        var updatedDate = new Date(item.evaluationCompletedDate + "Z");
        compDate = new Date(updatedDate).toLocaleString();
      } 
      return{...item,evaluationCompletedDate:compDate}
    }))
    this.dataSourceOverview.data = this.empLinkedData;   
    this.dataSourceOverview.sort = this.sort;
   
    this.setChartsData();
  }


  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSourceOverview.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.classEmployeeId);
    });
  }

  openFlyPanelEditScore(templateRef: any, row: any) {
    const portal = new TemplatePortal(templateRef, row);
    this.flyPanelSrvc.open(portal);
  }

  setChartsData() {
    this.hideChart = true;
    var pass = this.empLinkedData.filter((rost: RosterOverviewVM) => {
      return (rost.grade && rost.grade.trim().toLowerCase() === 'p');
    }).length;
    this.passCount=pass;
    var fail = this.empLinkedData.filter((rost: RosterOverviewVM) => {
      return (rost.grade && rost.grade.trim().toLowerCase() === 'f');
    }).length;
    this.failCount=fail;
    var notCompleted = this.empLinkedData.filter((rost: RosterOverviewVM) => {
      return rost.score === null && rost.grade === null;
    }).length;
    this.notCompCount=notCompleted;
    var percentages = this.empLinkedData.map((rost) => {
      return (rost.score === null ? null : rost.score)
    })
    var barData: number[] = [0, 0, 0, 0, 0];
    percentages.forEach((data,i) => {
      if (data === null) {
        barData[4] = barData[4] + 1
      }
      else if (data >= 90 && data <= 100) {
        barData[0] = barData[0] + 1;
      }
      else if (data >= 80 && data < 90) {
        barData[1] = barData[1] + 1
      }
      else if (data >= 70 && data < 80) {
        barData[2] = barData[2] + 1
      }
      else if (data >= 0 && data < 70) {
        barData[3] = barData[3] + 1
      }
      else {
        barData[4] = barData[4] + 1
      }
    });
    this.pieChartData['datasets'][0]['data'] = Object.assign([pass, fail, notCompleted]);
    this.barChartData['datasets'][0]['data'] = barData;
    this.passCount = (pass/this.empLinkedData.length)*100;
    this.failCount = (fail/this.empLinkedData.length)*100;
    this.notCompCount = (notCompleted/this.empLinkedData.length)*100;
    this.pieChartData['labels'] = [[Math.round(this.passCount)+'% Pass'], [Math.round(this.failCount)+'% Fail'],Math.round(this.notCompCount)+'% Not Completed'];
    setTimeout(() => {
      this.hideChart = false;
    }, 1)
  }

   updateValue(empId:any){
    this.isSaving = true;
    var options = new ClassRoasterUpdateOptions();
    switch(this.editType){
      case 'grade':
        options.grade = this.valueToSave;
        options.classId = this.classId;
         this.trService.updateGrade(empId,options).then((_)=>{
          this.alert.successToast("Grade Updated Successfully");
          this.readyData();
        }).finally(()=>{
          this.isSaving = false;
        })
        break;
      case 'score':
        options.score = this.valueToSave;
        options.classId = this.classId;
         this.trService.updateScore(empId,options).then((_)=>{
          this.alert.successToast("Score Updated Successfully");
          this.readyData();
        }).finally(()=>{
          this.isSaving = false;
        })
        break;
      case 'notes':
        options.gradeNotes = this.valueToSave;
        options.classId = this.classId;
         this.trService.updateNotes(empId,options).then((_)=>{
          this.alert.successToast("Grade Notes Updated Successfully");
          this.readyData();
        }).finally(()=>{
          this.isSaving = false;
        })
        break;
        case 'completionDate':
          options.completionDate = this.valueToSave + 'T12:00:00';
          options.classId = this.classId;
           this.trService.updateEnrollment(empId,options).then((_)=>{
            this.alert.successToast("Completion Date Updated Successfully");
            this.readyData();
          }).finally(()=>{
            this.isSaving = false;
          })
          break;
    }
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  checkInputTbl(event:any){
    if(event.data){
      if(this.validGrades.includes(String(event.data).trim().toUpperCase())){
        this.tblGradeError = false;
      }
      else{
        this.tblGradeError =true
      }
    }
    else{
      this.tblGradeError = true;
    }
  }

  async bulkGradeUpdate(option:any){
    this.spinner = true;
    this.isSaving = true;   
    await this.trService.bulkUpdate(this.classId,option).then((_)=>{
      this.alert.successToast("Grades Updated In Bulk");
      this.readyData();
    }).finally(()=>{
      this.spinner = false;
      this.gradeError = true;
      this.isSaving = false;
      this.dialog.closeAll()
    })
  }

  zeroCheck(number:number){
    return number === 0;
  }

  async unenrollStudent(name:any){
    if(this.doUnEnrollFor === 'table'){
      var options = new TrainingStudentCreationOptions();
      options.employeeIds = [];
      options.employeeIds.push(this.selectedEMPId);
      await this.trService.unLinkedEmployees(this.classId,options).then((_)=>{
        this.readyData();
        this.alert.successToast("Student Enrollement and Related record deleted");
        this.dataBroadcastService.refreshRosterData.next(null);
      })
    }else{
      var options = new TrainingStudentCreationOptions();
      options.employeeIds = [];
      this.unlinkIds.forEach((res)=>{
        options.employeeIds.push(res);
      })

      await this.trService.unLinkedEmployees(this.classId,options).then((_)=>{
        this.readyData();
        this.alert.successToast("Student Enrollement and Related record deleted");
        this.dataBroadcastService.refreshRosterData.next(null);
      })
    }

  }

  openRetakeStatusPanel(templateRef:any,row:RosterOverviewVM){
    this.selectedEMPId = row.classEmployeeId;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal)
  }

  openFlyInPanelUpdateEnrollment(templateRef: any,employee:any) {
    this.currrentEmployee = employee  
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  updateClassScheduleEnrollment(option:any){
    this.trService.updateEnrollment(this.currrentEmployee.classEmployeeId,option).then((_)=>{
      this.alert.successToast("Enrollment Data Updated Successfully");
      this.readyData();
    }).finally(()=>{      
      this.flyPanelSrvc.close();
    })
  }
 

}
