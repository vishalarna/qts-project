import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { LegacyPageEvent as PageEvent } from '@angular/material/legacy-paginator';
import { Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { OnlineCoursesService } from 'src/app/_Services/QTD/online-courses.service';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import { CBT_ScormRegistration } from '@models/CBT/CBT_ScormRegistration';

const ELEMENT_DATA_COMPLETED: any[] = [
  { ilaNo: 'QTS_037', ilaTitle: 'Power System Protection', courseRequirements: 'Pretest, Test', score: '94', grade: 'P' },
  { ilaNo: 'QTS_038', ilaTitle: 'QTD Train the Trainer', courseRequirements: 'Test', score: '95', grade: 'F' },
  { ilaNo: 'QTS_036', ilaTitle: 'Mitigate System Disturbances', courseRequirements: 'Pretest', score: '96', grade: 'P' },
  { ilaNo: 'QTS_039', ilaTitle: 'Power System Protection', courseRequirements: 'Pretest, Test', score: '83', grade: 'F' },
];
const ELEMENT_DATA_PENDING: any[] = [
  { ilaNo: 'QTS_036', ilaTitle: 'Mitigate System Disturbances', courseRequirements: 'Pretest', courseDueDate: '07-04-2023', status: 'Pending' },
  { ilaNo: 'QTS_037', ilaTitle: 'Power System Protection', courseRequirements: 'Pretest, Test', courseDueDate: '08-04-2023', status: 'In Progress' },
  { ilaNo: 'QTS_038', ilaTitle: 'QTD Train the Trainer', courseRequirements: 'Test', courseDueDate: '09-04-2023', status: 'Pending' },
  { ilaNo: 'QTS_039', ilaTitle: 'Power System Protection', courseRequirements: 'Pretest, Test', courseDueDate: '10-04-2023', status: 'In Progress' },
];

@Component({
  selector: 'app-online-courses-overview',
  templateUrl: './online-courses-overview.component.html',
  styleUrls: ['./online-courses-overview.component.scss']
})
export class OnlineCoursesOverviewComponent implements OnInit {
  url = 'Dashboard / Online Courses';
  completedDataSource = new MatTableDataSource<any>();
  pendingDataSource = new MatTableDataSource<any>();
  selectedSchedule?: any;
  isLoadingCompletedCourses = false;
  isLoadingPendingCourses = false;
  completedCoursesPage = 0;
  completedCoursesPageSize = 20;
  totalCompletedCourses = 0;
  pendingCoursesPage = 0;
  pendingCoursesPageSize = 20;
  totalPendingCourses = 0;

  displayedPendingColumns = [
    'ilaNo',
    'ilaTitle',
    'courseDueDate',
    'courseRequirements',
    'status',
    'action',
  ];

  displayedCompletedColumns = [
    'ilaNo',
    'ilaTitle',
    'score',
    'grade',
    'completionDate',
    'courseRequirements'
  ];

  constructor(
    public readonly dialog: MatDialog,
    private readonly onlineCourseService: OnlineCoursesService) 
  {
  }

  ngOnInit(): void {
    this.completedPageEvent({pageIndex: 0, pageSize: 20, length: 0});
    this.pendingPageEvent({pageIndex: 0, pageSize: 20, length: 0});
  }

  loadPendingCourses(orderBy:string) {
    if (orderBy.includes('CourseRequirements') || orderBy.includes('Status')) {
      // Client-side sorting for CourseRequirements and Status columns
      this.sortCoursesLocally(orderBy, this.pendingDataSource);
    }
    else {
      // Server-side sorting for other columns
      this.isLoadingPendingCourses = true;
      this.pendingDataSource.data = []; // make empty data in order render spinner in the table body

      this.onlineCourseService.getPendingCourses({
        orderBy: orderBy,
        page: this.pendingCoursesPage,
        pageSize: this.pendingCoursesPageSize,
      }).subscribe((result) => {
        if (result.isSuccess && result.data) {
          const pendingRecords = result.data.filter(record => {
            return !record.scormRegistrations.some(scormReg => scormReg.registrationSuccess === 2);
          });
          this.pendingDataSource.data = pendingRecords;
          this.totalPendingCourses = result.totalItems;
        }
        this.isLoadingPendingCourses = false;
      });
    }
  }

  pendingPageEvent(e: PageEvent ){
    this.pendingCoursesPage = e.pageIndex;
    this.pendingCoursesPageSize = e.pageSize;
    this.loadPendingCourses("")
  }
  pendingSortEvent(e: Sort ){
    let orderBy = '';
    orderBy = (e.direction == 'desc' ? `-${e.active}` : e.active) ?? '';
    this.loadPendingCourses(orderBy)
  }
  completedPageEvent(e: PageEvent ){
    this.completedCoursesPage = e.pageIndex;
    this.completedCoursesPageSize = e.pageSize;
    this.loadCompletedCourses("")
  }
  completedSortEvent(e: Sort ){
    let orderBy = '';
    orderBy = (e.direction == 'desc' ? `-${e.active}` : e.active) ?? '';
    this.loadCompletedCourses(orderBy)
  }

  loadCompletedCourses(orderBy:string) {

    if (orderBy.includes('CourseRequirements')) {
      // Client-side sorting for CourseRequirements 
      this.sortCoursesLocally(orderBy, this.completedDataSource);
    }
    else {
      // Server-side sorting for other columns
      this.isLoadingCompletedCourses = true;
      this.completedDataSource.data = [];

      this.onlineCourseService.getCompletedCourses({
        orderBy: orderBy,
        page: this.completedCoursesPage,
        pageSize: this.completedCoursesPageSize,
      }).subscribe((result) => {
        if (result.isSuccess && result.data) {
          this.completedDataSource.data = result.data;
          this.totalCompletedCourses = result.totalItems;
        }
  
        this.isLoadingCompletedCourses = false;
      });
    }
  }

  filterCompletedTest(e: Event) {
    const filter = (e.target as HTMLInputElement).value;
    this.completedDataSource.filter = filter.trim().toLowerCase();
  }

  convertUtcToLocalDate(val : Date) : Date {
    const d = new Date(val); // val is in UTC
    const localOffset = d.getTimezoneOffset() * 60000;
    const localTime = d.getTime() - localOffset;
    d.setTime(localTime);
    return d;
  }

  filterPendingTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.pendingDataSource.filter = filter.trim().toLowerCase();
  }

  startCourse(classSchedule?: ClassSchedules){
    // TODO

    // P.S If you need to show the dialog the below event handles that
  }


  startCoursedialogue(viewRef:any,schedule?: any) {
    this.selectedSchedule = schedule;
    const dialogRef = this.dialog.open(viewRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
      data: {
        object: schedule
      },
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  viewNotes(classSchedule?: ClassSchedules) {
    // TODO
  }

  reviewCourse(classSchedule?: ClassSchedules) {
    // TODO
  }

  private sortCoursesLocally(orderBy: string, dataSource: MatTableDataSource<any>) {
    let sortFunction: (a: any, b: any) => number | null = null;

    if (orderBy.includes('CourseRequirements')) {
      sortFunction = (a, b) => {
        let aValue = this.getSortableValueForCourseRequirements(a);
        let bValue = this.getSortableValueForCourseRequirements(b);
        return orderBy.startsWith('-') ? bValue - aValue : aValue - bValue;
      };
    }
    else if (orderBy.includes('Status')) {
      sortFunction = (a, b) => {
        let aValue = this.getSortableValueForStatus(a);
        let bValue = this.getSortableValueForStatus(b);
        return orderBy.startsWith('-') ? bValue - aValue : aValue - bValue;
      };
    }

    if (sortFunction) {
      dataSource.data = dataSource.data.sort(sortFunction);
    }
  }
  
  private getSortableValueForCourseRequirements(course: any): number {
    if (course?.usePreTestAndTest) {
      return 1; // Case for N/A
    }
    else {
      // If preTestRequired then local value is "PreTest, Test"
      return course?.preTestRequired ? 2 : 3;
    }
  }

  private getSortableValueForStatus(course: any): number {
    const hasLaunchLink = course.scormRegistrations?.some(reg => !!reg.launchLink);
    // Return 1 for "In Progress", 2 for "Pending"
    return hasLaunchLink ? 1 : 2;
  }

  hasLaunchLink(scormRegistrations: CBT_ScormRegistration[]): boolean {
    return scormRegistrations?.some(reg => !!reg.launchLink) ?? false;
  }
 
}
