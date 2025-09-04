import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { PublicallyAvailableClassesVm } from '@models/PublicClasses/PublicallyAvailableClasses';
import { Store } from '@ngrx/store';
import { CalendarMonthViewBeforeRenderEvent } from 'angular-calendar';
import { isSameDay } from 'date-fns';
import { PublicClassScheduleRequestService } from 'src/app/_Services/QTD/public-class-schedule-request.service';
import { PublicPortalClassScheduleService } from 'src/app/_Services/QTD/public-portal-class-schedule.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-public-portal-dashboard',
  templateUrl: './public-portal-dashboard.component.html',
  styleUrls: ['./public-portal-dashboard.component.scss']
})
export class PublicPortalDashboardComponent implements OnInit {

  selectedTab: 'detail' | 'dashboard' = 'dashboard';
  selectedIndex = 1;
  currentDate = new Date();
  selectedDay = new Date();
  newDate = new Date();
  selectedDayString!: string;
  events?: any;
  showLoader: boolean = false;
  publicClasses: PublicallyAvailableClassesVm[];
  dateClassesDataSource = new MatTableDataSource<any>([]);
  instanceName: string;
  selectedClassId: string;
  companyLogo: string;  
  companyName: string;
  generalSetting: any;
  daysInfo = ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'];
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

