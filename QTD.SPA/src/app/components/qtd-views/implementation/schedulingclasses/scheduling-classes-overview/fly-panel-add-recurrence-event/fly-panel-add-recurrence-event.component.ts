import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, UntypedFormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
@Component({
  selector: 'app-fly-panel-add-recurrence-event',
  templateUrl: './fly-panel-add-recurrence-event.component.html',
  styleUrls: ['./fly-panel-add-recurrence-event.component.scss'],
})
export class FlyPanelAddRecurrenceEventComponent implements OnInit {
  @Input() dateInformation: any;
  @Output() createdRecurrenceEvent = new EventEmitter<any>();
  @Output() recurrenceInformationEvent = new EventEmitter<any>();
  startDate: Date;
  endDate: Date;
  @Input() minEndDate: string;
  startEndDateDifferenceInDays: number;
  recurrenceForm: UntypedFormGroup;
  recurrenceArray: any[] = [];

  recurrenceTypes = [
    { id: 1, name: 'Daily' },
    { id: 2, name: 'Weekly' },
    { id: 3, name: 'Monthly' },
  ];

  dailyDayTypes = [
    { id: 1, name: 'Everyday' },
    { id: 2, name: 'Week Days' },
    { id: 3, name: 'Weekend Days' },
  ];

  daysOfWeek = [
    { id: 1, name: 'Monday' },
    { id: 2, name: 'Tuesday' },
    { id: 3, name: 'Wednesday' },
    { id: 4, name: 'Thursday' },
    { id: 5, name: 'Friday' },
    { id: 6, name: 'Saturday' },
    { id: 0, name: 'Sunday' },
  ];

  monthlyOrdinalTypes = [
    { id: 1, name: 'First' },
    { id: 2, name: 'Second' },
    { id: 3, name: 'Third' },
    { id: 4, name: 'Fourth' },
    { id: 5, name: 'Last' },
  ];

  constructor(private flyPanelSrvc: FlyInPanelService) {}

  ngOnInit(): void {
    this.readyForm();

    this.startDate = new Date(this.dateInformation.startDate + 'T' + this.dateInformation.startTime);
    this.endDate = new Date(this.dateInformation.endDate + 'T' + this.dateInformation.endTime);
    this.startEndDateDifferenceInDays = 
      (new Date(this.endDate.getFullYear(), this.endDate.getMonth(), this.endDate.getDate()).getTime() - 
      new Date(this.startDate.getFullYear(), this.startDate.getMonth(), this.startDate.getDate()).getTime()) / (1000 * 60 * 60 * 24);;
  }

  readyForm() {
    this.recurrenceForm = new UntypedFormGroup({
      recurrenceType: new UntypedFormControl(1, Validators.required),
      endDate: new UntypedFormControl(this.dateInformation.startDate, Validators.required),
      dailyDayType: new UntypedFormControl(1, Validators.required),
      weeklyDays: new UntypedFormArray(this.daysOfWeek.map((_) => { return new UntypedFormControl(false, Validators.required); })),
      weeklyRecursEvery: new UntypedFormControl(1, [Validators.required, Validators.min(1)]),
      monthlyRecursEveryOption: new UntypedFormControl(1, Validators.required),
      monthlyRecursEveryDayOfWeekOrdinals: new UntypedFormArray([]),
      monthlyRecursEveryDayNumbers: new UntypedFormArray([], [Validators.required, Validators.min(1), Validators.max(31)]),
      monthlyRecursEvery: new UntypedFormControl(1, Validators.required)
    });

    this.addMonthlyRecursEveryDayOfWeekOrdinal();
    this.addMonthlyRecursEveryDayNumber();
  }

  addMonthlyRecursEveryDayOfWeekOrdinal(){
    (this.recurrenceForm.get('monthlyRecursEveryDayOfWeekOrdinals') as UntypedFormArray).push(new UntypedFormGroup({
      ordinal: new UntypedFormControl(1, Validators.required),
      dayOfWeek: new UntypedFormControl(1, Validators.required)
    }));
  }

