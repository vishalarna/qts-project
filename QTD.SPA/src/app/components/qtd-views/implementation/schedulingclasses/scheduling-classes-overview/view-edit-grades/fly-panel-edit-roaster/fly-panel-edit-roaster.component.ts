import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { ClassScheduleEnrollOptions } from 'src/app/_DtoModels/SchedulesClassses/ClassScheduleEnrollOptions';
import { EmployeesLinkedToSchedule } from 'src/app/_DtoModels/SchedulesClassses/EmployeesLinkedToSchedule';
import { RoastersModel } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RostersModel';
import { TrainingStudentCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { RostersService } from 'src/app/_Services/QTD/rosters.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-edit-roaster',
  templateUrl: './fly-panel-edit-roaster.component.html',
  styleUrls: ['./fly-panel-edit-roaster.component.scss']
})
export class FlyPanelEditRoasterComponent implements OnInit {
  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<Employee>();
  selection = new SelectionModel<Employee>(true, []);
  selection1 = new SelectionModel<any>(true, []);
  employeeData!: Employee[];
  employeeDataCopy!: Employee[];
  @Input() classId = "";
  @Input() startDate: any = "";
  @Input() endDate: any = "";
  @Input() trainingTitle: any = "";

  orgId = "";
  posId = "";
  searchString = "";

  selectedEMPId = "";

  description = "";

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
    private trService: TrainingService,
    private employeeService: EmployeesService,
    private rosterService: RostersService,
    private dataBroadcastService: DataBroadcastService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
  ) { }

  empIds: any[] = [];

  ngOnInit(): void {
    this.readyEmployeesData();
  }

  async readyEmployeesData() {
    this.selection.clear();
    this.employeeData = await this.employeeService.getEmpWithPosAndOrgIdsOnly();
    this.employeeDataCopy = Object.assign(this.employeeData);

    this.readyempIds();
    this.dataSource.data = this.employeeData;
    this.startDate = this.datePipe.transform(this.startDate, 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(this.endDate, 'yyyy-MM-dd');
  }

  async readyempIds() {
    var emps = await this.trService.getLinkedEmployees(this.classId);
    this.empIds = emps.map(d => d.empId);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {

    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {

    this.isAllSelected() ?
      this.selection.clear() :
      this.employeeData.forEach((row) => { this.empIds.includes(row.id) ? '' : this.selection.select(row) });
  }

  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }

  openFlyInPanelAssignScore(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  async enrollStudents() {
    this.spinner = true;
    var options = new TrainingStudentCreationOptions();
    options.employeeIds = this.selection.selected.map((data: Employee) => {
      return data.id;
    });
    options.classScheduleId = this.classId;
    await this.trService.createEmployees(options).then((_) => {
      this.readyEmployeesData();
      this.dataBroadcastService.refreshRosterData.next(null);
      this.alert.successToast("Student(s) Enrolled In Class");
      this.closed.emit();
    }).finally(() => {
      this.spinner = false;
    })
  }

  async removeEMP() {
    var options = new TrainingStudentCreationOptions();
    options.employeeIds = [];
    options.employeeIds.push(this.selectedEMPId);
    await this.trService.unLinkedEmployees(this.classId,options).then((_)=>{
      this.readyEmployeesData();
      this.alert.successToast("Student Enrollement and Related record deleted");
      this.dataBroadcastService.refreshRosterData.next(null);
    })

  }

  removeEMPDialog(templateRef: any, employee: Employee) {
    this.selectedEMPId = employee.id;
    this.description = `You are selecting to remove ${employee.person.firstName + ' ' + employee.person.lastName} from training ${this.trainingTitle}, ${this.startDate} to ${this.endDate}.<b> This action will permanently erase any student records for this Training.</b>`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  removestudent(){

  }

  filterByOrg(event: any) {
    this.selection.clear();
    this.orgId = event;
    this.filterEmployees();
    this.openPanel = 'main';
  }

  filterByPos(event: any) {
    this.selection.clear();
    this.posId = event.data;
    this.filterEmployees();
    this.openPanel = 'main';
  }

  clearFilters() {
    this.selection.clear();
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
}

