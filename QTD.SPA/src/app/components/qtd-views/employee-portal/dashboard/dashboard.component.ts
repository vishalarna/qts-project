import {Component, OnInit, TemplateRef} from '@angular/core';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {Router} from '@angular/router';
import {Subject} from 'rxjs';
import {Store} from '@ngrx/store';
import {CalendarMonthViewBeforeRenderEvent} from 'angular-calendar';
import {EmployeeDashboardStats} from '@models/Employee';
import {EmpEvaluationVM} from 'src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM';
import {DashboardService} from 'src/app/_Services/QTD/Dashboard/dashboard.service';
import {EmpEvaluationService} from 'src/app/_Services/QTD/Employees/emp-evaluation.service';
import {EmployeesService} from 'src/app/_Services/QTD/employees.service';
import {StartTestDialogComponent} from '../../implementation/test/start-test-dialog/start-test-dialog.component';
import {
  StartPrcedureReviewDialogComponent
} from '../procedure-review/start-prcedure-review-dialog/start-prcedure-review-dialog.component';
import {ProceduresService} from 'src/app/_Services/QTD/procedures.service';
import {sideBarBackDrop, sideBarOpen} from 'src/app/_Statemanagement/action/state.menutoggle';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  url = 'Dashboard';
  selectedTab: 'detail' | 'dashboard' = 'dashboard';
  currentDate = new Date();
  selectedDay = new Date();
  stats?: EmployeeDashboardStats;
  eventsSubject = new Subject<any>();
  events?: any[];
  selectedRow!: EmpEvaluationVM;
  selectedTemplateRef!: TemplateRef<any>;
  isCourseAvailable = false;
  courseSpinner = false;
  daysInfo = ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'];
  empName = '';

  monthInfo = [
    {
      number: 0,
      title: 'January',
    },
    {
      number: 1,
      title: 'Feburary',
    },
    {
      number: 2,
      title: 'March',
    },
    {
      number: 3,
      title: 'April',
    },
    {
      number: 4,
      title: 'May',
    },
    {
      number: 5,
      title: 'June',
    },
    {
      number: 6,
      title: 'July',
    },
    {
      number: 7,
      title: 'August',
    },
    {
      number: 8,
      title: 'September',
    },
    {
      number: 9,
      title: 'October',
    },
    {
      number: 10,
      title: 'November',
    },
    {
      number: 11,
      title: 'December',
    }
  ]

  constructor(
    public readonly dialog: MatDialog,
    private readonly dashboardService: DashboardService,
    private readonly empService: EmployeesService,
    private readonly empEvalService: EmpEvaluationService,
    private readonly empTestService: EmployeesService,
    private readonly procedureService: ProceduresService,
    private readonly router: Router,
    private readonly store: Store)
  {
  }

  ngOnInit() {
    this.getCurrentEMPName();
    this.fetchStats();
    this.fetchEvents();
    this.readyIsCourseAvailable();
    this.store.dispatch(sideBarOpen());
    this.store.dispatch(sideBarBackDrop({backdrop: false}));
  }

  async getCurrentEMPName() {
    this.empName = await this.dashboardService.getCurrentUserName();
  }

  async readyIsCourseAvailable() {
    await this.getAvailableCourses();
  }

  async getAvailableCourses() {
    const currentDate = new Date().toUTCString();
    this.courseSpinner = false;
    await this.empService
      .getAvailableCourses(currentDate)
      .then((res) => {
        this.isCourseAvailable = res.length > 0;
      }).finally(() => {
        this.courseSpinner = false;
      });
  }

  isSameMonth(date: Date): boolean {
    return date.getUTCMonth() === this.selectedDay.getUTCMonth() &&
      date.getUTCFullYear() === this.selectedDay.getUTCFullYear();
  }

  isSameDay(date1: Date, date2: Date): boolean {
    return date1.getUTCDate() === date2.getUTCDate() &&
      date1.getUTCMonth() === date2.getUTCMonth() &&
      date1.getUTCFullYear() === date2.getUTCFullYear();
  }

  testCellClick(data: any) {
    this.events = null;
    this.selectedDay = data.date;
    this.currentDate = data.date;
    const currDate = new Date();
    this.setHoursMinutesSeconds(currDate.getHours(), currDate.getMinutes(), currDate.getSeconds());
    this.fetchEvents();
  }

  previousAndNextClick(type:string){
    const currentMonth = this.selectedDay.getMonth();
    this.selectedDay.setDate(1);

    if (type == 'previous') {
      this.selectedDay.setMonth(currentMonth - 1);
      this.selectedDay = new Date(this.selectedDay);
    }
    else {
      this.selectedDay.setMonth(currentMonth + 1);
      this.selectedDay = new Date(this.selectedDay);
    }
  }

  setHoursMinutesSeconds(hours: number, minutes: number, seconds: number) {
    this.selectedDay.setHours(hours);
    this.selectedDay.setMinutes(minutes);
    this.selectedDay.setSeconds(seconds);
  }

  fetchStats() {
    this.dashboardService.getStats().subscribe((result) => {
      if (result.isSuccess && result.data) {
        this.stats = result.data;
      }
    });
  }

  fetchEvents() {
    const date = this.selectedDay.toUTCString();
    this.dashboardService.getEventsForDate(date).subscribe((events) => {
      this.events = events;
      this.eventsSubject.next(this.events);
    });
  }

  async start(data: any | null) {
    if (data && data.type === 'Evaluation') {
      const evals = await this.empEvalService.getAllEvaluations();
      this.selectedRow = evals.filter(x => x.evaluationId === data.id && x.classScheduleId == data.parentId)[0]
      const dialogRef = this.dialog.open(this.selectedTemplateRef, {
        width: '900px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }

    if (data && data.type === 'Test') {
      const tests: any = await this.empTestService.getTestEmployees();
      const selectedTest = tests.filter((x) => x.classScheduleId === data.parentId && x.testId === data.id)[0];
      const dialogRef = this.dialog.open(StartTestDialogComponent, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
        data: {
          object: selectedTest
        },
      });
    }

    if (data && data.type === 'Procedure Review') {
      const procs = await this.procedureService.getProcedureReviewEmpSide();
      const selectedProc = procs.filter((x) => x.procedureReviewId === data.id && x.procedureId === data.parentId)[0];
      const dialogRef = this.dialog.open(StartPrcedureReviewDialogComponent, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
        data: {
          object: selectedProc
        },
      });
    }
  }

  navigateToSelfReg() {
    this.router.navigate(['/emp/self-registration']);
  }

  changeMonth(month: number) {
    const tempSelectedDay = new Date(this.selectedDay);
    this.currentDate.setDate(1);
    this.currentDate.setMonth(month);
    tempSelectedDay.setDate(1);
    tempSelectedDay.setMonth(month);
    this.selectedDay = tempSelectedDay;
    this.testCellClick({date: this.currentDate});
  }

  calendarBeforeViewRender(e: CalendarMonthViewBeforeRenderEvent) {
    e.body.forEach(day => {
      if (this.eventExistsOnDay(day.date)) {
        day.cssClass = 'highlight-event-date';
      }
    });
  }

  private eventExistsOnDay(date: Date): boolean {
    if (!this.events) {
      return false;
    }

    return this.events.some((event) => {
      const dueDate = new Date(event.dueDate);
      return this.isSameDay(dueDate, date);
    });
  }
}
