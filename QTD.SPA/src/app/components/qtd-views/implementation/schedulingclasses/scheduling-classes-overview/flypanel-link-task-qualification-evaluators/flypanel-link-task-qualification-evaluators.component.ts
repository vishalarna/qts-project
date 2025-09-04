import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ClassScheduleEvaluatorLinksVM, EMPSettingsTQTaskEvaluation } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiClassScheduleTqReleaseSettingService } from 'src/app/_Services/QTD/ClassScheduleTestReleaseSettings/api.classScheduleTqReleaseSetting.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-link-task-qualification-evaluators',
  templateUrl: './flypanel-link-task-qualification-evaluators.component.html',
  styleUrls: ['./flypanel-link-task-qualification-evaluators.component.scss'],
})
export class FlypanelLinkTaskQualificationEvaluatorsComponent implements OnInit {
  @Output() tqSelected = new EventEmitter<any>();
  @Output() refreshtq = new EventEmitter<any>();

  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  employeeData!: any[];
  employeeDataCopy!: any[];
  @Input() classScheduleId = "";
  @Input() startDate: any = "";
  @Input() endDate: any = "";
  @Input() trainingTitle: any = "";
  @Input() qualData!:any;
  @Input() isComingFromClassSchedule;

  @Input() IlaId="";

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
  linkText : string = "";
  selectableEmployees:any;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private employeeService: EmployeesService,
    private alert : SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    public dialog: MatDialog,
    private ilaService: IlaService,
    private classTQSettingService : ApiClassScheduleTqReleaseSettingService,
    private labelPipe:LabelReplacementPipe
  ) { }


  empIds: any[] = [];

  ngOnInit(): void {
    this.readyEvalData();
  }

  async readyEvalData() {
    this.linkText = `Link to ${this.isComingFromClassSchedule ? "Class" : await this.labelPipe.transform("ILA")}`;
    this.selection.clear();
    this.employeeData = await this.employeeService.getAllEvaluators()
    this.employeeDataCopy = Object.assign(this.employeeData);
    
    this.readyEvalIds();
    this.dataSource.data = this.employeeData;
  }

  async readyEvalIds() {
    
    var emps = this.qualData;
    this.empIds = emps.map(d => d.id);
  }

  /** Whether the number of selected elements matches the total number of rows. */
   isAllSelected() {
    this.selectableEmployees = this.employeeData.filter(emp => !this.empIds.includes(emp.id));
    const numSelected = this.selection.selected.length;
    const numRows = this.selectableEmployees.length;
    return numSelected === numRows && numRows > 0;
  }
 
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()? this.selectableEmployees.forEach(emp => this.selection.deselect(emp)) : this.selectableEmployees.forEach(emp => this.selection.select(emp));
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
    let evaluatorIds = this.selection.selected.map((data: any) => { return data.id;});
    if(this.isComingFromClassSchedule){
      await this.createTQTaskEvaluationsByClassAsync(evaluatorIds);
    }
    else{
      await this.createTQTaskEvaluationsByILAAsync(evaluatorIds);
    }
    this.readyEvalData();
  }
  async createTQTaskEvaluationsByILAAsync(evalIds : string[]){
    let createEvaluator: EMPSettingsTQTaskEvaluation = {
      ilaId: this.IlaId,
      evaluatorIds: evalIds
    }
    await this.ilaService.createTQTaskEvaluations(createEvaluator).then((res) => {
      this.dataBroadcastService.refreshEvalQualification.next(null);
      this.alert.successToast(`Link has been created `);
    }).catch((res: any) => {
      this.alert.errorToast(res);
    }).finally(() => {
      this.spinner = false;
      this.closeProvider();
      this.refreshtq.emit(true);
    })
  }
  async createTQTaskEvaluationsByClassAsync(evalIds : string[]){
    let createEvaluator: ClassScheduleEvaluatorLinksVM = {
      classScheduleId: this.classScheduleId,
      evaluatorIds: evalIds
    }
    await this.classTQSettingService.linkEvaluatorsFromClassSchedule(createEvaluator).then((res) => {
      this.dataBroadcastService.refreshEvalQualification.next(null);
      this.alert.successToast(`Link has been created `);
    }).catch((res: any) => {
      this.alert.errorToast(res);
    }).finally(() => {
      this.spinner = false;
      this.closeProvider();
      this.refreshtq.emit(true);
    })
  }

  async removeEMP() {
    let evaluatorIdToRemove = [this.selectedEMPId];
    if(this.isComingFromClassSchedule){
      await this.removeTQTaskEvaluationsByClassAsync(evaluatorIdToRemove);
    }
    else{
      await this.removeTQTaskEvaluationsByILAAsync(evaluatorIdToRemove);
    }
    this.readyEvalData();
  }
  async removeTQTaskEvaluationsByILAAsync(evalIds : string[]){
    let removeEvaluator : EMPSettingsTQTaskEvaluation = {
      ilaId: this.IlaId,
      evaluatorIds: evalIds
    }
    await this.ilaService.removeTQTaskEvaluations(removeEvaluator).then((_)=>{
      this.alert.successToast("Link eval is deleted ");
      this.dataBroadcastService.refreshProcedureReviewData.next(null);
      this.refreshtq.emit(true);
      this.closeProvider();
    })
  }
  async removeTQTaskEvaluationsByClassAsync(evalIds : string[] ){
    let removeEvaluator : ClassScheduleEvaluatorLinksVM = {
      classScheduleId: this.classScheduleId,
      evaluatorIds: evalIds
    }
    await this.classTQSettingService.unlinkEvaluatorsFromClassSchedule(removeEvaluator).then((_)=>{
      this.alert.successToast("Link eval is deleted ");
      this.dataBroadcastService.refreshProcedureReviewData.next(null);
      this.refreshtq.emit(true);
      this.closeProvider();
    })
  }

  removeEMPDialog(templateRef: any, employee: any) {
    
    this.selectedEMPId = employee.id;
    this.description = `You are selecting to remove ${employee.person.firstName + ' ' + employee.person.lastName} from Eval.`
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
    this.filterQal();
    this.openPanel = 'main';
  }

  filterByPos(event: any) {
    this.selection.clear();
    this.posId = event.data;
    this.filterQal();
    this.openPanel = 'main';
  }

  clearFilters() {
    this.selection.clear();
    this.orgId = "";
    this.posId = "";
    this.searchString = "";
    this.filterQal();
    this.employeeData = Object.assign(this.employeeDataCopy);
  }

  filterQal() {
    
    this.employeeData = this.employeeDataCopy.filter((emp) => {
      return (this.posId === "" ? true : emp.employeePositions.map(d => d.positionId).includes(this.posId))
        && (this.orgId === "" ? true : emp.employeeOrganizations.map(d => d.organizationId).includes(this.orgId))
        && ((emp.person.firstName + ' ' + emp.person.lastName).trim().toLowerCase().includes(this.searchString.trim().toLowerCase())
          || emp.person.username.trim().toLowerCase().includes(this.searchString.trim().toLowerCase()))

    })
  }

  searchevaluator(event: any) {
    this.searchString = event.target.value;
    this.filterQal()
  }
}
