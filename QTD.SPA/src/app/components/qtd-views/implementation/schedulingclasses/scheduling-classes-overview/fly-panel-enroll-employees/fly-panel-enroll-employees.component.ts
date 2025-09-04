import {SelectionModel} from '@angular/cdk/collections';
import {TemplatePortal} from '@angular/cdk/portal';
import {DatePipe} from '@angular/common';
import {Component, EventEmitter, Input, OnInit, Output, ViewContainerRef} from '@angular/core';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {Employee} from 'src/app/_DtoModels/Employee/Employee';
import {EmployeeCreateOptions} from 'src/app/_DtoModels/Employee/EmployeeCreateOptions';
import {ClassScheduleEnrollOptions} from 'src/app/_DtoModels/SchedulesClassses/ClassScheduleEnrollOptions';
import {TrainingStudentCreationOptions} from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import {EmployeesService} from 'src/app/_Services/QTD/employees.service';
import {TrainingService} from 'src/app/_Services/QTD/training.service';
import {FlyInPanelService} from 'src/app/_Shared/services/flyInPanel.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';

export interface Employees {
  name: string;
}

@Component({
  selector: 'app-fly-panel-enroll-employees',
  templateUrl: './fly-panel-enroll-employees.component.html',
  styleUrls: ['./fly-panel-enroll-employees.component.scss']
})
export class FlyPanelEnrollEmployeesComponent implements OnInit {
  @Output() idSelected = new EventEmitter<any>();
  showSpinner = false;
  @Input() classData!: any;
  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<Employee>();
  selection = new SelectionModel<Employee>(true, []);
  employeeData!: Employee[];
  employeeDataCopy!: Employee[];
  isEmployeeFilterLabel : boolean = false;
  employeeFilterLabelValue : string ;
  @Input() classId = "";
  @Input() startDate: any = "";
  @Input() endDate: any = "";
  @Input() trainingTitle: any = "";
  empIds: any[] = [];
  orgId = "";
  posId = "";
  searchString = "";

  selectedEMPId = "";

  description = "";

  procedureReviewId = "";
  datePipe = new DatePipe('en-us');
  spinner = false;
  displayedColumns: string[] = [
    "select",
    "image",
    "employees",
  ];

  openPanel: 'assign' | 'org' | 'pos' | 'main' = 'main';

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private empSrvc: EmployeesService,
    private trService: TrainingService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private labelPipe:LabelReplacementPipe
  ) {
  }

  ngOnInit(): void {
    this.readyEmployeesData();
  }


  async readyEmployeesData() {
    this.showSpinner = true
    this.selection.clear();
    this.employeeData = await this.empSrvc.getEmployeeList();
    this.employeeDataCopy = Object.assign(this.employeeData);

    this.readyempIds();
    this.dataSource.data = this.employeeData;
    this.showSpinner = false
  }


  addToClass(event) {
    this.idSelected.emit(event);
    this.closeProvider()
  }


  async readyempIds() {
    var emps = await this.trService.getLinkedEmployees(this.classData.id);
    this.empIds = emps.map(d => d.empId);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.filter(x=>this.employeeData.includes(x)).length;
    const numRows = this.employeeData.filter(x=>!this.empIds.includes(x.id)).length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
    this.clearSelection() : this.employeeData.forEach((row) => {
        this.empIds.includes(row.id) ? '' : this.selection.select(row)
      });
  }

  closeProvider() {

    this.flyPanelSrvc.close();
  }

  openFlyInPanelAssignScore(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  async enrollStudents() {
    this.spinner = true;
    var options: TrainingStudentCreationOptions = new TrainingStudentCreationOptions()
    options.classScheduleId = this.classData.id;
    options.employeeIds = this.selection.selected.map((data) => {
      return data.id
    });
    await this.trService.createEmployees(options).then(async (_) => {
      this.alert.successToast(await this.labelPipe.transform('Employee') + "s Added To Class");
      this.idSelected.emit();
      this.closed.emit();
    }).finally(() => {
      this.spinner = false;
    })
  }

  async removeEMP() {
    var options = new TrainingStudentCreationOptions();
    options.employeeIds = [];
    options.employeeIds.push(this.selectedEMPId);
    await this.trService.unLinkedEmployees(this.classData.id, options).then((_) => {
      this.readyEmployeesData();
      this.alert.successToast("Student Enrollement and Related record deleted");
      this.idSelected.emit();
      this.closed.emit();
    })

  }

  removeEMPDialog(templateRef: any, employee: Employee) {
    this.selectedEMPId = employee.id;
    this.description = `You are selecting to remove ${employee.person.firstName + ' ' + employee.person.lastName} from Class.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  filterByOrg(event: any) {
    this.orgId = event;
    this.posId = "";
    this.filterEmployees();
    this.openPanel = 'main';
    this.isEmployeeFilterLabel = true;
  }

  selectedOrganization(event: any){
    this.employeeFilterLabelValue = event;
  }

  filterByPos(event: any) {
    this.posId = event.data;
    this.orgId = "";
    this.filterEmployees();
    this.openPanel = 'main';
    this.isEmployeeFilterLabel = true;
  }

  positionSelected(event: any){
    this.employeeFilterLabelValue = event;
  }

  clearFilters() {
    this.isEmployeeFilterLabel = false;
    this.orgId = "";
    this.posId = "";
    this.searchString = "";
    this.filterEmployees();
    this.employeeData = Object.assign(this.employeeDataCopy);
  }

  filterEmployees() {

    this.employeeData = this.employeeDataCopy.filter((emp) => {
      return (this.posId === "" ? true : emp.employeePositions.map(d => d.positionId).includes(this.posId))
        && (this.orgId === "" ? true : emp.employeeOrganizations.map(d => d.organizationId).includes(this.orgId))
        && ((emp.person.firstName + ' ' + emp.person.lastName).trim().toLowerCase().includes(this.searchString.trim().toLowerCase())
          || emp.person.username.trim().toLowerCase().includes(this.searchString.trim().toLowerCase()))

    })
  }

  clearSelection() {   
    this.employeeData.forEach((row) => {
        this.selection.deselect(row);
    });
}
}

