import {SelectionModel} from '@angular/cdk/collections';
import {AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';
import {DataTableDirective} from 'angular-datatables';
import {Observable, Subject, Subscription} from 'rxjs';
import {Employee} from "../../../../../_DtoModels/Employee/Employee";
import {
  ClientSettings_Notification_Step_Recipient
} from "../../../../../_DtoModels/ClientsSettingsNotification/ClientSettings_Notification_Step_Recipient";

@Component({
  selector: 'app-employee-picker',
  templateUrl: './employee-picker.component.html',
  styleUrls: ['./employee-picker.component.scss']
})
export class EmployeePickerComponent implements AfterViewInit, OnDestroy, OnInit {
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  public dtOptions: DataTables.Settings = {};
  @Input()
  Employees: Array<Employee>;
  @Input()
  mode: string;
  public dtTrigger: Subject<any> = new Subject();

  @Input()
  SelectedEmployees: Array<ClientSettings_Notification_Step_Recipient>;

  @Output()
  employeeListChangedEvent: EventEmitter<any> = new EventEmitter();

  checkedEmployee: Array<any> = [];
  unCheckedEmployee: Array<any> = [];

  constructor() {
  }

  ngOnInit(): void {
    this.dtOptions = {
      destroy: true,
      ordering: true,
      pageLength: 50,
      info: false,
      paging: false,
      search: false,

      language: {searchPlaceholder: 'Enter keywords to search'},
      searching: true,
      scrollX: true,
    };
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(null);
  }

  public ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  getCheckedValue(item: any): boolean {
    return !!(this.SelectedEmployees.filter(dd => dd.employeeId == item.id)[0]);
  }

  public handleEmployeeChecked(value: any, item: Employee) {
    if (value.target.checked) {
      this.checkedEmployee.push(item.id);
      if (this.unCheckedEmployee.length > 0 || this.unCheckedEmployee.includes(item.id)) {
        const employeeIndex = this.unCheckedEmployee.indexOf(item.id);
        this.unCheckedEmployee.splice(employeeIndex, 1);
      }
      const empPickerEmitModel = {
        checked: this.checkedEmployee,
        unchecked: this.unCheckedEmployee
      }
      this.employeeListChangedEvent.emit(empPickerEmitModel);
    } else {
      this.unCheckedEmployee.push(item.id);
      if (this.checkedEmployee.length > 0 || this.checkedEmployee.includes(item.id)) {
        const employeeIndex = this.checkedEmployee.indexOf(item.id);
        this.checkedEmployee.splice(employeeIndex, 1);
      }
      const empPickerEmitModel = {
        checked: this.checkedEmployee,
        unchecked: this.unCheckedEmployee
      }
      this.employeeListChangedEvent.emit(empPickerEmitModel);
    }
  }
}
