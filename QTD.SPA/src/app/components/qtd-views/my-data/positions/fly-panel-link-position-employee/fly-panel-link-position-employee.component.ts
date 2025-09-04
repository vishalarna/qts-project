import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { DatePipe, formatDate } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeSummary } from 'src/app/_DtoModels/Employee/EmployeeSummary';
import { Position_Employee_LinkOptions } from 'src/app/_DtoModels/Position_Employee_Link/Position_Employee_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-link-position-employee',
  templateUrl: './fly-panel-link-position-employee.component.html',
  styleUrls: ['./fly-panel-link-position-employee.component.scss']
})
export class FlyPanelLinkPositionEmployeeComponent implements OnInit {
  linkPos: boolean = true;
  IsTrainee : boolean = false;
  dateError: boolean = false;
  datePipe = new DatePipe('en-us');
  startDate:any;
  showActive: boolean = true;
  isLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinkedIds: any[] = [];
  linkedIds: any[] = [];
  employees: EmployeeSummary[];
  filteredList: EmployeeSummary[];
  subscription = new SubSink();
  linkForm: UntypedFormGroup;
posId = '';
  constructor(
    private posSrvc: PositionsService,
    private empSrvc: EmployeesService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    private fb: UntypedFormBuilder,
    private dataBroadcastService:DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.startDate =  this.datePipe.transform(Date.now(), "yyyy-MM-dd");
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      
      this.posId = String(res.id).split('-')[0];
    });
    this.getEmployees();
    this.readyLinkForm();
  }

  readyLinkForm() {
    this.linkForm = this.fb.group({
      startDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      search: new UntypedFormControl('')

    });
  }

  filterData(e: Event) {
    
    let filterString = (e.target as HTMLInputElement).value;
    this.filteredList = [
      ...this.employees.filter((x) =>
        x.firstName.toLowerCase().includes(String(filterString).toLowerCase()) ||
        x.lastName.toLowerCase().includes(String(filterString).toLowerCase())
      ),
    ];
  }

  filterStatus(active: boolean) {
    this.filteredList = [...this.employees.filter((x) => x.active == active)];
    this.showActive = active;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  linkToPosition() {
    let option = new Position_Employee_LinkOptions();
    option.employeeIds = this.linkedIds;
    option.trainee = this.IsTrainee;
    option.startDate = this.linkForm.get('startDate')?.value;
    this.posSrvc.LinkEmployeesToPosition(this.posId, option).then(async (res) => {
      this.alert.successToast( await this.labelPipe.transform('Employee') + '(s) linked to ' + await this.transformTitle('Position'));
      this.closed.emit('fp-link-pos-emp-closed');
      this.refresh.emit('refresh position tbl');
    });
  }

  positionChecked(checked: boolean, id: any) {
    if (checked) {
      this.linkedIds.push(id);
    } else {
      this.linkedIds.splice(this.linkedIds.indexOf(id), 1);
    }
    this.linkedIds = [...new Set(this.linkedIds)];
  }
  traineehecked(checked: boolean)
    {
      if (checked)
       {
        this.IsTrainee = true;
       }
       else
       {
        this.IsTrainee = false;
       }

    }
  async getEmployees() {
    await this.empSrvc.getAllSimplifiedEmployees().then((res) => {
      
      this.employees = res;
      this.filteredList = res;

      this.filterStatus(this.showActive);
    });
  }

  clearSearch:string ='';
  clearFilter(){
    this.linkForm.get('search').setValue('');
    this.getEmployees();
  }
}
