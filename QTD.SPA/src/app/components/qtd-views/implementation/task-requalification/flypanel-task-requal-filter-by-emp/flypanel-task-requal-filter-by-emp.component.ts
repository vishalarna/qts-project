import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';

@Component({
  selector: 'app-flypanel-task-requal-filter-by-emp',
  templateUrl: './flypanel-task-requal-filter-by-emp.component.html',
  styleUrls: ['./flypanel-task-requal-filter-by-emp.component.scss']
})
export class FlypanelTaskRequalFilterByEmpComponent implements OnInit {
  emps: any[] = [];
  empList:any[] = [];
  positions:any[] = [];
  positionList:any[] = [];
  organizations:any[] = [];
  organizationList:any[] = [];
  selectedPositionId: string = "";
  selectedOrganizationId: string = "";
  filterText:any

  @Output() empselected = new EventEmitter<any>();
  @Output() closed = new EventEmitter<any>();

  empForm = new UntypedFormGroup({
    'emp': new UntypedFormControl(''),
    'searchTxt' : new UntypedFormControl(''),
    'organization' : new UntypedFormControl(''),
    'position' : new UntypedFormControl(''),
  });

  constructor(
    private empService: EmployeesService,
    private tqService : TaskRequalificationService,
    private dataBroadcastService : DataBroadcastService,
    private positionService: PositionsService,
    private organizationService: OrganizationsService
  ) { }

  ngOnInit(): void {
    this.readyEmps();
    this.getPositions();
    this.getOrganizations();
  }

  async readyEmps() {
    this.emps = await this.empService.getEmployeeWithOrgAndPosList();
    this.emps = this.emps.sort((a,b) => a.fullName.trim().toLowerCase().localeCompare(b.fullName.trim().toLowerCase()));
    this.empList = Object.assign(this.emps);
  }

  emitEMP(){
    var empId = this.empForm.get('emp')?.value;
    this.dataBroadcastService.filterByEmp.next(empId);
    this.closed.emit();
  }

  employeeSearch(value: any) {
    var filterString = this.empForm.get('searchTxt')?.value;;
    
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.emps = this.empList.filter((f) => {
      return f.firstName.toLowerCase().trim().includes(filterString) || f.lastName.toLowerCase().trim().includes(filterString);
    });
  }

  async getPositions() {
    this.positions= await this.positionService.getAll();
    this.positionList = Object.assign(this.positions);
  }

  positionSearch(value: any) {
    var filterString = this.empForm.get('searchTxt')?.value;;
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.positions = this.positionList.filter((f) => {
      return f.positionTitle.toLowerCase().trim().includes(filterString);
    });
  }

  onSelectPosition(event: any){
    this.selectedPositionId = event;
    if(this.selectedOrganizationId == null || this.selectedOrganizationId == ""){
      this.emps = this.empList.filter(x => x.employeePositions.some(pos => pos.id === this.selectedPositionId));
    }
    else {
      this.emps = this.empList.filter(x => x.employeePositions.some(pos => pos.id === this.selectedPositionId) 
      && x.employeeOrganizations.some(org => org.id === this.selectedOrganizationId));
    }
  }

  clearSelectedPosition(){
    this.selectedPositionId = "";
    this.empForm.get('position').setValue(null);
    this.empForm.get('emp').setValue(null);
    if(this.selectedOrganizationId != null && this.selectedOrganizationId != ""){
      this.onSelectOrganization(this.selectedOrganizationId);
    }
    else{
      this.emps = this.empList;
    }
  }

  async getOrganizations() {
    this.organizations= await this.organizationService.getAll();
    this.organizationList = Object.assign(this.organizations);
  }

  organizationSearch(value: any) {
    var filterString = this.empForm.get('searchTxt')?.value;;
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.organizations = this.organizationList.filter((f) => {
      return f.name.toLowerCase().trim().includes(filterString);
    });
  }

  onSelectOrganization(event: any){
    this.selectedOrganizationId = event;
    if(this.selectedPositionId == null || this.selectedPositionId == ""){
      this.emps = this.empList.filter(x => x.employeeOrganizations.some(org => org.id === this.selectedOrganizationId));
    }
    else {
      this.emps = this.empList.filter(x => x.employeeOrganizations.some(org => org.id === this.selectedOrganizationId)
      && x.employeePositions.some(pos => pos.id === this.selectedPositionId));
    }
  }

  clearSelectedOrganization(){
    this.selectedOrganizationId = "";
    this.empForm.get('organization').setValue(null);
    this.empForm.get('emp').setValue(null);
    if(this.selectedPositionId != null && this.selectedPositionId != ""){
      this.onSelectPosition(this.selectedPositionId);
    }
    else{
      this.emps = this.empList;
    }
  }

}
