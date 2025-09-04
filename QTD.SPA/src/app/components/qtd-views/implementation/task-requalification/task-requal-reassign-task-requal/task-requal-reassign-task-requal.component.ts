import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { TQReleasedToEMPVM } from 'src/app/_DtoModels/TaskQualification/TQReleasedToEMPVM';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';

@Component({
  selector: 'app-task-requal-reassign-task-requal',
  templateUrl: './task-requal-reassign-task-requal.component.html',
  styleUrls: ['./task-requal-reassign-task-requal.component.scss']
})
export class TaskRequalReassignTaskRequalComponent implements OnInit {
  @Output() canceled = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();
  @Input() selectedData!: TQReleasedToEMPVM;
  datePipe = new DatePipe('en-us')
  releaseDate: any = this.datePipe.transform(Date.now(), 'yyyy-MM-dd');
  dueDate: any = this.datePipe.transform(Date.now(), 'yyyy-MM-dd');
  // This needs to be multiple ids
  evaluatorId = "";

  qualificationForm = new UntypedFormGroup({});

  evaluators: Employee[] = [];

  constructor(
    private employeeService : EmployeesService,
  ) { }

  ngOnInit(): void {
    this.qualificationForm.addControl('evaluatorId', new UntypedFormControl(''));
    this.qualificationForm.addControl('checkStarted', new UntypedFormControl('1'));
    this.qualificationForm.addControl('release', new UntypedFormControl(this.getDateStringFromUtc(this.selectedData.releaseDate,),Validators.required));
    this.qualificationForm.addControl('due', new UntypedFormControl(this.datePipe.transform(this.selectedData.dueDate,'yyyy-MM-dd'),Validators.required));
    this.qualificationForm.addControl('startTime',new UntypedFormControl({ value: this.getTimeFromUtc(this.selectedData.releaseDate), disabled: true }, Validators.required));
    this.readyEvaluators();
    this.setupTimeField('release', 'startTime');
  }

  async readyEvaluators() {
    this.evaluators = await this.employeeService.getAllEvaluators();
    var values = this.evaluators.filter((eval1) => {
      return this.selectedData.evaluatorIds.includes(eval1.id);
    })

    this.qualificationForm.get('evaluatorId')?.setValue(values);
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

  private setupTimeField(dateControlName: string, timeControlName: string): void {
    const dateControl = this.qualificationForm.get(dateControlName);
    const timeControl = this.qualificationForm.get(timeControlName);
  
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
    let startDateTime = `${this.qualificationForm.get('release')?.value}T${this.qualificationForm.get('startTime')?.value}`;
    this.confirmed.emit(JSON.stringify({
      evaluatorIds: this.qualificationForm.get('evaluatorId')?.value.map((data:Employee)=> data.id),
      releaseDate: startDateTime,
      dueDate: this.qualificationForm.get('due')?.value,
      checkStarted:this.qualificationForm.get('checkStarted')?.value
    }));
    this.canceled.emit();
  }

  removeData(data: Person) {
    var values = this.qualificationForm.get('evaluatorId')?.value.filter((per) => {
      return per.id !== data.id;
    })
    this.qualificationForm.get('evaluatorId')?.setValue(values)
  }

}
