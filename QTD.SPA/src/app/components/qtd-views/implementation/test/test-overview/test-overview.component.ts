import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { StartTestDialogComponent } from '../start-test-dialog/start-test-dialog.component';
import { MatLegacyTabGroup as MatTabGroup } from '@angular/material/legacy-tabs';
import { Store } from '@ngrx/store';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { data } from 'jquery';

export interface CompletedTest {
  testNo: string;
  iLaNoWithTitle: string;
  testType: string;
  dueDate: string;
  score: string;
  grade: string;
}
export interface PendingTest {
  testNo: string;
  iLaNoWithTitle: string;
  testType: string;
  dueDate: string;
  status: string;

}
const ELEMENT_DATA_COMPLETED: CompletedTest[] = [


  { testNo: '1.1', iLaNoWithTitle: 'Task', testType: 'Task Description 1', dueDate: '20-24-2023', score: '2.00', grade: 'A' },
  { testNo: '1.3', iLaNoWithTitle: 'Testdads', testType: 'Task Description 1', dueDate: '20-24-2023', score: '4.40', grade: 'B' },

];
const ELEMENT_DATA_PENDING: PendingTest[] = [
  { testNo: '1.1', iLaNoWithTitle: 'Task', testType: 'Task Description 1', dueDate: '20-24-2023', status: 'Incomplete' },
  { testNo: '1.3', iLaNoWithTitle: 'Testdads', testType: 'Task Description 1', dueDate: '20-24-2023', status: 'Incomplete' },

];
@Component({
  selector: 'app-test-overview',
  templateUrl: './test-overview.component.html',
  styleUrls: ['./test-overview.component.scss']
})
export class TestOverviewComponent implements OnInit {
  url: string = 'Dashboard / Test';
  completedDataSource: MatTableDataSource<any> = new MatTableDataSource([]);

  pendingDataSource: MatTableDataSource<any> = new MatTableDataSource([]);
  selectedIndex:number = 1;
  isLoading: boolean = true;
  pendingLabel = 'Pending Tests ';
  completedLabel = 'Completed Tests ';

  datePipe = new DatePipe('en-us');
  employeeTestList: any[] = [];
  @ViewChild('compSort') compSort!: MatSort;
  @ViewChild('pendSort') pendSort!: MatSort;
  @ViewChild('compPage') completedDataPaginator: MatPaginator;
  @ViewChild('pendPage') pendingDataPaginator: MatPaginator;
  displayedCompletedColumns: string[] = [
    'testNo',
    'ilaWithTitle',
    'testType',
    'completedDate',
    'score',
    'grade',
    'instructpr',
    'location',
  ];
  displayedPendingColumns: string[] = [
    'testNo',
    'ilaWithTitle',
    'testType',
    'dueDate',
    'instructpr',
    'location',
    'status',
    'action'
  ];
  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;
  constructor(public dialog: MatDialog, private empService: EmployeesService, private store: Store) { }

