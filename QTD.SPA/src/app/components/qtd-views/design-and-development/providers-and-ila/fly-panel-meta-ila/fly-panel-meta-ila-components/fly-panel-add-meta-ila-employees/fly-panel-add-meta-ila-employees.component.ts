import {SelectionModel} from '@angular/cdk/collections';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import {Employee} from 'src/app/_DtoModels/Employee/Employee';
import {EmployeeOrganization} from 'src/app/_DtoModels/EmployeeOrganization/EmployeeOrganization';
import {EmployeePosition} from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import {MetaILAEmployeesLinkOptions} from 'src/app/_DtoModels/MetaILAEmployeesLink/MetaILAEmployeesLinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import {EmployeesService} from 'src/app/_Services/QTD/employees.service';
import {MetaILAService} from 'src/app/_Services/QTD/meta-ila.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-meta-ila-employees',
  templateUrl: './fly-panel-add-meta-ila-employees.component.html',
  styleUrls: ['./fly-panel-add-meta-ila-employees.component.scss']

})
export class FlyPanelAddMetaILAEmployeesComponent implements OnInit {
  @Input() metaILA: MetaILAVM;
  @Input() metaILAId: string;
  @Input() mode: string;
  @Output() closed = new EventEmitter<any>();
  isLoading: boolean = true;
  employeesListData: MatTableDataSource<Employee> = new MatTableDataSource();
  employees: Employee[];
  employeesColumns: string[] = ['empId', 'name', 'positions', 'organizations'];
  openPanel: string = 'main';
  selection = new SelectionModel<Employee>(true, []);
  orgId = "";
  posId = "";
  searchString = "";
  empIds: any[] = [];
  selectedEMPId = "";
  description = "";

  constructor(private empService: EmployeesService, public dialog: MatDialog,
              private metaILAService: MetaILAService,
              private alert: SweetAlertService,
              private labelPipe:LabelReplacementPipe
  ) {
  }

  ngOnInit(): void {
    this.loadAsync();
  }

  async loadAsync() {
    this.employees = await this.empService.getEmpWithPosAndOrg();
    this.setTableDataSource();

  }

  setTableDataSource() {
    var selectEmployees = this.metaILA.metaILA_EmployeeVM ? this.metaILA.metaILA_EmployeeVM.map(x => {
      return this.employees.find(y => x.employeeId === y.id);
    }) : [];
    this.empIds = selectEmployees.map(d => d.id);
    this.employeesListData = new MatTableDataSource(this.employees);
    this.isLoading = false;
  }

  searchEmployees(event: any) {
    this.searchString = event.target.value;
    this.filterEmployees();
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
    this.employeesListData.data = Object.assign(this.employees);
  }

  filterEmployees() {
    this.employeesListData.data = this.employees.filter((emp) => {
      return (this.posId === "" ? true : emp.employeePositions.map(d => d.positionId).includes(this.posId))
        && (this.orgId === "" ? true : emp.employeeOrganizations.map(d => d.organizationId).includes(this.orgId))
        && ((emp.person.firstName + ' ' + emp.person.lastName).trim().toLowerCase().includes(this.searchString.trim().toLowerCase())
          || emp.person.username.trim().toLowerCase().includes(this.searchString.trim().toLowerCase()))

    })
  }

  getPositionsString(positions: EmployeePosition[]) {
    return positions.length > 0 ? positions.map(x => x.position.positionTitle).join(', ') : '';
  }

  getOrganizationsString(organizations: EmployeeOrganization[]) {
    return organizations.length > 0 ? organizations.map(x => x.organization.name).join(', ') : '';
  }

  async enrollStudents() {
    this.isLoading = true;
    var options = new MetaILAEmployeesLinkOptions()
    options.metaILAIDs.push(this.metaILA.id);
    options.isComingFrom = "metaILAWizard";
    options.employeeIDs = this.selection.selected.map((data) => {
      return data.id
    });
    var res = await this.metaILAService.linkMetaILAEmployee(options);
    this.alert.successToast(await this.labelPipe.transform('Employee') + "s Added To Meta " + await this.labelPipe.transform('ILA'));
    var emps = await this.metaILAService.getMetaILAEmployees(this.metaILA.id);
    this.metaILA.metaILA_EmployeeVM = emps;
    this.setTableDataSource();
    this.closed.emit();
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() : this.employeesListData.data.forEach((row) => {
        this.empIds.includes(row.id) ? '' : this.selection.select(row)
      });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeesListData.data.length;
    return numSelected === numRows;
  }

  async removeEMPDialog(templateRef: any, employee: Employee) {
    this.selectedEMPId = employee.id;
    this.description = `You are selecting to remove ${employee.person.firstName + ' ' + employee.person.lastName} from the Meta ` + await this.labelPipe.transform('ILA') +`.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async removeEMP() {
    this.isLoading = true;
    var options = new MetaILAEmployeesLinkOptions();
    options.metaILAIDs.push(this.metaILA.id);
    options.employeeIDs = [this.selectedEMPId];
    await this.metaILAService.unlinkMetaILAEmployees(options).then((res) => {
      this.alert.successToast("Student Enrollement and Related record deleted");
    });
    var emps = await this.metaILAService.getMetaILAEmployees(this.metaILA.id);
    this.metaILA.metaILA_EmployeeVM = emps;
    this.setTableDataSource();
  }

}
