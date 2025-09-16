import { freezeMenu, sideBarOpen } from './../../../../../_Statemanagement/action/state.menutoggle';
import {
  Component,
  OnDestroy,
  OnInit,
  TemplateRef,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarMonthViewComponent,
  CalendarView,
} from 'angular-calendar';
import {
  addDays,
  addHours,
  endOfMonth,
  endOfWeek,
  format,
  isSameDay,
  isSameMonth,
  lastDayOfMonth,
  startOfDay,
  startOfWeek,
  subDays,
} from 'date-fns';
import { Subject, Subscription } from 'rxjs';
import {
  sideBarBackDrop,
  sideBarClose,
  sideBarDisableClose,
} from 'src/app/_Statemanagement/action/state.menutoggle';
import { EventColor } from 'calendar-utils';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ScheduleClassHistoryVM } from 'src/app/_DtoModels/SchedulesClassses/ScheduleClassHistory/ScheduleClassHistoryVM';
import { ScheduleClassesStats } from 'src/app/_DtoModels/SchedulesClassses/ScheduleClassesStats';
import { getWeekYearWithOptions } from 'date-fns/fp';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { threadId } from 'worker_threads';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { Instructor } from 'src/app/_DtoModels/Instructors/Instructor';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ReportExportOptions, ReportExportType } from '@models/Report/ReportExportOptions';
import { ReportSkeleton } from '@models/ReportSkeleton/ReportSkeleton';
import { ReportUpdateOptions } from '@models/Report/ReportUpdateOptions';
import { ReportSkeletonColumn } from '@models/ReportSkeleton/ReportSkeletonColumn';
import { HttpResponse } from '@angular/common/http';
import { ReportFilterOption } from '@models/Report/ReportFilterOption';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';

export interface Schedule {
  startDate: string | undefined;
  endDate: string;
  location: string;
  instructor: string;
  ila: string;
  description: string;
  modifyBy: string;
  modiftDate: string;
  classScheduleId?: string;
  instructorName?: string;
  employeeName?: string;
  ilaName?: string;
}
const ELEMENT_DATA: Schedule[] = [
  {
    startDate: '04-02-22',
    endDate: '07-02-22',
    location: 'Conference Room',
    instructor: 'John Smith',
    ila: 'OJT_Train_The_Trainer',
    description: '07-02-22',
    modifyBy: 'Daniela Petrovic',
    modiftDate: '04-02-22',
  },
  {
    startDate: '04-03-22',
    endDate: '07-03-22',
    location: 'Training Hall',
    instructor: 'Stephnie Lynn',
    ila: 'OJT_Train_The_Trainer',
    description: '07-02-22',
    modifyBy: 'Daniela Petrovic',
    modiftDate: '04-02-22',
  },
];

export interface LatestActivity {
  ila: string;
  description: string;
  modifyBy: string;
  modiftDate: string;
}
const Activity_DATA: LatestActivity[] = [
  {
    ila: 'OJT_Train_The_Trainer',
    description: '07-02-22',
    modifyBy: 'Daniela Petrovic',
    modiftDate: '04-02-22',
  },
  {
    ila: 'OJT_Train_The_Trainer',
    description: '07-02-22',
    modifyBy: 'Stephnie Lynn',
    modiftDate: '07-02-22',
  },
  {
    ila: 'OJT_Train_The_Trainer',
    description: '07-02-22',
    modifyBy: 'Stephnie Lynn',
    modiftDate: '07-02-22',
  },
];
const colors: Record<string, EventColor> = {
  yellow: {
    primary: 'rgb(92 155 49)',
    secondary: '#f8faf7b8',
  },
};
@Component({
  selector: 'app-scheduling-classes-overview',
  templateUrl: './scheduling-classes-overview.component.html',
  styleUrls: ['./scheduling-classes-overview.component.scss'],
})
export class SchedulingClassesOverviewComponent implements OnInit, OnDestroy {
  viewSelected:'ILA'|'Calender' = 'ILA';
  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  view: CalendarView = CalendarView.Day;
  activeDayIsOpen: boolean = true;
  refresh = new Subject<void>();
  startDateTime: string;
  endDateTime: string;
  isLoading: boolean = true;
  isDownloadingReport: boolean = false;
  isIncludeInactiveILAs : boolean = false;
  url: string = 'Implementation / Scheduling Classes and Roster';
  dataSource = new MatTableDataSource<any>();
  dataSourceLatestActivity = new MatTableDataSource<ScheduleClassHistoryVM>();
  events: any[] = [];
  scheduleList: Schedule[] = [];
  range: number[] = [];
  yearnow = new Date().getFullYear();
  myNavBarState = '';
  @ViewChild('eoPaging', { static: false }) eoPaging!: MatPaginator;
  datePipe = new DatePipe('en-us');
  displayedColumns: string[] = [
    'startDateTime',
    'endDateTime',
    'location',
    'instructor',
    'employeeCount',
    'action',
  ];
  getyearsdropdown() {
    var i = 0;
    while (this.range[this.range.length - 1] !== 2005) {
      this.range.push((this.yearnow + 4) - i);
      i++;
    }
  }
  provider_list: Provider[] = [];
  provider_list_original: Provider[] = [];
  ila_list: any[] = [];
  ila_list_original: any[] = [];
  isSpinner: boolean = true;
  isILALoading: boolean = false;
  noILAFound: boolean = false;
  noProviderFound: boolean = false;
  isOpen = false;
  displayedColumnsLatest: string[] = ['ilaName', 'activityDesc', 'createdDate'];
  ilaFilterForm: UntypedFormGroup = new UntypedFormGroup({
    providerId: new UntypedFormControl(''),
    ilaId: new UntypedFormControl(''),
    searchTxt: new UntypedFormControl(''),
    ilaSearch: new UntypedFormControl(''),
    selectedYear: new UntypedFormControl([this.yearnow]),
  });
  deleteChecks: any = {};
  stats!: ScheduleClassesStats;
  deleteDescription: string;
  scId: any;
  eventrefresh = new Subject<void>();
  @ViewChild('ilaPage') set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild('matSortActivity', { static: false }) matSortActivity!: MatSort