  addMonthlyRecursEveryDayNumber(){
    (this.recurrenceForm.get('monthlyRecursEveryDayNumbers') as UntypedFormArray).push(new UntypedFormControl(1, [Validators.required, Validators.min(1), Validators.max(31)]));
  }

  saveInfo() {
    let currentDate = new Date(this.dateInformation.startDate + 'T00:00:00');
    let recurrenceEndDate = new Date(this.recurrenceForm.get('endDate')?.value + 'T00:00:00');

    let controlRecurrenceType = this.recurrenceForm.get('recurrenceType')?.value;
    let controlDailyDayType = this.recurrenceForm.get('dailyDayType')?.value;
    let controlWeeklyDays = this.recurrenceForm.get('weeklyDays')?.value;
    let controlWeeklyRecursEvery = this.recurrenceForm.get('weeklyRecursEvery')?.value;
    let controlMonthlyRecursEveryOption = this.recurrenceForm.get('monthlyRecursEveryOption')?.value;
    let controlMonthlyRecursEveryDayOfWeekOrdinals = this.recurrenceForm.get('monthlyRecursEveryDayOfWeekOrdinals')?.value;
    let controlMonthlyRecursEveryDayNumbers = this.recurrenceForm.get('monthlyRecursEveryDayNumbers')?.value;
    let controlMonthlyRecursEvery = this.recurrenceForm.get('monthlyRecursEvery')?.value;

    // Construct RecurrenceInformation object
    let recurrenceInformation = {"endDate": this.recurrenceForm.get('endDate')?.value, "recurrenceType": this.recurrenceTypes.filter(r => r.id === controlRecurrenceType)[0]?.name, "recurrenceDescription": "", "recursEveryNumber": ""}
    switch(controlRecurrenceType){
      case 1:
        recurrenceInformation.recurrenceDescription = this.dailyDayTypes.filter(r => r.id === controlDailyDayType)[0].name;
        break;
      case 2:
        recurrenceInformation.recurrenceDescription = this.daysOfWeek
          .filter((_, index) => controlWeeklyDays[index])
          .map(day => day.name)
          .join(", ");
        recurrenceInformation.recursEveryNumber = controlWeeklyRecursEvery;
        break;
      case 3:
        let recurrenceInstances : Set<string> = new Set<string>();
        switch(controlMonthlyRecursEveryOption){
          case 1:
            for(let dayOfWeekOrdinal of controlMonthlyRecursEveryDayOfWeekOrdinals){
              let monthlyOrdinalTypeName = this.monthlyOrdinalTypes.filter(r => r.id === dayOfWeekOrdinal.ordinal)[0]?.name;
              let dayOfWeekName = this.daysOfWeek.filter(r => r.id === dayOfWeekOrdinal.dayOfWeek)[0].name;
              recurrenceInstances.add(monthlyOrdinalTypeName + " " + dayOfWeekName);
            }
            recurrenceInformation.recurrenceDescription = Array.from(recurrenceInstances).sort((a, b) => 
            {
              let aComponents = a.split(' ');
              let bComponents = b.split(' ');

              let monthlyOrdinalTypeIdA = this.monthlyOrdinalTypes.find(r => r.name === aComponents[0])?.id;
              let monthlyOrdinalTypeIdB = this.monthlyOrdinalTypes.find(r => r.name === bComponents[0])?.id;
        
              if (isNaN(monthlyOrdinalTypeIdA) || isNaN(monthlyOrdinalTypeIdB)) {
                  return a.localeCompare(b);
              }

              if(monthlyOrdinalTypeIdA !== monthlyOrdinalTypeIdB){
                return monthlyOrdinalTypeIdA - monthlyOrdinalTypeIdB;
              }

              let dayOfWeekOrdinalIndexA = this.daysOfWeek.findIndex(r => r.name === aComponents[1]);
              let dayOfWeekOrdinalIndexB = this.daysOfWeek.findIndex(r => r.name === bComponents[1]);

              if (isNaN(dayOfWeekOrdinalIndexA) || isNaN(dayOfWeekOrdinalIndexB)) {
                return a.localeCompare(b);
              }

              return dayOfWeekOrdinalIndexA - dayOfWeekOrdinalIndexB;
            })
            .join(", ");
            break;
          case 2:
            for(let dayOfMonthNumber of controlMonthlyRecursEveryDayNumbers){
              recurrenceInstances.add(dayOfMonthNumber + this.getDaySuffix(dayOfMonthNumber));
            }
            recurrenceInformation.recurrenceDescription = Array.from(recurrenceInstances).sort((a, b) => 
            {
              let intA = parseInt(a);
              let intB = parseInt(b);
        
              if (isNaN(intA) || isNaN(intB)) {
                  return a.localeCompare(b);
              }
          
              return intA - intB;
            })
            .join(", ");
            break;
        }
        recurrenceInformation.recursEveryNumber = controlMonthlyRecursEvery;
        break;
    }
    this.recurrenceInformationEvent.emit(recurrenceInformation);

    // Construct Recurrence array
    this.recurrenceArray = [];
    this.changeDate(currentDate, 1); // Don't allow recurrence array to include the Start Date, so incrment past it before constructing RecurrenceArray
    while (currentDate <= recurrenceEndDate) {
      let currentDayOfWeek = currentDate.getDay();
      switch (controlRecurrenceType) {
        case 1: // Daily
          switch (controlDailyDayType) {
            case 1: // Everyday
              this.addToRecurrenceArray(currentDate);
              break;
            case 2: // Week Day
              if (currentDayOfWeek >= 1 && currentDayOfWeek <= 5) {
                this.addToRecurrenceArray(currentDate);
              }
              break;
            case 3: // Weekend Day
              if (currentDayOfWeek === 0 || currentDayOfWeek === 6) {
                this.addToRecurrenceArray(currentDate);
              }
              break;
          }
          break;
        case 2: // Weekly
          // Add a recurrence if 'currentDayOfWeek' is one of the selected 'daysOfWeek'
          // Shifting 1 day back because with daysOfWeek array, 0 is Monday, but with Date.getDay(), 0 is Sunday, whats why its doing modulus here
          if(controlWeeklyDays[(((currentDayOfWeek-1) % 7) + 7) % 7]){
            this.addToRecurrenceArray(currentDate);
          }
          // Skip 'weeklyRecursEvery' amount of weeks once on the specific day of week 
          // 0 makes Sunday be considered the last day of the week
          if(currentDayOfWeek === 0){
            this.changeDate(currentDate, (controlWeeklyRecursEvery - 1) * 7);
          }
          break;
        case 3: // Monthly
          let lastDayOfMonth = new Date(currentDate);
          lastDayOfMonth.setMonth(currentDate.getMonth() + 1, 0);

          switch(controlMonthlyRecursEveryOption){
            case 1: // Day Of Week Ordinals
              for(let dayOfWeekOrdinal of controlMonthlyRecursEveryDayOfWeekOrdinals){
                // If the correct day of the week
                if(currentDate.getDay() === dayOfWeekOrdinal.dayOfWeek){
                  if(dayOfWeekOrdinal.ordinal >= 1 && dayOfWeekOrdinal.ordinal <= 4){
                    // First, Second, Third, or Fourth Week - Check if day is within specfic week
                    if(currentDate.getDate() > 7 * (dayOfWeekOrdinal.ordinal-1) && currentDate.getDate() <= 7 * dayOfWeekOrdinal.ordinal){
                      this.addToRecurrenceArray(currentDate);
                      break;
                    }
                  }else{
                    // Last Week - Check if within last 7 days of month i.e. the last week of the month
                    if(lastDayOfMonth.getDate() - currentDate.getDate() < 7){
                      this.addToRecurrenceArray(currentDate);
                      break;
                    }
                  }
                };
              }
              break;
            case 2: // Day Numbers
              for(let dayOfMonthNumber of controlMonthlyRecursEveryDayNumbers){
                // Add recurrence when currentDate day number matches 'dayOfMonthNumber'
                // Shifted lower to 'lastDayOfMonth' when out of given month's date range
                if(currentDate.getDate() === Math.min(dayOfMonthNumber, lastDayOfMonth.getDate())){
                  this.addToRecurrenceArray(currentDate);
                  break;
                }
              }
              break;
          }

          // Skip 'monthlyRecursEvery' amount of months once on the last day of the month
          // There is nuance here with how the Day number component of the Date is handled by this
          // We set to the start of the target month + 1, and '0' for the Day number, which turns into the last Day number of the target month
          // Eg currentDate = 2/28, monthlyRecursEvery = 2, goal is to be 3/31 (effectively skipping through all days of March, don't be worried either, the final changeDate call starts the next While loop at 4/1)
          //    Sets to 4/0 (April "0th" in a sense), which successfully turns into 3/31 
          //    Notice we got away with ignoring currentDate's day number of 28 by doing it this way
          if(lastDayOfMonth.getDate() === currentDate.getDate()){
            currentDate.setMonth(currentDate.getMonth() + controlMonthlyRecursEvery, 0);
          }
          break;
      }

      this.changeDate(currentDate, 1);
    }
    this.createdRecurrenceEvent.emit(this.recurrenceArray);
    this.closeflyPanel();
  }

