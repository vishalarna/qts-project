import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { DIFSurveyEmployeeLinkUnlinkOptions } from '@models/DIFSurvey/DIFSurveyEmployeeLinkUnlinkOptions';
import { DIFSurvey_EmployeeVM } from '@models/DIFSurvey/DIFSurvey_EmployeeVM';
import { Employee } from '@models/Employee/Employee';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiDifSurveyEmployeeService } from 'src/app/_Services/QTD/DifSurveyEmployee/api.difsurvey-employee.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-dif-survey-employees',
  templateUrl: './fly-panel-dif-survey-employees.component.html',
  styleUrls: ['./fly-panel-dif-survey-employees.component.scss'],
})
export class FlyPanelDifSurveyEmployeesComponent implements OnInit {
  @Input() alreadyLinkedEmployees : DIFSurvey_EmployeeVM[];
  @Input() difSurveyId : string;
  @Output() updatedEmployees = new EventEmitter<DIFSurvey_EmployeeVM[]>();
  employeeData!: Employee[];
  employeeDataCopy!: Employee[];
  selection = new SelectionModel<Employee>(true, []);
  org: { id: string, name: string } | null = null;
  pos: { id: string, name: string } | null = null;
  searchString: string;
  displayedColumns:string[] ;
  EmployeeDataSource:MatTableDataSource<any>;

  openPanel: 'filter' | 'main' = 'main';

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private empSrvc: EmployeesService,
    private difSurveyEmpService :ApiDifSurveyEmployeeService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.org = null;
    this.pos = null;
    this.searchString = ""
    this.readyEmployeesData();
    this.displayedColumns = ["id","allEmployee"];
  }

  async readyEmployeesData() {
    this.selection.clear();
    this.employeeData = await this.empSrvc.getEmployeeList();
    this.EmployeeDataSource = new MatTableDataSource(this.employeeData)    
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const visibleRows = this.EmployeeDataSource.data.filter(
      x => !this.alreadyLinkedEmployees.some(z => z.employeeId === x.id)
    );
    return visibleRows.every(row => this.selection.isSelected(row));
  }
  
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    const visibleRows = this.EmployeeDataSource.data.filter(
      x => !this.alreadyLinkedEmployees.some(z => z.employeeId === x.id)
    );
  
    const allSelected = visibleRows.every(row => this.selection.isSelected(row));
  
    if (allSelected) {
      visibleRows.forEach(row => this.selection.deselect(row));
    } else {
      visibleRows.forEach(row => this.selection.select(row));
    }
  }

  filterByValues() {
    this.filterEmployees();
    this.openPanel = 'main';
  }

  clearFilters() {
    this.org = null;
    this.pos = null;
    this.searchString = "";
    this.filterEmployees();
    this.EmployeeDataSource.data = Object.assign(this.employeeData);
  }
 
  filterEmployees() {
    this.EmployeeDataSource.data = this.employeeData.filter((emp) => {
      return (!this.pos || emp.employeePositions.some(d => d.positionId === this.pos!.id))
        && (!this.org || emp.employeeOrganizations.some(d => d.organizationId === this.org!.id))
        && ((emp.person.firstName + ' ' + emp.person.lastName).trim().toLowerCase().includes(this.searchString.trim().toLowerCase())
          || emp.person.username.trim().toLowerCase().includes(this.searchString.trim().toLowerCase()))
    });
  }
  
  getSearchValue(event: any) {
    this.searchString = event.target.value;
    this.filterEmployees()
  }
  isLinked(empId:string){
    return this.alreadyLinkedEmployees.some(item => item.employeeId === empId);
  }
  async addEmployeeeToDifSurveyAsync(){
    var linkOptions= new DIFSurveyEmployeeLinkUnlinkOptions();
    linkOptions.difSurveyId = this.difSurveyId;
    linkOptions.employeeIds = Array.from(new Set(this.selection.selected.map(z=>z.id)));
    await this.difSurveyEmpService.linkEmployeesAsync(linkOptions).then(async res=>{
      this.updatedEmployees.emit(res);
      this.alert.successToast(await this.labelPipe.transform('Employee') + "(s) Successfully Linked");
    })
  }
}
