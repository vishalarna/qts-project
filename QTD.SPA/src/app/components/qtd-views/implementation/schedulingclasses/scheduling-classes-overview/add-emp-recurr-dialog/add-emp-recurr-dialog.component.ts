import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import { TrainingService } from 'src/app/_Services/QTD/training.service';

@Component({
  selector: 'app-add-emp-recurr-dialog',
  templateUrl: './add-emp-recurr-dialog.component.html',
  styleUrls: ['./add-emp-recurr-dialog.component.scss']
})
export class AddEmpRecurrDialogComponent implements OnInit {
  @Input() classId = "";
  @Output() closed = new EventEmitter<any>();
  @Output() selected = new EventEmitter<any>();
  empForm = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  classes !: ClassSchedules[];
  constructor(
    private trService : TrainingService,
  ) { }

  ngOnInit(): void {
    this.empForm.addControl('data',new UntypedFormControl('',Validators.required));
    this.readyData();
  }

  async readyData(){
    this.classes = await this.trService.getRecurrenceEmployees(this.classId)
  }

  closeDialog(){
    this.closed.emit();
  }

  emitData(){
    var data = this.empForm.get('data')?.value;
    var modifiedDate = `${this.datePipe.transform(data.startDate,'MM-dd-yy')} - ${this.datePipe.transform(data.endDate,'MM-dd-yy')}`
    var modified = {id:data.id,date:modifiedDate};
    this.selected.emit(modified);
    this.closed.emit();
  }

}
