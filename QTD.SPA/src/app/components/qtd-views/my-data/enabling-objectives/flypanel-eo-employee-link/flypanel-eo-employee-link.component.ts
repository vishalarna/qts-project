import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeSummary } from 'src/app/_DtoModels/Employee/EmployeeSummary';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-eo-employee-link',
  templateUrl: './flypanel-eo-employee-link.component.html',
  styleUrls: ['./flypanel-eo-employee-link.component.scss']
})
export class FlypanelEoEmployeeLinkComponent implements OnInit {
  linkPos: boolean = true;
  dateError: boolean = false;
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
  eoId = "";
  constructor(
    private empSrvc: EmployeesService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    private eoService : EnablingObjectivesService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      this.eoId = String(res.id).split('-')[1];
    });
    this.getEmoployees();
  }

  filterData(e: Event) {
    let filterString = (e.target as HTMLInputElement).value;
    this.filteredList = [
      ...this.employees.filter((x) =>
        x.firstName.toLowerCase().includes(String(filterString).toLowerCase()) ||
        x.lastName.toLowerCase().includes(String(filterString).toLowerCase()) ||
        x.userName.toLowerCase().includes(String(filterString).toLowerCase())
      ),
    ];
  }

  filterStatus(active: boolean) {
    this.filteredList = [...this.employees.filter((x) => x.active == active)];
    this.showActive = active;
  }

  async linkToSQ() {
    this.isLoading = true;
    var options = new EO_LinkOptions();
    options.eoId = this.eoId;
    options.employeeIds = this.linkedIds;
    options.startDate = this.startDate;
    await this.eoService.linkEmployees(options).then(async (_)=>{
      this.alert.successToast("Selected " + await this.labelPipe.transform('Employee') + "s Linked to SQ");
      this.refresh.emit();
      this.closed.emit();
    }).finally(()=>{
      this.isLoading = false;
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

  async getEmoployees() {
    await this.empSrvc.getAllSimplifiedEmployees().then((res) => {
      this.employees = res;
      this.filteredList = res;
      this.filterStatus(this.showActive);
    });
  }

  dateChanged(event: any)
  {
    
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
      this.startDate = event;
    }
    else {
      this.dateError = true;
    }
  }

}
