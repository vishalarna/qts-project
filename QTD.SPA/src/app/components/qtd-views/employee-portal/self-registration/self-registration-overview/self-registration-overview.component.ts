import { animate, state, style, transition, trigger } from '@angular/animations';
import { DatePipe } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode } from 'src/app/_Statemanagement/action/state.menutoggle';
export interface PeriodicElement {
  ila: string;
  title: string;
  classDetail: string;
  location: string;
  instructor: string;
  status: string;

}
const ELEMENT_DATA: PeriodicElement[] = [
  { ila: '33', title: 'ewew', classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', status: '1' },
  { ila: '33', title: 'ewew', classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', status: '1' },
  { ila: '33', title: 'ewew', classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', status: '1' },
  { ila: '33', title: 'ewew', classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', status: '1' }

];
const ELEMENT_DATA1: any[] = [
  { number: '33', ilaTitle: 'ewew' }

];

const ELEMENT_DATA3: any[] = [
  { classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', seatAvailable: '1' },
  { classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', seatAvailable: '1' },
  { classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', seatAvailable: '1' },
  { classDetail: 'fsdfds', instructor: 'fdfdsf', location: 'fdf', seatAvailable: '1' }

];
@Component({
  selector: 'app-self-registration-overview',
  templateUrl: './self-registration-overview.component.html',
  styleUrls: ['./self-registration-overview.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class SelfRegistrationOverviewComponent implements OnInit {
  currentTab: any;
  url = "Dashboard / Self Registration";
  selectedIndex = 1;
  ApprovedCount = 0;
  AvailableCount = 0;
  DroppedCount = 0;
  DeniedCount = 0;
  todaysDate: any;
  modelDescription: string;
  RegistrationApproval: string;
  ilaNum: any
  ilaTitle: any
  @ViewChild('outerSort', { static: false }) outerSort: MatSort;
  @ViewChild('deniedSort', { static: false }) deniedSort: MatSort;
  @ViewChild('droppedSort', { static: true }) droppedSort: MatSort;
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChildren('innerSort') innerSort: QueryList<MatSort>;
  // @ViewChildren('innerTables') innerTables: QueryList<MatTable<Address>>;
  displayedColumnsApproved = ["ilaNum", "ilaTitle", "classDetail", "location", "instructor", "status", "action"];

  columnsToDisplay = ["expand", 'provider','ilaNum','ilaTitle', 'totalCourses'];
  innerDisplayedColumns2 = ['ilaNum', 'ilaTitle'];
  innerDisplayedColumns = ['classStartDateTime', 'location', 'instructor', 'seatsAvailable', 'action'];
  displayedColumnsAvailable = ["expand", "providerTitle", "totalCourses"];
  displayedExpandedColumns: string[] = [
    'classDetail',
    'location',
    'instructor',
    'seatAvailable',
    'action',
  ];
  displayedColumnsDropped = ["ila", "title", "classDetail", "location", "instructor", "status", "action"];
  displayedColumnsDenied = ["ilaNum", "ilaTitle", "classStartDateTime", "location", "instructor", "status", "action"];

  private dataSourceSubscription: Subscription;
  ApprovedCourses = new MatTableDataSource<any>();
  AvailableCourses = new MatTableDataSource<any>();
  DroppedCourses = new MatTableDataSource<any>();
  DeniedCourses = new MatTableDataSource<any>();
  dataSource = new MatTableDataSource<any>();
  unlinkDescription: string;
  joinListDescription: string = "You are going to Join Wait List.";
  currentDate: any;
  classInfo: any;
  iLAId: any;
  expandedElement: User | null;
  usersData: User[] = [];
  datePipe = new DatePipe('en-us');
  isLoading: boolean = true;
  @ViewChild('approvedCoursesPage') approvedCoursesPaginator: MatPaginator;
  @ViewChild('availableCoursesPage') availableCoursesPaginator: MatPaginator;
  @ViewChild('droppedCoursesPage') droppedCoursesPaginator: MatPaginator;
  @ViewChild('deniedCoursesPage') deniedCoursesPaginator: MatPaginator;
  constructor(private store: Store<{ toggle: string }>,
    private _router: Router,
    private _empService: EmployeesService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog,
    private alert: SweetAlertService,
  ) { }


  ngOnInit(): void {

    
    //this.DroppedCourses.data = ELEMENT_DATA;
    //this.DeniedCourses.data = ELEMENT_DATA;
    //this.AvailableCourses.data = ELEMENT_DATA1;
    var classStartDateTime = "2023-07-19T01:49:00";
    var localDate = this.covertUtcToLocalTime(classStartDateTime)
    this.todaysDate = new Date();
    // this.todaysDate = this.datePipe.transform(
    //   Date.now(),
    //    'yyyy-MM-dd'
    //  );
    var checkDate = this.areDatesEqualWithoutTime(localDate, new Date())

    
    //this.todaysDate = this.covertUtcToLocalTime(this.todaysDate);
    this.getAvailableCourses();
    this.dataSource.data = this.usersData;
    this.dataSourceSubscription = this.dataSource.connect().subscribe();
    
    // this.dataSource.sort = this.outerSort;
    this.DroppedCourses.sort = this.droppedSort;

  }

  expandedTableSource = new MatTableDataSource<any>();

  toggleRow(element: any, index: any) {
    
    (this.expandedElement = this.expandedElement?.empSelfregistrationEmployees[0]?.ilaId === element?.empSelfregistrationEmployees[0]?.ilaId ? null : element);
    if (this.expandedElement) {
      this.expandedTableSource.data = element?.empSelfregistrationEmployees;

      // setTimeout(() => {
      //   this.expandedTableSource.sort = this.innerSort.get(index);
      //   // this.dataSource.sort = this.outerSort;
      //   this.cd.detectChanges();
      // }, 1);
    }
    else {
      this.expandedTableSource.data = [];
      // this.dataSource.sort = this.outerSort;
    }
    //this.cd.detectChanges();
    // 
    // 
    // 
    // this.dataSource.sort = this.outerSort;
    // this.cd.detectChanges();
    // this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Address>).sort = this.innerSort.toArray()[index]);
  }
  filterApproved(event: any) {
    
    this.ApprovedCourses.filter = String(event.target.value).trim().toLowerCase();
  }

  filterAvailable(event: any) {
    
    this.dataSource.filter = String(event.target.value).trim().toLowerCase();
  }
  filterDropped(event: any) {
    this.DroppedCourses.filter = String(event.target.value).trim().toLowerCase();
  }
  filterDenied(event: any) {
    this.DeniedCourses.filter = String(event.target.value).trim().toLowerCase();
  }

  courseViewDetail(event) {
    
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this._router.navigate(['/emp/course-detail', event.classId, event.ilaId]);
  }


  modelConformation($event: any, name: string) {
    if (name === 'register') {
      this.registerAvailableCourses(this.classInfo);
    } else if (name === 'drop') {
      this.dropCourses(this.classInfo);
    } else if (name === 'waitList') {
      this.joinwaitlist(this.classInfo);
    }

  }
  onTabChanged($event) {
    
    this.currentTab = $event.index;
    let clickedIndex = $event.index;
    if (clickedIndex === 0) {
      this.isLoading = true;
      this.getApprovedCourses();
    }
    else if (clickedIndex === 1) {
      this.isLoading = true;
      this.getAvailableCourses();
    }
    else if (clickedIndex === 2) {
      this.isLoading = true;
      this.getDroppedCourses();
    }
    else if (clickedIndex === 3) {
      this.isLoading = true;
      this.getDeniedCourses();
    }

  }

  modelDialog(templateRef, event) {

    
    this.ilaNum = event.ilaNum;
    this.ilaTitle = event.ilaTitle;
    this.unlinkDescription = `You have successfully dropped registration for ${this.ilaNum} and ${this.ilaTitle}`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    this.classInfo = event;
  }

  modelDialogRegister(templateRef, event, name) {
    
    this.ilaNum = event.ilaNum;
    this.ilaTitle = event.ilaTitle;
    if (name === 'Approval') {
      if (event.acknolwedgement !== null) {
        var text = event.acknolwedgement;
        this.modelDescription = `Your request has been sent to the Administrator for Approval` + `\n ${text}`;
      }
      else {
        this.modelDescription = `Your request has been sent to the Administrator for Approval` + `\n`;
      }

      this.RegistrationApproval = `Registration sent for Approval`;
    }
    else {
      if (event.acknolwedgement !== null) {
        var text = event.acknolwedgement;
        if (this.ilaNum === null) {
          this.modelDescription = `You have successfully registered for  ${this.ilaTitle}` + `\n ${text}`;
        }
        else {
          this.modelDescription = `You have successfully registered for ${this.ilaNum} and ${this.ilaTitle}` + `\n ${text}`;
        }
      }

      else {
        if (this.ilaNum === null) {
          this.modelDescription = `You have successfully registered for  ${this.ilaTitle}` + `\n`;
        }
        else {
          this.modelDescription = `You have successfully registered for ${this.ilaNum} and ${this.ilaTitle}` + `\n`;
        }

      }

      this.RegistrationApproval = `Course Registration`;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    this.classInfo = event;
  }

  async registerAvailableCourses(event) {
    
    await this._empService
      .registerAvailableCourses(event.classId, event.ilaId)
      .then((res) => {
        
        this.alert.successToast(`Course is Successfully registered! `, true);
        this.getAvailableCourses();
        this.getDroppedCourses();

      }).catch((res: any) => {
        
        this.alert.errorToast(res);
      });;
  }

  async dropCourses(event) {
    
    await this._empService
      .dropCourses(event.classId, event.ilaId)
      .then((res) => {
        
        this.getApprovedCourses();
        this.getDroppedCourses();
        this.alert.successToast(`Course is Successfully dropped! `, true);
      }).catch((res: any) => {
        
        this.alert.errorToast(res);
      });;
  }

  async joinwaitlist(event) {
    

    
    await this._empService
      .joinWaitList(event.classId, event.ilaId)
      .then((res) => {
        
        if (this.currentTab === 2) {
          this.getDroppedCourses();
        }
        else {
          this.getAvailableCourses();
        }
        this.alert.successToast(`Your request to join the Waiting List has been sent to Administrator for approval`, true);
      }).catch((res: any) => {
        
        this.alert.errorToast(res);
      });;
  }

  async getAvailableCourses() {
    var currentDate = new Date();
    this.usersData = [];
    this.currentDate = currentDate.toUTCString();
    // this.dataSource.data = [];
    this.expandedElement =null;
    this.isLoading = true;
    await this._empService
      .getAvailableCourses(this.currentDate)
      .then((res) => {
        
        if (res) {
          
          
          //this.dataSource.data = res.filter(x=>x.empSelfregistrationEmployees.length>0);
          this.AvailableCount = res.filter(x => x.empSelfregistrationEmployees.length > 0).length;
          this.isLoading = true;

          
          res.forEach((user) => {
            if (user.empSelfregistrationEmployees && Array.isArray(user.empSelfregistrationEmployees) && user.empSelfregistrationEmployees.length) {

              // var currentDateLocal = new Date();
              // var currentDateLocalstr = this.datePipe.transform(
              //   currentDateLocal,
              //   'yyyy-MM-dd hh:mm a'
              // );
              // 
              //  for (let i = 0; i < user.empSelfregistrationEmployees.length; i++)
              // {

              // var endDateString = this.datePipe.transform(
              //   user.empSelfregistrationEmployees[i].classEndDateTime,
              //   'yyyy-MM-dd hh:mm a'
              // );
              // var endDateLocal = new Date(endDateString)
              // endDateLocal = this.covertUtcToLocalTime(endDateLocal);
              // var endDatestr = this.datePipe.transform(
              //   endDateLocal,
              //   'yyyy-MM-dd hh:mm a'
              // );
              // 
              // //user.empSelfregistrationEmployees[i].classEndDateTime = new Date(Date.parse(localendDateTimeString));
              // 
              // if(endDatestr <= currentDateLocalstr)
              // {
              //   user.empSelfregistrationEmployees.push(user.empSelfregistrationEmployees[i])
              // }


              // }
              

              this.usersData = [...this.usersData, { ...user, empSelfregistrationEmployees: user.empSelfregistrationEmployees }];
              
            } else {
              this.usersData = [...this.usersData, user];
            }
          });
          this.dataSource.data = this.usersData;
          this.dataSource.data = this.dataSource.data.filter(x => x.empSelfregistrationEmployees.length > 0);
          // setTimeout(() => {
          //   if (this.outerSort) {
          //     
          //     // this.dataSource.sort = this.outerSort;
          //     // this.innerTables.forEach((table, index) => (table.dataSource as MatTableDataSource<Address>).sort = this.innerSort.toArray()[index]);
          //   }
          // }, 1)
          this.isLoading = false;
          this.dataSource.paginator = this.availableCoursesPaginator
        }
      }).finally(()=>{
        this.isLoading = false;
      });
    
  }

  async getApprovedCourses() {
    
    await this._empService
      .getApprovedCourses()
      .then((res) => {
        
        if (res) {
          
          
          this.ApprovedCourses.data = res;
          this.ApprovedCount = res.length;
          this.isLoading = false;
          this.ApprovedCourses.paginator = this.approvedCoursesPaginator;
        }
      });
  }


  async getDroppedCourses() {
    
    await this._empService
      .getDroppedCourses()
      .then((res) => {
        
        if (res) {
          
          
          this.DroppedCourses.data = res;
          this.DroppedCount = res.length;
          this.isLoading = false;
          this.DroppedCourses.paginator = this.droppedCoursesPaginator;
        }
      }).catch((res: any) => {
        
        this.alert.errorToast(res);
      });
  }
  async getDeniedCourses() {
    
    await this._empService
      .getDeniedCourses()
      .then((res) => {
        
        if (res) {
          
          
          this.DeniedCourses.data = res;
          this.DeniedCount = res.length;
          this.isLoading = false;
          this.DeniedCourses.paginator = this.deniedCoursesPaginator;

          setTimeout(() => {
            if (this.deniedSort) {
              this.DeniedCourses.sort = this.deniedSort;
            }
          }, 1)
        }
      });
  }
  covertUtcToLocalTime(datetime: any): Date {

    var startDateString = this.datePipe.transform(
      datetime,
      'yyyy-MM-dd hh:mm a'
    );
    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    //
    return newdatetime;
  }

  areDatesEqualWithoutTime(date1: Date, date2: Date): boolean {

    
    const year1: number = date1.getFullYear();
    const month1: number = date1.getMonth();
    const day1: number = date1.getDate();

    // Extract year, month, and day components from date2
    const year2: number = date2.getFullYear();
    const month2: number = date2.getMonth();
    const day2: number = date2.getDate();

    // Compare the dates
    if (year1 === year2 && month1 === month2 && day1 === day2) {
      return true
    }
    return false;
  }
  areDatesGreaterWithoutTime(date1: Date, date2: Date): boolean {

    // Compare the dates
    if (date1 > date2) {
      return true
    }
    return false;
  }
  areDatesLessWithoutTime(date1: Date, date2: Date): boolean {

    // Compare the dates
    if (date1 < date2) {
      return true
    }
    return false;
  }

  sortApprovedCoursesData(sort: Sort) {
    this.ApprovedCourses.sort = this.tblSort;
    const data = this.ApprovedCourses.data;
    if (!sort.active || sort.direction === '') {
      this.ApprovedCourses.data = data;
      return;
    }

    this.ApprovedCourses.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'ilaNum':
          return this.compare(a.ilaNum, b.ilaNum, isAsc);
        case 'ilaTitle':
          return this.compare(a.ilaTitle, b.ilaTitle, isAsc);
        case 'classDetail':
          return this.compare(a.classStartDateTime, b.classStartDateTime, isAsc);
        case 'location':
          return this.compare(a.location, b.location, isAsc);
        case 'instructor':
          return this.compare(a.instructor, b.instructor, isAsc);
        case 'status':
          return this.compare(a.status, b.status, isAsc);
        default:
          return 0;
      }
    });
  }

  sortDroppedCoursesData(sort: Sort) {
    this.DroppedCourses.sort = this.tblSort;
    const data = this.DroppedCourses.data;
    if (!sort.active || sort.direction === '') {
      this.DroppedCourses.data = data;
      return;
    }

    this.DroppedCourses.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'ilaNum':
          return this.compare(a.ilaNum, b.ilaNum, isAsc);
        case 'ilaTitle':
          return this.compare(a.ilaTitle, b.ilaTitle, isAsc);
        case 'classDetail':
          return this.compare(a.classStartDateTime, b.classStartDateTime, isAsc);
        case 'location':
          return this.compare(a.location, b.location, isAsc);
        case 'instructor':
          return this.compare(a.instructor, b.instructor, isAsc);
        case 'status':
          return this.compare(a.status, b.status, isAsc);
        default:
          return 0;
      }
    });
  }


  sortDeniedCoursesData(sort: Sort) {
    this.DeniedCourses.sort = this.deniedSort;
    // const data = this.DeniedCourses.data;
    // if (!sort.active || sort.direction === '') {
    //   this.DeniedCourses.data = data;
    //   return;
    // }

    // this.DeniedCourses.data = data.sort((a, b) => {
    //   const isAsc = sort.direction === 'asc';
    //   switch (sort.active) {
    //     case 'ilaNum':
    //       return this.compare(a.ilaNum, b.ilaNum, isAsc);
    //     case 'ilaTitle':
    //       return this.compare(a.ilaTitle, b.ilaTitle, isAsc);
    //     case 'classDetail':
    //       return this.compare(a.classStartDateTime, b.classStartDateTime, isAsc);
    //       case 'location':
    //         return this.compare(a.location, b.location, isAsc);
    //       case 'instructor':
    //         return this.compare(a.instructor, b.instructor, isAsc);
    //         case 'status':
    //         return this.compare(a.status, b.status, isAsc);
    //     default:
    //       return 0;
    //   }
    // });
  }
  sortAvailbleCoursesData(sort: Sort,isInner:boolean) {
    // this.dataSource.sort = this.outerSort;
    const data = this.dataSource.data;
    if (!sort.active || sort.direction === '') {
      this.dataSource.data = data;
      return;
    }

    if(!isInner){
      this.dataSource.data = data.sort((a, b) => {
        const isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'provider':
            return this.compare(a.provider, b.provider, isAsc);
          case 'totalCourses':
            return this.compare(a.totalCourses, b.totalCourses, isAsc);
          case 'ilaNum':
            return this.compare(a.ilaNum, b.ilaNum, isAsc);
          case 'ilaTitle':
            return this.compare(a.ilaTitle, b.ilaTitle, isAsc);

          case 'classStartDateTime':
            return this.compare(a.classStartDateTime, b.classStartDateTime, isAsc);

          case 'location':
            return this.compare(a.location, b.location, isAsc);

          case 'instructor':
            return this.compare(a.instructor, b.instructor, isAsc);
          case 'seatsAvailable':
            return this.compare(a.seatsAvailable, b.seatsAvailable, isAsc);
          default:
            return 0;
        }
      });
    }

    if (this.expandedElement !== null && this.expandedTableSource.data.length > 0 && isInner) {
      this.expandedTableSource.data = this.expandedTableSource.data.sort((a, b) => {
        const isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'classStartDateTime':
            return this.compare(a.classStartDateTime, b.classStartDateTime, isAsc);

          case 'location':
            return this.compare(a.location, b.location, isAsc);

          case 'instructor':
            return this.compare(a.instructor, b.instructor, isAsc);
          case 'seatsAvailable':
            return this.compare(a.seatsAvailable, b.seatsAvailable, isAsc);
          default:
            return 0;
        }
      })
    }
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  matchDate(data: any): boolean {
    
    var fromUTC = this.covertUtcToLocalTime(data.classStartDateTime);
    var startDate = new Date(fromUTC);
    var currDate = new Date();
    if (startDate >= currDate) {
      return true;
    }
    return false;
  }
}

export interface User {
  expand: string;
  provider: string;
  totalCourses: string;
  empSelfregistrationEmployees?: Address[] | MatTableDataSource<Address>;
}


export interface Address {
  classStartDateTime: Date;
  classEndDateTime: Date;
  classDetail: string;
  location: string;
  instructor: string;
  seatsAvailable: any;
  action: string;
  ilaTitle: string;
  ilaNum: string;
}