  ngOnInit(): void {
    this.store.next(sideBarOpen());
    this.getTestsByEmployee();

    // this.completedDataSource.data = ELEMENT_DATA_COMPLETED;
    // this.pendingDataSource.data = ELEMENT_DATA_PENDING;
    //check local storage either it comes from main test page

  }
  filterCompletedTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.completedDataSource.filter = filter.trim().toLowerCase();
  }
  // sortData(sort: Sort) {

  //   this.completedDataSource.sort = this.sort;
  // }

  sortPendingData(sort: Sort) {
    // this.pendingDataSource.sort = this.sort;
    const data = this.pendingDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.pendingDataSource.data = data;
      return;
    }

    this.pendingDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'testNo':
          return this.compare(a.testNo, b.testNo, isAsc);
        case 'dueDate':
          return this.compare(a.dueDate, b.dueDate, isAsc);
        case 'testType':
          return this.compare(a.testType, b.testType, isAsc);
        case 'ilaWithTitle':
          return this.compare(a.ila, b.ila, isAsc);
        case 'ila':
          return this.compare(a.ila, b.ila, isAsc);
        case 'instructor':
          return this.compare(a.instructpr, b.instructpr, isAsc);
        case 'location':
          return this.compare(a.location, b.location, isAsc);
        case 'status':
          return this.compare(a.status, b.status, isAsc);
        default:
          return 0;
      }
    });

  }

  sortCompletedData(sort: Sort) {
    // this.completedDataSource.sort = this.sort;
    const data = this.completedDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.completedDataSource.data = data;
      return;
    }

    this.completedDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'testNo':
          return this.compare(a.testNo, b.testNo, isAsc);
        case 'completedDate':
          return this.compare(a.completedDate, b.completedDate, isAsc);
        case 'testType':
          return this.compare(a.testType, b.testType, isAsc);
        case 'ilaWithTitle':
          return this.compare(a.ila, b.ila, isAsc);
        case 'ila':
          return this.compare(a.ila, b.ila, isAsc);
        case 'instructor':
          return this.compare(a.instructpr, b.instructpr, isAsc);
        case 'location':
          return this.compare(a.location, b.location, isAsc);
        case 'status':
          return this.compare(a.status, b.status, isAsc);
        default:
          return 0;
      }
    });

  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
  sortData() {
    let sortFunction = (items: any[], sort: MatSort): any[] => {
      if (!sort.active || sort.direction === '') {
        return items;
      }

      return items.sort((a: any, b: any) => {
        let comparatorResult = 0;
        switch (sort.active) {
          case 'name':
            comparatorResult = a.name.localeCompare(b.name);
            break;
          case 'class':
            comparatorResult = a.class.localeCompare(b.class);
            break;
          case 'section':
            comparatorResult = a.section.localeCompare(b.section);
            break;
          case 'subjects':
            comparatorResult = a.subjects.length - b.subjects.length;
            break;
          case 'marks':
            comparatorResult =
              a.marks.reduce((prev, curr) => prev + curr) / a.marks.length -
              b.marks.reduce((prev, curr) => prev + curr) / b.marks.length;
            break;
          default:
            comparatorResult = a.name.localeCompare(b.name);
            break;
        }
        return comparatorResult * (sort.direction == 'asc' ? 1 : -1);
      });
    };

    return sortFunction;
  }
  filterPendingTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.pendingDataSource.filter = filter.trim().toLowerCase();
  }


  startTest(event) {
    const dialogRef = this.dialog.open(StartTestDialogComponent, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
      data: {
        object: event
      },
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }


  getTestsByEmployee() {
    const currentDate = new Date();
    this.empService.getTestEmployees().then((res) => {
      this.isLoading = false;
      this.employeeTestList = res;
      const currentDate = new Date();
      this.pendingDataSource.data = this.employeeTestList.filter(x => x.status !== 'Completed' && this.covertUtcToLocalTime(x.dueDate) > currentDate).sort((a, b) => this.compare(a.dueDate, b.dueDate, true)).map((x, index)=> {x.testNo = index; return x});
      this.completedDataSource.data = this.employeeTestList.filter(x => x.status === 'Completed').sort((a, b) => this.compare(a.completedDate, b.completedDate, true)).map((x, index)=> {x.testNo = index; return x});
      setTimeout(() => {
        this.completedDataSource.sort = this.compSort;
        this.pendingDataSource.sort = this.pendSort;
        this.completedDataSource.paginator = this.completedDataPaginator;
        this.pendingDataSource.paginator = this.pendingDataPaginator;
        this.tabGroup.selectedIndex = 1;
      }, 1)
    }).finally(() => {
      this.isLoading = false;
    });
  }

  changeTab(index: number) {
    this.tabGroup.selectedIndex = index;
  }
  covertUtcToLocalTime(datetime: any): Date {
    //
    var startDateString = this.datePipe.transform(
      datetime,
      'yyyy-MM-dd hh:mm a'
    );
    const utcStartDateTime = new Date((startDateString?.toString()) + ' UTC');
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime?.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    //
    return newdatetime;
  }

}