  addToRecurrenceArray(currentDate: Date) {
    this.recurrenceArray.push({
      _startDate: new Date(
        currentDate.getFullYear(),
        currentDate.getMonth(),
        currentDate.getDate(),
        this.startDate.getHours(),
        this.startDate.getMinutes()
      ) ,
      _endDate: new Date(
        currentDate.getFullYear(),
        currentDate.getMonth(),
        currentDate.getDate() + this.startEndDateDifferenceInDays,
        this.endDate.getHours(),
        this.endDate.getMinutes()
      ),
      instructorName: this.dateInformation.instructorName,
      locationName: this.dateInformation.locationName,
      isClassPubliclyAvailable: this.dateInformation.isClassPubliclyAvailable
    });
  }

  changeDate(date: Date, days: number){
    date.setDate(date.getDate() + days);
  }

  closeflyPanel() {
    this.flyPanelSrvc.close();
  }

  getMonthlyRecursEveryDayOfWeekOrdinalsControls(){
    return (this.recurrenceForm.get('monthlyRecursEveryDayOfWeekOrdinals') as UntypedFormArray).controls;
  }

  getMonthlyRecursEveryDayNumbersControls(){
    return (this.recurrenceForm.get('monthlyRecursEveryDayNumbers') as UntypedFormArray).controls;
  }

