import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { AddEmployeeToProcedureReviewCreationOptions } from '@models/Procedure/Procedure_review';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-employee',
  templateUrl: './fly-panel-add-employee.component.html',
  styleUrls: ['./fly-panel-add-employee.component.scss']
})
export class FlyPanelAddEmployeeComponent implements OnInit {

  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<Employee>();
  selection = new SelectionModel<Employee>(true, []);
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

  procedureReviewId="";
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
    private procedureService : ProceduresService,
    private alert : SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) { }


  empIds: any[] = [];

  ngOnInit(): void {
    this.route.params.subscribe((params: any) => {
      
      if (params.hasOwnProperty('id') ) {
        this.procedureReviewId = params['id'];
        this.readyEmployeesData();
      }
    });
   
  }

  async readyEmployeesData() {
    this.selection.clear();
    this.employeeData = await this.empSrvc.getEmpWithPosAndOrg();
    this.employeeDataCopy = Object.assign(this.employeeData);
    
    this.readyempIds();
    this.dataSource.data = this.employeeData;
  }

  async readyempIds() {
    var emps = await this.procedureService.getLinkProcedureReviewEmp(this.procedureReviewId);
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
      this.selection.clear() :this.employeeData.forEach((row) => { this.empIds.includes(row.id) ? '' : this.selection.select(row) });
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
    var options:AddEmployeeToProcedureReviewCreationOptions = new AddEmployeeToProcedureReviewCreationOptions()
    options.procedureReviewId = this.procedureReviewId;
    options.employeeIds = this.selection.selected.map((data)=>{ return data.id});
    await this.procedureService.linkEmpProcedureReview(options.procedureReviewId,options).then(async (_)=>{
      this.alert.successToast("" + await this.labelPipe.transform('Employee') + "s Enrolled In " + await this.transformTitle('Procedure') + " Review");
      this.readyEmployeesData();
      this.dataBroadcastService.refreshProcedureReviewData.next(null);
      this.closed.emit();
    }).finally(() => {
      this.spinner = false;
    })
  }

  async removeEMP() {
    
    var options:AddEmployeeToProcedureReviewCreationOptions = new AddEmployeeToProcedureReviewCreationOptions()
    options.procedureReviewId = this.procedureReviewId;
    options.employeeIds.push(this.selectedEMPId);
   // options.employeeIds = this.selection.selected.map((data)=>{ return data.id});
    await this.procedureService.unLinkProcedureReviewEmp(options.procedureReviewId,options).then(async (_)=>{
      this.readyEmployeesData();
      this.alert.successToast(await this.transformTitle('Procedure') + "review " + await this.labelPipe.transform('Employee') + " has been deleted");
      this.dataBroadcastService.refreshProcedureReviewData.next(null);
    })

  }

  async removeEMPDialog(templateRef: any, employee: Employee) {
    
    this.selectedEMPId = employee.id;
    this.description = `You are selecting to remove ${employee.person.firstName + ' ' + employee.person.lastName} from ` + await this.transformTitle('Procedure') + `.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
