import { animate, state, style, transition, trigger } from "@angular/animations";
import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { MatLegacyDialog as MatDialog } from "@angular/material/legacy-dialog";
import { MatLegacyTableDataSource as MatTableDataSource } from "@angular/material/legacy-table";
import { Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { CalendarMonthViewBeforeRenderEvent } from "angular-calendar";
import { isSameDay } from "date-fns";
import { PublicClassScheduleRequestService } from "src/app/_Services/QTD/public-class-schedule-request.service";
import { FlyInPanelService } from "src/app/_Shared/services/flyInPanel.service";
import { sideBarOpen } from "src/app/_Statemanagement/action/state.menutoggle";

@Component({
  selector: 'app-fly-panel-public-class-detail-view',
  templateUrl: './fly-panel-public-class-detail-view.component.html',
  styleUrls: ['./fly-panel-public-class-detail-view.component.scss'],
  animations: [
    trigger('slideToggle', [
      state('closed', style({
        height: '0',
        overflow: 'hidden',
        opacity: 0
      })),
      state('open', style({
        height: '*',
        opacity: 1
      })),
      transition('closed <=> open', [
        animate('300ms ease-in-out')
      ])
    ])
  ]
})
export class FlyPanelPublicClassDetailViewComponent implements OnInit {
  @Input() selectedClass: any;
  isHourDetailCollapsed: boolean = false;
  isDescriptionCollapse: boolean = true;
  isCourseRequirementCollapse: boolean = false;
  isAvailbleClassDetailCollapse: boolean = true;
  availableClassDetails: any;
  @Input() instanceName: string
  classDetails: any;
  ilaRequirements: any;
  availableClassesToDisplay = ['classStartDateTime', 'location', 'instructor', 'availableSeat', 'action'];
  availableClassesDataSource = new MatTableDataSource<any>();
  @Output() register = new EventEmitter<any>();
  classId: any;
  selectedTab: 'calenderView' | 'tableView' = 'tableView';
  selectedDay = new Date();
  loader: boolean = false;
  daysInfo = ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'];
  monthInfo = [ { number: 0, title: 'January',},
                { number: 1, title: 'Feburary', },
                { number: 2, title: 'March',},
                { number: 3, title: 'April',},
                { number: 4, title: 'May',},
                { number: 5, title: 'June',},
                { number: 6, title: 'July',},
                { number: 7, title: 'August',},
                { number: 8, title: 'September',},
                { number: 9, title: 'October',},
                { number: 10,title: 'November',},
                { number: 11,title: 'December', }
  ]
  newDate = new Date();
  events?: any;
  currentDate = new Date();
  selectedDayString!: string;
  dateClassesDataSource = new MatTableDataSource<any>([]);
  selectedDateClassesToDisplay:any[]; 

  constructor(private store: Store<{ toggle: string }>,
    public flyPanelSrvc: FlyInPanelService,
    private publicClassScheduleRequestService: PublicClassScheduleRequestService,
    public dialog: MatDialog,
    private route: Router){  }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.getIlaRequirements();
    this.selectedDateClassesToDisplay = ['classStartDateTime', 'location', 'instructor', 'availableSeat', 'action'];
  }

  toggleHoursDetails(){
    this.isHourDetailCollapsed = !this.isHourDetailCollapsed;
  }

  toggleDescriptionDetails(){
    this.isDescriptionCollapse = !this.isDescriptionCollapse;
  }

  toggleIlaRequirementDetails(){
    this.isFinalAssessmentRequired();
    this.isCourseRequirementCollapse = !this.isCourseRequirementCollapse;
  }

  toggleAvailbleClassDetails(){
    this.isAvailbleClassDetailCollapse = !this.isAvailbleClassDetailCollapse;
  }
  async getIlaRequirements(){
    this.loader = true;
    this.ilaRequirements = await this.publicClassScheduleRequestService.GetILACompletionRequirementAsync(this.instanceName, this.selectedClass[0].ilaObject.id);
    this.availableClassDetails = this.selectedClass[0].classes.map((cls) => {
      const seats = this.ilaRequirements.availableSeatsDetails.find(x => x.classScheduleId === cls.id);
      return{
        ...cls,
        availableSeats: seats
      }
    })
    this.availableClassesDataSource = this.availableClassDetails;
    this.loader = false;
  }

  redirectToEmployee(){
    let fullPath = window.location.href;
    let path = fullPath.split("/");
    let domain = path.slice(0,-2).join("/");
    this.dialog.closeAll();
    const loginUrl = `${domain}/auth/login`
    window.open(loginUrl, '_blank');
  }

  openRegistrationForm(){
    this.dialog.closeAll();
    this.register.emit(this.classId)
  }

  onRegisterClick(templateRef: any, id: string){

    const dialog_ref = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    this.classId = id
  }

  isFinalAssessmentRequired(){
    const flags = [
      'isCBTRequired',
      'isPreTestRequired',
      'isSimulatorScenarioRequired',
      'isStudentEvaluationRequired',
      'isTaskQualificationRequired',
      'isFinalTestRequired'
    ];
    const isFinalAssessmentreq = flags.some((key) => this.ilaRequirements[key]);
    return isFinalAssessmentreq;
  }

  onCalenderViewClick(){
    this.selectedTab = 'calenderView'
  }

  calendarBeforeViewRender(e: CalendarMonthViewBeforeRenderEvent) {
      e.body.forEach(day => {
        if (this.eventExistsOnDay(day.date)) {
          day.cssClass = 'highlight-event-date';
        }
      });
    }

  eventExistsOnDay(date: Date): boolean{

    const classes: any[] = this.selectedClass[0].classes          
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

  onClickDate(clickedDate: Date): void {
    this.selectedDay = clickedDate;
    this.selectedDayString = clickedDate.toDateString();
    const dayString = this.selectedDayString;
    const filteredClasses = this.availableClassDetails
      .filter((cls: any) =>
        new Date(cls.startDateTime).toDateString() === dayString
      );
    
    this.dateClassesDataSource.data = filteredClasses;
  }
}
