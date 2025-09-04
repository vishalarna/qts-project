import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { DIFSurveyEmployeeLinkUnlinkOptions } from '@models/DIFSurvey/DIFSurveyEmployeeLinkUnlinkOptions';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';
import { DIFSurvey_EmployeeVM } from '@models/DIFSurvey/DIFSurvey_EmployeeVM';
import { Store } from '@ngrx/store';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { ApiDifSurveyEmployeeService } from 'src/app/_Services/QTD/DifSurveyEmployee/api.difsurvey-employee.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-view-enrollment',
  templateUrl: './view-enrollment.component.html',
  styleUrls: ['./view-enrollment.component.scss']
})
export class ViewEnrollmentComponent implements OnInit {
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
 
  public pieChartData: ChartData<'pie'>;
 
  hideChart = false;
  enrollmentDataSource: MatTableDataSource<DIFSurvey_EmployeeVM>=new MatTableDataSource<DIFSurvey_EmployeeVM>();
  selection = new SelectionModel<DIFSurvey_EmployeeVM>(true, []);
  @ViewChild(MatSort) sort: MatSort;
  tableColumns:string[];
  difSurveyId:string;
  difSurvey:DIFSurveyVM = new DIFSurveyVM();
  difSurveyList: DIFSurveyVM = new DIFSurveyVM();
  isLoading:boolean=false;
  unlinkDescription:string;
  unlinkHeader:string;
  unlinkEmpId:string;
  isSingleUnlink:boolean;
  surveyStatus:string;

  constructor(private router: Router,
    private route : ActivatedRoute,
    private difSurveyService: ApiDifSurveyService,
    private difSurveyEmpService: ApiDifSurveyEmployeeService,
    private alert: SweetAlertService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private store: Store<{ toggle: string }>,
    private labelPipe:LabelReplacementPipe
    ) { }

  ngOnInit(): void {
    this.isLoading=true;
    this.selection.clear();
    this.store.dispatch(sideBarClose());
    this.tableColumns = ['checkBox', 'empFirstName', 'positions', 'organizations', 'releasedDate', 'completedDate', 'status', 'action'];
    this.route.params.subscribe(params => {
      this.difSurveyId = params['id'];
    });
    this.loadAsync();
  }

  goBack(){
    this.router.navigate(['/analysis/dif-survey/overview']);
  }
  async loadAsync(){
    await this.difSurveyService.getEnrollmentsByIdAsync(this.difSurveyId).then((res) => {
      this.selection.clear();
      this.difSurvey = res;
      this.surveyStatus = this.difSurvey.surveyStatus;
      this.enrollmentDataSource = new MatTableDataSource<DIFSurvey_EmployeeVM>(this.difSurvey.employees);
      this.enrollmentDataSource.sort=this.sort;
      this.preparePieChart(this.enrollmentDataSource.data);
      this.isLoading=false;
    });
  }
  preparePieChart(employees:DIFSurvey_EmployeeVM[]){
    var notStartedEmps= employees.filter(x=>x.status.toUpperCase() == "NOT STARTED").length;
    var inProgressEmps= employees.filter(x=>x.status.toUpperCase() == "IN PROGRESS").length;
    var completedEmps= employees.filter(x=>x.status.toUpperCase() == "COMPLETED").length;
    this.pieChartData = {
      datasets: [
        {
          data: [notStartedEmps, inProgressEmps, completedEmps],
          backgroundColor: ['#eecd12', '#2292d3', '#40c27b'],
          borderColor: ['#c2a710', ' #1c6e9e', '#339961'],
          hoverBackgroundColor: [
            '#c2a710',
            '#1c6e9e',
            '#339961',
          ],
          hoverBorderColor: [
            '#eecd12',
            '#2292d3',
            '#40c27b',
          ],
        },
      ],
    };
  }
  getPercentage(index:number){
    if(this.enrollmentDataSource.data.length>0){
      var count =this.pieChartData?.datasets[0]?.data[index] ?? 0;
      return `${((count/this.enrollmentDataSource.data.length)*100).toFixed(0)}%`;
    }
    else{
      return "0%"
    }
  }
  viewResults(){
    this.router.navigate(['/analysis/dif-survey', this.difSurveyId, 'results']);
  }

  removeItemsModal(templateRef: any, row?:DIFSurvey_EmployeeVM) {
    if(row == null){
      this.isSingleUnlink=false;
      var selectedEmpNames= this.selection.selected.map(s=> s.empFirstName +  " " + s.empLastName);
      this.unlinkHeader= "Remove Employee(s)"
      this.unlinkDescription = `You are selecting to remove Employee(s) <b>${selectedEmpNames.join(", ")}</b> from the selected DIF Survey <b>${this.difSurvey?.surveyTitle}</b>.If the Employees have not started the DIF Survey  in EMP, un-enrolling the Employees will recall the DIF Survey from EMP. If the Employees have completed the DIF Survey , un-enrolling the Employees will remove all completion information.`;
    }
    else{
      this.isSingleUnlink=true;
      this.unlinkEmpId=row.employeeId;
      this.unlinkHeader="Remove Employee";
      this.unlinkDescription = `You are selecting to remove Employee <b>${row.empFirstName + " " + row.empLastName}</b> from the selected DIF Survey <b>${this.difSurvey?.surveyTitle}</b>.If the Employee has not started the DIF Survey in EMP, un-enrolling the Employee will recall the DIF Survey from EMP. If the Employee has completed the DIF Survey, un-enrolling the Employee will remove all completion information.`;
    }
      const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async removeEmployeeAsync() {
    var empIdsToUnlink = this.isSingleUnlink ? [this.unlinkEmpId] : Array.from(new Set(this.selection.selected.map(z=>z.employeeId)));
    var unlinkOptions = new DIFSurveyEmployeeLinkUnlinkOptions();
    unlinkOptions.difSurveyId = this.difSurvey?.id;
    unlinkOptions.employeeIds = empIdsToUnlink;
    await this.difSurveyEmpService.unlinkEmployeesAsync(unlinkOptions).then(async res => {
      if (res?.status == 200) {
        await this.loadAsync()
        if(this.isSingleUnlink){
          this.alert.successToast(await this.labelPipe.transform('Employee') + " Successfully Removed");
        }else{
          this.alert.successToast( await this.labelPipe.transform('Employee') + "(s) Successfully Removed");
        }
      }
    })
  }

  openFlyInPanelAddEmployees(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }

  updateEmployeesList(event: DIFSurvey_EmployeeVM[]) {
    this.difSurvey.employees = event;
    this.enrollmentDataSource = new MatTableDataSource(this.difSurvey.employees);
    this.flyPanelService.close();
    this.loadAsync();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.difSurvey.employees.filter(x => x.status.toUpperCase() !== 'COMPLETED').length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.difSurvey.employees.forEach((row) => {
        if (row.status.toUpperCase() !== 'COMPLETED') {
          this.selection.select(row);
        }
        });
  }
}