  publicallyAvailableCourses = new MatTableDataSource<any>([]);
  expandedTableSource = new MatTableDataSource<any>();
  expandedElement: any | null;
  columnsToDisplay = ["expand", 'ilaNumber', 'ilaName', 'deliveryMethodName', 'creditHours', 'classesCount', 'action'];
  expandedColumnsToDisplay = ['classStartDateTime', 'location', 'instructor', 'action'];
  classDetailsOnDateClick = new MatTableDataSource();
  displayedDateColumns: string[] = [
    'expand',
    'ilaName',
    'totalTrainingHours',
    'ilaDescription',
    'moreInformation'
  ];
  dashBoardClassDetails = ['classStartDateTime', 'classEndDateTime', 'location', 'instructor', 'action']
  datePipe = new DatePipe('en-us');
  currentTab: any;
  public expandedIlaId: string | null = null;
  selectedClass: any;
  selectedClassDetails: any;
  constructor(private store: Store<{ toggle: string }>,
    public dialog: MatDialog,
    private router: Router,
    private publicClassesService: PublicPortalClassScheduleService,
    public flyPanelService: FlyInPanelService,
    private route: ActivatedRoute,
    private publicClassScheduleRequestService: PublicClassScheduleRequestService
  ) { }
  ILA: any[] = [];
  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.instanceName = this.route.snapshot.paramMap.get('instanceName')!
    this.readyClasses();
  }
  getExpandedElement(row: { ilaObject: any; classes: any[] }) {
    this.expandedElement = this.expandedElement === row ? null : row;

    this.expandedTableSource.data = this.expandedElement
      ? this.expandedElement.classes
      : [];
  }

  openPublicClassFlyPanel(templateRef: TemplateRef<any>, rowId: string) {
    this.publicClasses = this.publicClasses.map(cls => ({
      ...cls,
      classStartTime: new Date(cls.startDateTime + 'Z').toLocaleString(),
      classEndTime: new Date(cls.endDateTime + 'Z').toLocaleString(),
    }));
    this.selectedClass = this.publicClasses.filter((items) => items.publicILA.id == rowId);
    this.selectedClass = this.groupClassByIla(this.selectedClass);
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }
  calendarBeforeViewRender(e: CalendarMonthViewBeforeRenderEvent) {
    e.body.forEach(day => {
      if (this.eventExistsOnDay(day.date)) {
        day.cssClass = 'highlight-event-date';
      }
    });
  }

  private eventExistsOnDay(date: Date): boolean {
    if (!this.publicClasses) {
      return false;
    }
    const classes: any[] = Array.isArray(this.publicClasses)
      ? this.publicClasses
      : Object.values(this.publicClasses);
    return classes.some((cls: any) => {
      const dtString = cls.startDateTime ?? cls.endDateTime;
      if (!dtString) {
        return false;
      }
      return isSameDay(new Date(dtString), date);
    });
  }
  previousAndNextClick(type: string) {
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

  classCellClick(data: any) {
    this.events = null;
    this.selectedDay = data;
  }

  changeMonth(month: number) {
    const year = this.currentDate.getFullYear();
    this.newDate = new Date(year, month, 1);
    const selectedDay = new Date(this.selectedDay);
    this.selectedDay = new Date(selectedDay.getFullYear(), month, selectedDay.getDate());
    this.classCellClick(this.newDate);
  }
  covertUtcToLocalTime(datetime: any): Date {

    var startDateString = this.datePipe.transform(
      datetime,
      'yyyy-MM-dd hh:mm a'
    );
    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    return newdatetime;
  }

  isSameMonth(date: Date): boolean {
    return date.getUTCMonth() === this.selectedDay.getUTCMonth() &&
      date.getUTCFullYear() === this.selectedDay.getUTCFullYear();
  }

  toggleRow(element: any, index: any) {

  }

  onClickDate(clickedDate: Date): void {
    this.selectedDay = clickedDate;
    this.selectedDayString = clickedDate.toDateString();
    const dayString = this.selectedDayString;
    const filteredClasses = this.publicClasses
      .filter((cls: any) =>
        new Date(cls.startDateTime).toDateString() === dayString
      )
    const groupedClasses = this.groupClassByIla(filteredClasses);
    this.dateClassesDataSource = new MatTableDataSource(groupedClasses);
  }

  onDetailViewClick() {
    this.selectedTab = "detail"
    const classDetailsByIla = this.groupClassByIla(this.publicClasses);
    this.publicallyAvailableCourses = new MatTableDataSource(classDetailsByIla);
  }

  groupClassByIla(classDetailList: any) {
    const groupedObj = classDetailList.reduce((acc, item) => {
      if (!acc[item.ilaId]) {
        acc[item.ilaId] = {
          ilaObject: item.publicILA,
          classes: []
        };
      }
      acc[item.ilaId].classes.push(item);
      return acc;
    }, {});

    const groupedArray = Object.values(groupedObj).map((cls: any) => {
      cls.classes = cls.classes.map((c: any) => {
        return {
          ...c,
          startDateTime: new Date(c.startDateTime + "Z").toLocaleString(),
          endDateTime: new Date(c.endDateTime + "Z").toLocaleString()
        };
      });
      return cls;
    });
    return groupedArray;
  }
  async readyClasses() {
    this.showLoader = true;
    this.publicClasses = await this.publicClassesService.getAvailableClassSchedules(this.instanceName);
    this.generalSetting = await this.publicClassScheduleRequestService.getPublicClassCompanyLogoAsync(this.instanceName);
    this.companyLogo = this.generalSetting.companyLogo;
    this.companyName = this.generalSetting.companyName;
    const today = new Date();
    this.onClickDate(today);
    this.showLoader = false;
  }

  submitRequestClick(templateRef: any, rowId: string) {
    this.selectedClassId = rowId;
    const dialog_ref = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });

    this.publicClasses = this.publicClasses.map(cls => ({
      ...cls,
      classStartTime: new Date(cls.startDateTime + 'Z').toLocaleString(),
      classEndTime: new Date(cls.endDateTime + 'Z').toLocaleString(),
    }));
    this.selectedClassDetails = this.publicClasses.find(x => x.id == rowId)
  }


  redirectToEmployee() {
    let fullPath = window.location.href;
    let path = fullPath.split("/");
    let domain = path.slice(0,-2).join("/");
    this.dialog.closeAll();
    const loginUrl = `${domain}/auth/login`
    window.open(loginUrl, '_blank');
  }

  openRegistrationForm(templateRef: any, rowId: any) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
    this.selectedClassId = rowId;
    this.publicClasses = this.publicClasses.map(cls => ({
      ...cls,
      classStartTime: new Date(cls.startDateTime + 'Z').toLocaleString(),
      classEndTime: new Date(cls.endDateTime + 'Z').toLocaleString(),
    }));
    this.selectedClassDetails = this.publicClasses.find(x => x.id == rowId)
  }

  openRegisterationFlyPanel(templateRef: any) {
    this.dialog.closeAll();
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);

  }

}




