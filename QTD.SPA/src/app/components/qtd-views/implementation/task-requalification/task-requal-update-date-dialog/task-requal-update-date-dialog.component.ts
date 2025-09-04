import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TQReleasedToEMPVM } from 'src/app/_DtoModels/TaskQualification/TQReleasedToEMPVM';

@Component({
  selector: 'app-task-requal-update-date-dialog',
  templateUrl: './task-requal-update-date-dialog.component.html',
  styleUrls: ['./task-requal-update-date-dialog.component.scss']
})
export class TaskRequalUpdateDateDialogComponent implements OnInit {
  @Output() canceled = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();
  @Input() selectedData!:TQReleasedToEMPVM;
  datePipe = new DatePipe('en-us')
  updateDatesForm!: UntypedFormGroup;

  constructor(private fb: UntypedFormBuilder) { }

  ngOnInit(): void {
    this.intializeForm();
    this.setupTimeField('releaseDate', 'startTime');
  }

  intializeForm(){
    this.updateDatesForm = this.fb.group({
      releaseDate: [this.getDateStringFromUtc(this.selectedData.releaseDate), Validators.required],
      dueDate: [this.getFormattedDate(this.selectedData.dueDate), Validators.required],
      startTime: new UntypedFormControl({ value: this.getTimeFromUtc(this.selectedData.releaseDate), disabled: true }, Validators.required),
    });
  }

 convertUtcTimeToLocalTime(datetime: any): Date {
    var startDateString = this.datePipe.transform(datetime,'yyyy-MM-dd hh:mm a');
    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    return newdatetime;
  }

  getTimeFromUtc(datetime: any): string {
    const localDate = this.convertUtcTimeToLocalTime(datetime);
    return localDate.toLocaleTimeString('en-GB', {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,
    });
  }

  getDateStringFromUtc(datetime: any): string | null {
    const localDate: Date = this.convertUtcTimeToLocalTime(datetime);
    return this.datePipe.transform(localDate, 'yyyy-MM-dd');
  }

  getFormattedDate(date: string | Date): string {
    return this.datePipe.transform(date, 'yyyy-MM-dd')!;
  }

  private setupTimeField(dateControlName: string, timeControlName: string): void {
      const dateControl = this.updateDatesForm.get(dateControlName);
      const timeControl = this.updateDatesForm.get(timeControlName);
    
      dateControl?.valueChanges.subscribe(date => {
        if (date) {
          if (!timeControl?.value) {
            timeControl?.setValue('08:00');
          }
          timeControl?.enable();
        } else {
          timeControl?.reset();
          timeControl?.disable();
        }
      });
      const initialDate = dateControl?.value;
      if (initialDate) {
        if (!timeControl?.value) {
          timeControl?.setValue('08:00');
        }
        timeControl?.enable();
      }
    }

  emitDate() {
    let startDateTime = `${this.updateDatesForm.get('releaseDate')?.value}T${this.updateDatesForm.get('startTime')?.value}`;
     this.confirmed.emit(JSON.stringify({
       releaseDate: startDateTime,
       dueDate: this.updateDatesForm.get('dueDate')?.value
     }));
    this.canceled.emit();
  }

}
