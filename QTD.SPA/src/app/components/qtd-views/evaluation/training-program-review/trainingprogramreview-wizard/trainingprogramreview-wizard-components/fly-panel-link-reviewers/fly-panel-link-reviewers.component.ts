import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EmployeeNameOnlyVM } from 'src/app/_DtoModels/Employee/EmployeeNameOnlyVM';
import { TrainingProgramReview_Employee_Link_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_Employee_Link_ViewModel';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';

@Component({
  selector: 'app-fly-panel-link-reviewers',
  templateUrl: './fly-panel-link-reviewers.component.html',
  styleUrls: ['./fly-panel-link-reviewers.component.scss']

})
export class FlyPanelLinkReviewersComponent implements OnInit {
  @Input() reviewers: TrainingProgramReview_Employee_Link_ViewModel[]=[];
  @Input() handleLoad: ()=>void;
  @Input() handleXClick: ()=>void;
  @Input() handleSetReviewersClick: (e:TrainingProgramReview_Employee_Link_ViewModel[])=>void;
  employeesListData :MatTableDataSource<EmployeeNameOnlyVM>= new MatTableDataSource();
  employees :EmployeeNameOnlyVM[];
  employeesColumns: string[] = ['empId','name'];
  selectEmployees:EmployeeNameOnlyVM[];
  @ViewChild('empSort') sort: MatSort;
  constructor( private empService: EmployeesService,) {}

  ngOnInit(): void {
   this.loadAsync();
  }

  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  _handleXClick() {
    if (this.handleXClick &&typeof this.handleXClick === 'function') {
      this.handleXClick();
    }
  }

  _handleSetReviewersClick(e) {
    if (this.handleSetReviewersClick &&typeof this.handleSetReviewersClick === 'function') {
      this.handleSetReviewersClick(e);
    }
  }

  async loadAsync() {
    this.employees = await this.empService.getEmployeeListNamesOnly();
    this.selectEmployees = this.reviewers?this.reviewers.map(x => {
      return this.employees.find(y => x.employeeId === y.empId);
    }):[];
    this.employeesListData = new MatTableDataSource(this.employees);
    this.employeesListData.sort = this.sort;
    this.employeesListData.sortingDataAccessor = this.customSortAccessor;
    this._handleLoad();
  }

  xClick() {
    this._handleXClick();
  }
  
  setReviewersClick() {
    let reviewers=this.selectEmployees.map(x=>{
      let reviewer = new TrainingProgramReview_Employee_Link_ViewModel();
      reviewer.employeeId=x.empId;
      reviewer.employeePersonFullName=x.firstName + " " + x.lastName;
      return reviewer;
    });
    this._handleSetReviewersClick(reviewers);
    this._handleXClick();
  }
  
  isSelected(empId:string){
    return this.selectEmployees.some(item => item.empId === empId);
  }
  employeeSelectionChange(employee:EmployeeNameOnlyVM){
    let index =this.selectEmployees.findIndex(x=>x.empId===employee.empId);
    if(index === -1){
      this.selectEmployees.push(employee);
    }
    else{
      this.selectEmployees.splice(index,1);
    }
  }
  searchEmployees(event: any) {
    let employees = this.employees;
    employees = this.employees.filter((item) =>
        item.firstName.toLowerCase().includes(event.target.value.toLowerCase()) || item.lastName.toLowerCase().includes(event.target.value.toLowerCase()));
    this.employeesListData.data =employees;
  }

  customSortAccessor = (employee: EmployeeNameOnlyVM, column: string): string  => {
    switch (column) {
      case 'name':
        return employee.firstName + " " + employee.lastName;
      default:
        return employee[column];
    }
  };
}