  isSaveButtonDisabled(): boolean {
    let isSaveButtonDisabled = false;
    if(this.recurrenceForm.get('recurrenceType')?.value === 2 )
    {
      const weeklyDaysArray = this.recurrenceForm.get('weeklyDays') as UntypedFormArray;
      isSaveButtonDisabled = weeklyDaysArray.controls.every(control => !control.value);
    }
   return isSaveButtonDisabled;
  }

  getDaySuffix(day: number): string {
    if (day >= 11 && day <= 13) {
      return 'th';
    }
  
    switch (day % 10) {
      case 1:
        return 'st';
      case 2:
        return 'nd';
      case 3:
        return 'rd';
      default:
        return 'th';
    }
  }

  onNumericInputChange(event: any, control: AbstractControl): void {
    const inputValue = control.value;
    const min = event.target.min !== "" ? (parseInt(event.target.min, 10) ?? 1) : 1;
    const max = event.target.max !== "" ? (parseInt(event.target.max, 10) ?? Number.MAX_SAFE_INTEGER) : Number.MAX_SAFE_INTEGER;

    if (!isNaN(inputValue)) {
      const clampedValue = Math.min(max, Math.max(min, parseInt(inputValue, 10)));

      control.setValue(clampedValue);
    } else {
      console.error('Invalid input. Please enter a number.');
    }
  }
}