  @ViewChild('provSelect', { static: false }) provSelect!: MatSelect;
  @ViewChild('ilaSelect', { static: false }) ilaSelect!: MatSelect;
  insName:any;
  locName:any;
  topicName:any;
  ilaName : any;
  showCloseButtonAndFilter:any= false;
  filterBy:any;
  reportSkeleton: ReportSkeleton;
  reportSkeletonName: string;
  reportCreateorUpdate:ReportUpdateOptions;
  displayColumns:ReportSkeletonColumn[];
  constructor(
    private store: Store<{ toggle: string }>,
    private _router: Router,
    public dialog: MatDialog,
    public flyPanelSrvc: FlyInPanelService,
    public vcf: ViewContainerRef,
    private trainingSevc: TrainingService,
    private providerSrvc: ProviderService,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe,
    private apireportService: ApiReportsService
  ) { }
  ngOnDestroy(): void {
    localStorage.removeItem("Instructor")
    localStorage.removeItem("Topic");
    localStorage.removeItem("Location");
    localStorage.removeItem("ILA");
    if (this.rosterSubscription) this.rosterSubscription.unsubscribe();
    if (this.signInSubscription) this.signInSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    // this.dataSource.data = ELEMENT_DATA;
    this.readyLatestActivity();
    //this.getAllTrainingSchedule();
    // Used to apply multiple filter options in single go. Nested with &&.
    this.dataSource.filterPredicate = (row: any, filter: string) => {
      const filterObject = JSON.parse(filter);
      return (
        (row.providerID?.toString().trim().includes(filterObject.providerID) &&
          row.ilaid?.toString().trim().includes(filterObject.ilaid)) ||
        row.instructorId
          ?.toString()
          .trim()
          .includes(filterObject.instructorId) ||
        row.locationId?.toString().trim().includes(filterObject.locationId) ||
        row.topicIds.includes(filterObject.topicId)
      );
    };
    this.getyearsdropdown();

    var options: CalendarViewSession = JSON.parse(localStorage.getItem('calendarSession'));
    if (options !== null && options !== undefined) {

      this.viewSelected = options.viewSelected;
      this.ToggleClassesGroupBy(this.viewSelected);
      this.view = options.view;
      this.startDateTime = options.startDateTime?.toString();
      this.endDateTime = options.endDateTime?.toString();
      this.viewDate = new Date(options.viewDate?.toString());
      this.setView(options.view);
      if (options.providerId !== null && options.providerId !== undefined && options.providerId !== "") {
        this.ilaFilterForm.get('providerId').setValue(options.providerId);
        this.selectProvider(options.providerId);
      }
      if (options.ilaId !== null && options.ilaId !== undefined && options.ilaId !== "") {
        this.ilaFilterForm.get('ilaId').setValue(options.ilaId);
        this.filterData();
      }

      if(options.filterYears !== null && options.filterYears !== undefined){
        this.ilaFilterForm.get('selectedYear')?.setValue(options.filterYears);
        this.ilaFilterForm.updateValueAndValidity();
      }

      this.ilaFilterForm.updateValueAndValidity();


      localStorage.removeItem('calendarSession');
    }
    else if(this.viewSelected !== 'ILA'){
      this.setView(this.view);
    }
    else{
      this.isLoading = false;
    }
    this.ToggleClassesGroupBy(this.viewSelected);
  }
  activityLoader = false;
  OrignalClassSchedulesListIlaView: any[];
  async readyLatestActivity() {
    this.activityLoader = true;
    var data = await this.trainingSevc.getAllHistory();
    this.dataSourceLatestActivity.data = data;
    this.activityLoader = false;

    this.stats = await this.trainingSevc.GetStats();

    setTimeout(() => {
      this.dataSourceLatestActivity.sort = this.matSortActivity;
      this.dataSourceLatestActivity.paginator = this.eoPaging;
    }, 1);
  }
  sortActivityData(sort: Sort) {
    this.dataSourceLatestActivity.sort = this.tblSort;
    const data = this.dataSourceLatestActivity.data;
    if (!sort.active || sort.direction === '') {
      this.dataSourceLatestActivity.data = data;
      return;
    }

    this.dataSourceLatestActivity.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'ilaName':
          return this.compare(a.ilaName, b.ilaName, isAsc);
        case 'activityDesc':
          return this.compare(a.activityDesc, b.activityDesc, isAsc);
        case 'createdDate':
          return this.compare(a.createdBy, b.createdBy, isAsc);
        default:
          return 0;
      }
    });
  }
  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  async getAllTrainingSchedule() {

    var startDate = new Date(this.startDateTime);
    this.startDateTime = startDate.toUTCString();
    var endDate = new Date(this.endDateTime);
    this.endDateTime = endDate.toUTCString();
    const data = await this.trainingSevc.getAll(
      this.startDateTime,
      this.endDateTime
    );

    // .then((res) => {
    //

    this.dataSource.data = data;
    this.events = [];
    this.dataSource.data.forEach((eo) => {

      var startDateString = this.datePipe.transform(
        eo.startDateTime,
        'yyyy-MM-dd hh:mm a'
      );

      const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
      // Convert UTC date and time to local time
      const localstartDateTimeString = utcStartDateTime.toLocaleString();
      eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
      var endDateString = this.datePipe.transform(
        eo.endDateTime,
        'yyyy-MM-dd hh:mm a'
      );
      //const utcendtDateTimeString = res.startDateTime.toDateString();
      const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
      // Convert UTC date and time to local time
      const localendDateTimeString = utcendtDateTime.toLocaleString();
      eo.endDateTime = new Date(Date.parse(localendDateTimeString));
      this.deleteChecks[eo.id] = eo.canDelete;
      this.events.push({
        start: eo.startDateTime,
        end: eo.endDateTime,
        provider:eo.provider,
        ila:eo.ila,
        location:eo.location,
        instructor:eo.instructor,
        title: `<div class="flex flex-col">
            <span class="font-bold breakText"> ${eo.provider} ${eo.ila}</span>
            <span class="nonGreen breakText"> ${eo.location} by ${eo.instructor}</span>
            </div>`,
        color: { ...colors.yellow },
        id: eo.id,
        resizable: {
          beforeStart: true,
          afterEnd: true,
        },
      });
      //eo.topicIds =Array.from(new Set(eo.topicLinks.map(x=>x.ilaTopicId)));
      //this.dataSource.data = res;
    });

    this.isLoading = false;
    //check localStorageItem

    var insId = localStorage.getItem("Instructor");

    if(insId !== null && insId !== undefined)
   {
   this.filterbyInstructor(insId)
   }
   var locId = localStorage.getItem("Location");
   if(locId !== null && locId !== undefined)
   {
   this.filterbylocation(locId)
   }
   var topicID = localStorage.getItem("Topic");
    if(topicID !== null && topicID !== undefined)
   {
   this.filterByTopic(topicID)
   }
   var ilaID = localStorage.getItem("ILA");
   if(ilaID !== null && ilaID !== undefined)
   {
   this.filterbyILA(ilaID)
   }
    this.refresh.next();
  }
  YearSelectionChanged() {
    if (this.OrignalClassSchedulesListIlaView.length > 0) {
      var tblEvents = [];
      var filterYears = this.ilaFilterForm.get('selectedYear')?.value;
      tblEvents = this.OrignalClassSchedulesListIlaView.filter((f) => {
        return filterYears.includes(f.startDateTime.getFullYear()) || filterYears.includes(f.endDateTime.getFullYear());
      });
      this.dataSource.data = tblEvents;
    }
  }
  changeStartSateForEdit(startDate: any) {
    let dateTime = new Date(startDate);
    let localDateTime = new Date(
      dateTime.getTime() + dateTime.getTimezoneOffset() * 60 * 1000
    );
    return localDateTime;
  }

  ToggleClassesGroupBy(groupBy: 'Calender'|'ILA') {
    this.dataSource.data =[];
    switch (groupBy) {
      case 'Calender':
        this.setView(this.view);
        break;
      case 'ILA':
        this.getProviders();
        break;
      default:
        break;
    }

    this.viewSelected = groupBy;

    if (this.viewSelected === 'ILA') {
      setTimeout(() => {
        this.provSelect._handleKeydown = (event: KeyboardEvent) => {

          if (event.key === 'SPACE')
            return
          if (!this.provSelect.disabled) {
            // this.select.panelOpen
            //   ? this.select._handleOpenKeydown(event)
            //   : this.select._handleClosedKeydown(event);
          }
        };
        this.ilaSelect._handleKeydown = (event: KeyboardEvent) => {

          if (event.key === 'SPACE')
            return
          if (!this.ilaSelect.disabled) {
            // this.select.panelOpen
            //   ? this.select._handleOpenKeydown(event)
            //   : this.select._handleClosedKeydown(event);
          }
        };
      }, 1)
    }
  }
  async getClassScheduleByILA(ilaId) {
    this.trainingSevc.getClassesByIla(ilaId).then((res) => {
      var tblEvents = [];

      res.forEach((obj) => {
        var startDateString = this.datePipe.transform(
          obj.startDateTime,
          'yyyy-MM-dd hh:mm a'
        );

        const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
        // Convert UTC date and time to local time
        const localstartDateTimeString = utcStartDateTime.toLocaleString();
        obj.startDateTime = new Date(Date.parse(localstartDateTimeString));
        var endDateString = this.datePipe.transform(
          obj.endDateTime,
          'yyyy-MM-dd hh:mm a'
        );
        //const utcendtDateTimeString = res.startDateTime.toDateString();
        const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
        // Convert UTC date and time to local time
        const localendDateTimeString = utcendtDateTime.toLocaleString();
        obj.endDateTime = new Date(Date.parse(localendDateTimeString));
        tblEvents.push({
          ...obj,
          location: obj.location === null ? 'N/A' : obj.location.locName,
          employeeCount: obj.employeeCount,
          instructor:
            obj.instructor === null ? 'N/A' : obj.instructor.instructorName,
          startDateTime: obj.startDateTime,
          endDateTime: obj.endDateTime,
          canDelte:obj.canDelete,
        });
      });
      this.OrignalClassSchedulesListIlaView = Object.assign(tblEvents);
      /// HERE +++++
      this.dataSource.data = tblEvents;
      this.dataSource.filter = "";
      this.YearSelectionChanged();
      //
      // this.dataSource.data=res.map((obj)=>{

      //   return {...obj,location:obj.location === null ? "N/A":obj.location.locName,instructor:obj.instructor === null ? "N/A":obj.instructor.instructorName,startDateTime:obj.startDateTime,endDateTime:obj.endDateTime}
      // });

      this.isLoading = false;
    });
  }
  filterData() {
    this.ilaFilterForm.get('ilaSearch')?.setValue('');
    this.ila_list = this.isIncludeInactiveILAs ? this.ila_list_original : this.ila_list_original.filter(x => x.active == true);
    var filterObj = {
      providerID: this.ilaFilterForm.get('providerId')?.value.trim(),
      ilaid: this.ilaFilterForm.get('ilaId')?.value.trim(),
    };
    if (filterObj.ilaid !== '') {
      this.getClassScheduleByILA(filterObj.ilaid);
    }

    //this.dataSource.filter = JSON.stringify(filterObj);
  }
  CreateNewTraining() {
    this.setCalendarState();
    //this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/addTraining']);
  }
  modalData: {
    action: string;
    event: CalendarEvent;
  };
  setView(view: CalendarView)
  {
    //var insId = localStorage.getItem("Instructor");

    this.view = view;
    switch (this.view) {
      case 'month':
        this.isLoading = true;
        const today = new Date(this.viewDate);
        this.startDateTime = format(today, 'yyyy-MM-01');
        this.endDateTime = format(lastDayOfMonth(today), 'yyyy-MM-dd');
        this.getAllTrainingSchedule();
        break;
      case 'week':
        this.isLoading = true;
        const todayDate = new Date(this.viewDate);
        this.startDateTime = format(startOfWeek(todayDate), 'yyyy-MM-dd');
        this.endDateTime = format(endOfWeek(todayDate), 'yyyy-MM-dd');
        this.getAllTrainingSchedule();

        break;
      case 'day':
        this.isLoading = true;
        const todayDateDay = new Date(this.viewDate);
        this.startDateTime = format(todayDateDay, 'yyyy-MM-dd');
        this.endDateTime = format(todayDateDay, 'yyyy-MM-dd');
        this.getAllTrainingSchedule();
        break;

      default:
        break;
    }
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    var state = this.store.select('toggle');
    state
      .subscribe((data) => {
        this.myNavBarState = data;
      })
      .unsubscribe();
    this.store.dispatch(sideBarClose());
    this.flyPanelSrvc.open(portal);
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }
  handleEvent(action: string, event: CalendarEvent): void { }
  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }
  closeOpenMonthViewDay() {
    this.setView(this.view);
    this.activeDayIsOpen = false;
  }

  async getProviders() {
    this.isSpinner = true;
    this.providerSrvc
      .getActiveProviders()
      .then((res: any) => {
        this.provider_list = res;
        this.provider_list_original = Object.assign(this.provider_list);
        if (this.provider_list.length === 0) {
          this.noProviderFound = true;
        } else {
          this.noProviderFound = false;
          var ilaId = this.ilaFilterForm.get('ilaId')?.value;
          if(ilaId !== null && ilaId !== undefined && ilaId !== ''){
            this.filterData();
          }
        }
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  selectProvider(event: any) {
    this.ila_list = [];
    this.isILALoading = true;
    this.ilaService
      .getByProvider(event)
      .then(async (res: any) => {
        this.ila_list = res;
        this.ila_list_original = Object.assign(this.ila_list);
        if (this.ila_list.length == 0) {
          this.noILAFound = true;
        } else {
          this.noILAFound = false;
        }
        this.filterData();
      })
      .finally(() => {
        this.isILALoading = false;
      });
  }
  selfRegistration() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/self-registration']);
  }
  reRelease() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/re-release']);
  }

  retake() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/retake']);
  }

  ilaClasses() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/ila-classes']);
  }

  waitList() {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/implementation/sc/waitlist']);
  }

  closeFlyPanel() {
    this.flyPanelSrvc.close();
    this.resetBackDropAndRestoreNavBar();
  }
  resetBackDropAndRestoreNavBar() {
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    if (this.myNavBarState === 'open') {
      this.store.dispatch(sideBarOpen());
    } else {
      this.store.dispatch(sideBarClose());
    }
  }
  ilaSelected(event: any) {

    this.ilaName = event.description;
    if (this.viewSelected === 'Calender') {
      this.events = [];
      localStorage.removeItem("Topic");
      localStorage.removeItem("Location");
      localStorage.removeItem("Instructor");
      localStorage.setItem("ILA",event.id);
      this.filterBy = "ila";
      this.showCloseButtonAndFilter = true
      this.dataSource.data
        .filter((x) => x.ilaid == event.id)
        .forEach((eo) => {

          var startDateString = this.datePipe.transform(
            eo.startDateTime,
            'yyyy-MM-dd hh:MM a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            eo.endDateTime,
            'yyyy-MM-dd hh:MM a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          eo.endDateTime = new Date(Date.parse(localendDateTimeString));
          this.deleteChecks[eo.id] = eo.canDelete;
          this.events.push({
            start: eo.startDateTime,
            end: eo.endDateTime,
            provider:eo.provider,
            ila:eo.ila,
            location:eo.location,
            instructor:eo.instructor,
            title: `<div class="flex flex-row justify-between">
           <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
            <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
          </div>`,
            color: { ...colors.yellow },
            id: eo.id,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
          });
          //this.dataSource.data = res;
        });
      this.closeFlyPanel();
    } else {
      let filterObj = {
        ilaid: event.id.trim(),
      };
      this.dataSource.filter = JSON.stringify(filterObj);
      this.closeFlyPanel();
    }
  }

  instructorSelected(event: any)
  {
    if (this.viewSelected === 'Calender') {
      var filterObj = {
        instructorId: event.trim(),

      };
      this.showCloseButtonAndFilter = true;
      this.filterBy = "instructor"
      localStorage.setItem("Instructor",filterObj.instructorId)
      localStorage.removeItem("Topic");
      localStorage.removeItem("Location");
      localStorage.removeItem("ILA");
      this.events = [];
      this.dataSource.data
        .filter((x) => x.instructorId == event)
        .forEach((eo) => {

          //this.insName = eo.instructor;
          // var insName = eo.find(x=>x.instructor).first();
          //
          var startDateString = this.datePipe.transform(
            eo.startDateTime,
            'yyyy-MM-dd hh:MM a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            eo.endDateTime,
            'yyyy-MM-dd hh:MM a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          eo.endDateTime = new Date(Date.parse(localendDateTimeString));
          this.deleteChecks[eo.id] = eo.canDelete;
          this.events.push({
            start: eo.startDateTime,
            end: eo.endDateTime,
            provider:eo.provider,
            ila:eo.ila,
            location:eo.location,
            instructor:eo.instructor,
            title: `<div class="flex flex-row justify-between">
          <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
          <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
          </div>`,
            color: { ...colors.yellow },
            id: eo.id,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
          });
          //this.dataSource.data = res;
        });

      this.closeFlyPanel();
    } else {
      this.showCloseButtonAndFilter = true;
      this.filterBy = "instructor"
      var filterObj = {
        instructorId: event.trim(),
      };
      localStorage.setItem("Instructor",filterObj.instructorId)
      localStorage.removeItem("Topic");
      localStorage.removeItem("Location");
      localStorage.removeItem("ILA");
      this.dataSource.filter = JSON.stringify(filterObj);
      this.closeFlyPanel();
    }
  }

  locationSelected(event: any) {
    this.showCloseButtonAndFilter = true;
    this.filterBy = "location"

    localStorage.setItem("Location",event);
    localStorage.removeItem("Instructor");
    localStorage.removeItem("Topic");
    localStorage.removeItem("ILA");
    if (this.viewSelected === 'Calender') {
      this.events = [];
      this.dataSource.data
        .filter((x) => x.locationId === event)
        .forEach((eo) => {

          var startDateString = this.datePipe.transform(
            eo.startDateTime,
            'yyyy-MM-dd hh:MM a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            eo.endDateTime,
            'yyyy-MM-dd hh:MM a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          eo.endDateTime = new Date(Date.parse(localendDateTimeString));
          this.deleteChecks[eo.id] = eo.canDelete;
          this.events.push({
            start: eo.startDateTime,
            end: eo.endDateTime,
            provider:eo.provider,
            ila:eo.ila,
            location:eo.location,
            instructor:eo.instructor,
            title: `<div class="flex flex-row justify-between">
          <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
          <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
          </div>`,
            color: { ...colors.yellow },
            id: eo.id,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
          });
          //this.dataSource.data = res;
        });
      this.closeFlyPanel();
    } else {
      var filterObj = {
        locationId: event.trim(),
      };
      this.dataSource.filter = JSON.stringify(filterObj);
      this.closeFlyPanel();
    }
  }
  topicSelected(event: any) {
    if (this.viewSelected === 'Calender') {
      var filterObj = {
        topicId: event.trim(),
      };
    localStorage.setItem("Topic",event);
    localStorage.removeItem("Location");
    localStorage.removeItem("Instructor");
    localStorage.removeItem("ILA");
    this.filterBy = "topic";
    this.showCloseButtonAndFilter = true;
      this.events = [];
      this.dataSource.data
        .filter((x) => x.topicIds.includes(event))
        .forEach((eo) => {

          var startDateString = this.datePipe.transform(
            eo.startDateTime,
            'yyyy-MM-dd hh:MM a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            eo.endDateTime,
            'yyyy-MM-dd hh:MM a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          eo.endDateTime = new Date(Date.parse(localendDateTimeString));
          this.deleteChecks[eo.id] = eo.canDelete;
          this.events.push({
            start: eo.startDateTime,
            end: eo.endDateTime,
            provider:eo.provider,
            ila:eo.ila,
            location:eo.location,
            instructor:eo.instructor,
            title: `<div class="flex flex-row justify-between">
          <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
          <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
          </div>`,
            color: { ...colors.yellow },
            id: eo.id,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
          });
          //this.dataSource.data = res;
        });
      this.closeFlyPanel();
    } else {
      var filterObj = {
        topicId: event.trim(),
      };
      this.dataSource.filter = JSON.stringify(filterObj);
      this.closeFlyPanel();
    }
  }
  // changeTimeZone(startDate: any) {
  //   let dateTime = new Date(startDate);
  //   let localDateTime = new Date(
  //     dateTime.getTime() + dateTime.getTimezoneOffset() * 60 * 1000
  //   );
  //   return localDateTime;
  // }

  deleteClass(templateRef: any, event: any) {

    if (this.viewSelected === 'Calender') {
      var scCalenderView;
      this.dataSource.data
        .filter((x) => x.id == event.id)
        .forEach((eo) => {

          scCalenderView = eo;
        });
      this.scId = scCalenderView.id;
      var ilaName = scCalenderView.ila;
      if (ilaName.name != null) {
        ilaName = scCalenderView.ila.name;
      }
      var startDT = this.datePipe.transform(
        scCalenderView.startDateTime,
        'yyyy-MM-dd hh:mm a'
      );
      var endDT = this.datePipe.transform(
        scCalenderView.endDateTime,
        'yyyy-MM-dd hh:mm a'
      );
      this.deleteDescription = `You are selecting to delete the Class for ${ilaName}, scheduled for  ${startDT.toString()} and ${endDT.toString()}`;
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    } else {

      this.scId = event.id;
      //var startDateTimeObj = this.changeStartSateForEdit(event.startDateTime);
      var ilaName = event.ila;
      if (ilaName.name != null) {
        ilaName = event.ila.name;
      }
      var startDT = this.datePipe.transform(
        event.startDateTime,
        'yyyy-MM-dd hh:mm a'
      );
      this.deleteDescription = `You are selecting to delete the Class for ${ilaName}, scheduled for ${startDT.toString()}`;
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
  }
  Delete(event: any) {

    this.trainingSevc.delete(this.scId).then((res: any) => {
      this.alert.successToast('Class Deleted Successfully');
      if (this.viewSelected === 'Calender') {
        this.getAllTrainingSchedule();
      } else {
        var ilaId = this.ilaFilterForm.get('ilaId')?.value;
        if(ilaId !== null && ilaId !== undefined && ilaId !== ''){
          this.getClassScheduleByILA(ilaId);
        }
      }
    });
  }
  async EditRecurrTrainingWithCompletionRecords(templateRef: any, id: any) {
    this.scId = id;
    this.deleteDescription = `` + await this.labelPipe.transform('Employee') + ` completion records have been recorded for this class. Editing class settings could result in reporting discrepancies.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  EditSchedule() {
    this.setCalendarState();
    this._router.navigate(['/implementation/sc/editTraining/', this.scId]);
  }

  convertUtcTimeToLocalTime(datetimeObj: any) {
    const utcStartDateTime = new Date(datetimeObj);
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var localDateTime = new Date(Date.parse(localstartDateTimeString));

    //var parsedDate = this.datePipe.transform(localstartDateTimeString,"yyyy-MM-dd hh:MM a")
    return localDateTime;
  }

  providerSearch(value: any) {
    var filterString = this.ilaFilterForm.get('searchTxt')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.provider_list = this.provider_list_original.filter((f) => {
      return f.name.toLowerCase().trim().includes(filterString);
    });
  }

  ilaSearch() {
    var filterString = this.ilaFilterForm.get('ilaSearch')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    if(this.isIncludeInactiveILAs){
      this.ila_list = this.ila_list_original.filter((f) => {
        return f.name.trim().toLowerCase().includes(filterString) || f.number?.trim().toLowerCase().includes(filterString);
      });
    }
    else{
      this.ila_list = this.ila_list_original.filter((f) => {
        return (f.name.trim().toLowerCase().includes(filterString) || f.number?.trim().toLowerCase().includes(filterString)) && f.active == true;
      });
    }
  }

  saveCalendarState(id: any) {
    this.setCalendarState();
    this._router.navigate(['/implementation/sc/grades/', id])
  }

  setCalendarState(){
    var options: CalendarViewSession = {
      viewSelected: this.viewSelected,
      startDateTime: this.startDateTime,
      endDateTime: this.endDateTime,
      view: this.view,
      providerId: this.ilaFilterForm.get('providerId')?.value,
      ilaId: this.ilaFilterForm.get('ilaId')?.value,
      shouldApply: false,
      viewDate: this.viewDate,
      filterYears:this.ilaFilterForm.get('selectedYear')?.value,
    }
    localStorage.setItem('calendarSession', JSON.stringify(options));
  }

  routeToEditWithState(id:any){
    this.setCalendarState();
    this._router.navigate(['/implementation/sc/editTraining/', id]);
  }
  filterbyInstructor(insId:any)
  {


    this.events = [];
    this.dataSource.data
      .filter((x) => x.instructorId == insId)
      .forEach((eo) => {

        //this.insName= eo.instructor;
        var startDateString = this.datePipe.transform(
          eo.startDateTime,
          'yyyy-MM-dd hh:MM a'
        );

        const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
        // Convert UTC date and time to local time
        const localstartDateTimeString = utcStartDateTime.toLocaleString();
        eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
        var endDateString = this.datePipe.transform(
          eo.endDateTime,
          'yyyy-MM-dd hh:MM a'
        );
        //const utcendtDateTimeString = res.startDateTime.toDateString();
        const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
        // Convert UTC date and time to local time
        const localendDateTimeString = utcendtDateTime.toLocaleString();
        eo.endDateTime = new Date(Date.parse(localendDateTimeString));
        this.deleteChecks[eo.id] = eo.canDelete;
        this.events.push({
          start: eo.startDateTime,
          end: eo.endDateTime,
          provider:eo.provider,
          ila:eo.ila,
          location:eo.location,
          instructor:eo.instructor,
          title: `<div class="flex flex-row justify-between">
        <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
        <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
        </div>`,
          color: { ...colors.yellow },
          id: eo.id,
          resizable: {
            beforeStart: true,
            afterEnd: true,
          },
        });
        //this.dataSource.data = res;
      });

  }
  filterbylocation(locId:any)
  {


    this.events = [];
    this.dataSource.data
      .filter((x) => x.locationId == locId)
      .forEach((eo) => {

        //this.insName= eo.instructor;
        var startDateString = this.datePipe.transform(
          eo.startDateTime,
          'yyyy-MM-dd hh:MM a'
        );

        const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
        // Convert UTC date and time to local time
        const localstartDateTimeString = utcStartDateTime.toLocaleString();
        eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
        var endDateString = this.datePipe.transform(
          eo.endDateTime,
          'yyyy-MM-dd hh:MM a'
        );
        //const utcendtDateTimeString = res.startDateTime.toDateString();
        const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
        // Convert UTC date and time to local time
        const localendDateTimeString = utcendtDateTime.toLocaleString();
        eo.endDateTime = new Date(Date.parse(localendDateTimeString));
        this.deleteChecks[eo.id] = eo.canDelete;
        this.events.push({
          start: eo.startDateTime,
          end: eo.endDateTime,
          provider:eo.provider,
          ila:eo.ila,
          location:eo.location,
          instructor:eo.instructor,
          title: `<div class="flex flex-row justify-between">
        <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
        <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
        </div>`,
          color: { ...colors.yellow },
          id: eo.id,
          resizable: {
            beforeStart: true,
            afterEnd: true,
          },
        });
        //this.dataSource.data = res;
      });

  }
 filterbyILA(ilaID:any)
 {
  this.events = [];

      this.filterBy = "ila";
      this.showCloseButtonAndFilter = true
      this.dataSource.data
        .filter((x) => x.ilaid == ilaID)
        .forEach((eo) => {

          var startDateString = this.datePipe.transform(
            eo.startDateTime,
            'yyyy-MM-dd hh:MM a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            eo.endDateTime,
            'yyyy-MM-dd hh:MM a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          eo.endDateTime = new Date(Date.parse(localendDateTimeString));
          this.deleteChecks[eo.id] = eo.canDelete;
          this.events.push({
            start: eo.startDateTime,
            end: eo.endDateTime,
            provider:eo.provider,
          ila:eo.ila,
          location:eo.location,
          instructor:eo.instructor,
            title: `<div class="flex flex-row justify-between">
           <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
            <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
          </div>`,
            color: { ...colors.yellow },
            id: eo.id,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
          });
          //this.dataSource.data = res;
        });
 }

 filterByTopic(topicId:any)
 {
  this.events = [];
      this.dataSource.data
        .filter((x) => x.topicIds.includes(topicId))
        .forEach((eo) => {

          var startDateString = this.datePipe.transform(
            eo.startDateTime,
            'yyyy-MM-dd hh:MM a'
          );

          const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localstartDateTimeString = utcStartDateTime.toLocaleString();
          eo.startDateTime = new Date(Date.parse(localstartDateTimeString));
          var endDateString = this.datePipe.transform(
            eo.endDateTime,
            'yyyy-MM-dd hh:MM a'
          );
          //const utcendtDateTimeString = res.startDateTime.toDateString();
          const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
          // Convert UTC date and time to local time
          const localendDateTimeString = utcendtDateTime.toLocaleString();
          eo.endDateTime = new Date(Date.parse(localendDateTimeString));
          this.deleteChecks[eo.id] = eo.canDelete;
          this.events.push({
            start: eo.startDateTime,
            end: eo.endDateTime,
            provider:eo.provider,
          ila:eo.ila,
          location:eo.location,
          instructor:eo.instructor,
            title: `<div class="flex flex-row justify-between">
          <span class="font-bold"> ${eo.provider} ${eo.ila}</span>
          <span class="nonGreen"> ${eo.location} by ${eo.instructor}</span>
          </div>`,
            color: { ...colors.yellow },
            id: eo.id,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
          });
          //this.dataSource.data = res;
        });
 }

  clearFilter()
  {
     this.showCloseButtonAndFilter = false;
     localStorage.removeItem("Instructor")
     localStorage.removeItem("Topic");
     localStorage.removeItem("Location");
     localStorage.removeItem("ILA");
     if(this.viewSelected === 'ILA'){
      var provId = this.ilaFilterForm.get('providerId')?.value;
      var ilaId = this.ilaFilterForm.get('ilaId')?.value
      if (provId !== null && provId !== undefined && provId !== "" && ilaId !== null && ilaId !== undefined && ilaId !== "") {
        this.filterData();
      }

      // if(options.filterYears !== null && options.filterYears !== undefined){
      //   this.ilaFilterForm.get('selectedYear')?.setValue(options.filterYears);
      //   this.ilaFilterForm.updateValueAndValidity();
      // }
     }
     else{
      this.getAllTrainingSchedule();
     }
  }

  selectedInsName(event:any)
  {

    this.insName = event;
  }
  selectedTopicName(event:any)
  {

    this.topicName = event;
  }
  selectedlocName(event:any)
  {

    this.locName = event;
  }
  selectedILAName(event:any)
  {

    this.ilaName = event;
  }

  includeInActiveILAs(event : any){
  this.isIncludeInactiveILAs = event.checked ? true : false;
  }

  async getReportSkeletonData() {
    this.reportSkeleton = await this.apireportService.getReportSkeletonByNameAsync(this.reportSkeletonName);
    this.displayColumns =  Object.assign(this.reportSkeleton?.displayColumns);
  }

  private handleFileDownload(response: HttpResponse<Blob>) {
      const contentDispositionHeader = response.headers.get('content-disposition');
  
      const fileName = contentDispositionHeader
        ? contentDispositionHeader.split(';')[1].trim().split('=')[1].replace(/["']/g, "")
        : 'downloaded-file.csv';
  
      const blob = new Blob([response.body!], { type: 'application/octet-stream' });
      const url = window.URL.createObjectURL(blob);
  
      const link = document.createElement('a');
      link.href = url;
      link.download = fileName;
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }

    getClassReportsCreateUpdateOptions(classId:any){
      var reportCreateOptions = new ReportUpdateOptions();
      this.displayColumns.map(item=>{
        reportCreateOptions.getDisplayColumns(item.columnName)
      })
      var reportFilters = Array<ReportFilterOption>();
      var classFilter  = this.reportSkeleton?.availableFilters?.find(x=>x.name.toLowerCase() =='training classes');
      const classIdFilter = new ReportFilterOption(classFilter.name,classId);
      reportFilters.push(classIdFilter);
      reportCreateOptions.filters = reportFilters;
      reportCreateOptions.reportSkeletonId = this.reportSkeleton?.id;
      reportCreateOptions.internalReportTitle = this.reportSkeletonName;
      this.reportCreateorUpdate = reportCreateOptions;
    }

  private rosterSubscription?: Subscription;
  private signInSubscription?: Subscription;

  public downloadClassRosterReport(classId: any) {
    this.isDownloadingReport = true;
    this.reportSkeletonName = "Class Roster";

    this.getReportSkeletonData().then(() => {
      this.getClassReportsCreateUpdateOptions(classId);

      var options = new ReportExportOptions();
      options.exportType = ReportExportType.Pdf;
      options.options = this.reportCreateorUpdate;

      if (this.rosterSubscription) this.rosterSubscription.unsubscribe();

      this.rosterSubscription = this.trainingSevc.generateClassRosterReport(options)
        .subscribe({
          next: (res) => this.handleFileDownload(res),
          error: (err) => {
            this.alert.errorToast("Download failed. Please try again.");
            this.isDownloadingReport = false;
          },
          complete: () => this.isDownloadingReport = false
        });
    });
  }

  public downloadClassSignInSheetReport(classId: any) {
    this.isDownloadingReport = true;
    this.reportSkeletonName = "Class Sign In Sheet";

    this.getReportSkeletonData().then(() => {
      this.getClassReportsCreateUpdateOptions(classId);

      var options = new ReportExportOptions();
      options.exportType = ReportExportType.Pdf;
      options.options = this.reportCreateorUpdate;

      if (this.signInSubscription) this.signInSubscription.unsubscribe();

      this.signInSubscription = this.trainingSevc.generateClassInSheetReport(options)
        .subscribe({
          next: (res) => this.handleFileDownload(res),
          error: (err) => {
            this.alert.errorToast("Download failed. Please try again.");
            this.isDownloadingReport = false;
          },
          complete: () => this.isDownloadingReport = false
        });
    });
  }
}



export class CalendarViewSession {
  viewSelected:'ILA'|'Calender';
  startDateTime!: any;
  endDateTime!: any;
  view!: CalendarView;
  providerId!: any;
  ilaId!: any;
  shouldApply!: boolean;
  viewDate!: any;
  filterYears!:any;
}
