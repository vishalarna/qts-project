import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EventColor } from 'calendar-utils';
import { Subject } from 'rxjs';
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
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { Router } from '@angular/router';
const colors: Record<string, EventColor> = {
  yellow: {
    primary: 'rgb(92 155 49)',
    secondary: '#f8faf7b8',
  },
};
@Component({
  selector: 'app-calendar-base',
  templateUrl: './calendar-base.component.html',
  styleUrls: ['./calendar-base.component.scss']
})
export class CalendarBaseComponent implements OnInit {
  CalendarView = CalendarView;
  view: CalendarView = CalendarView.Day;
  activeDayIsOpen: boolean = true;
  startDateTime: string;
  endDateTime: string;
  isLoading: boolean = true;
  range: number[] = [];
  viewSelected = 'Calender';
  yearnow = new Date().getFullYear();
  viewDate: Date = new Date();
  dataSource = new MatTableDataSource<any>();
  datePipe = new DatePipe('en-us');
  events: any[] = [];
  deleteChecks: any = {};
  refresh = new Subject<void>();
  constructor(private trainingSevc: TrainingService,
    private providerSrvc: ProviderService,
    private ilaService: IlaService,
    private _router: Router,) { }

  ngOnInit(): void {

    localStorage.removeItem("Dashboard")
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
      // if (options.providerId !== null && options.providerId !== undefined && options.providerId !== "") {
      //   this.ilaFilterForm.get('providerId').setValue(options.providerId);
      //   this.selectProvider(options.providerId);
      // }
      // if (options.ilaId !== null && options.ilaId !== undefined && options.ilaId !== "") {
      //   this.ilaFilterForm.get('ilaId').setValue(options.ilaId);
      //   this.filterData();
      // }

      // if(options.filterYears !== null && options.filterYears !== undefined){
      //   this.ilaFilterForm.get('selectedYear')?.setValue(options.filterYears);
      //   this.ilaFilterForm.updateValueAndValidity();
      // }

      // this.ilaFilterForm.updateValueAndValidity();

      // 
      // localStorage.removeItem('calendarSession');
    }
    else {
      this.setView(this.view);
    }
    this.ToggleClassesGroupBy(this.viewSelected);
  }

  closeOpenMonthViewDay() {
    this.setView(this.view);
    this.activeDayIsOpen = false;
  }
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
  getyearsdropdown() {
    var i = 0;
    while (this.range[this.range.length - 1] !== 2005) {
      this.range.push((this.yearnow + 4) - i);
      i++;
    }
  }
  ToggleClassesGroupBy(groupBy: string) {
    switch (groupBy) {
      case 'Calender':
        break;
      case 'ILA':
        //this.getProviders();
        break;
      default:
        break;
    }

    this.viewSelected = groupBy;

    // if (this.viewSelected === 'ILA') {
    //   setTimeout(() => {
    //     this.provSelect._handleKeydown = (event: KeyboardEvent) => {
    //       
    //       if (event.key === 'SPACE')
    //         return
    //       if (!this.provSelect.disabled) {
    //         // this.select.panelOpen
    //         //   ? this.select._handleOpenKeydown(event)
    //         //   : this.select._handleClosedKeydown(event);
    //       }
    //     };
    //     this.ilaSelect._handleKeydown = (event: KeyboardEvent) => {
    //       
    //       if (event.key === 'SPACE')
    //         return
    //       if (!this.ilaSelect.disabled) {
    //         // this.select.panelOpen
    //         //   ? this.select._handleOpenKeydown(event)
    //         //   : this.select._handleClosedKeydown(event);
    //       }
    //     };
    //   }, 1)
    // }
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
    this.dataSource.data?.forEach((eo) => {
      
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

      //this.dataSource.data = res;
    });
    
    this.isLoading = false;
    //check localStorageItem

    
    this.refresh.next();
  }
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

  handleEvent(action: string, event: CalendarEvent): void { 

    

localStorage.setItem("Dashboard",'1')
this._router.navigate(['/implementation/sc/editTraining/', event.id]);
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
}
export class CalendarViewSession {
  viewSelected!: string;
  startDateTime!: any;
  endDateTime!: any;
  view!: CalendarView;
  providerId!: any;
  ilaId!: any;
  shouldApply!: boolean;
  viewDate!: any;
  filterYears!:any;
}