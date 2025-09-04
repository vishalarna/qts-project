import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit,Optional, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import { QTDDialogueComponent } from 'src/app/components/base/qtd-dialogue/qtd-dialogue.component';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-enrollment-date-dialogue',
  templateUrl: './enrollment-date-dialogue.component.html',
  styleUrls: ['./enrollment-date-dialogue.component.scss']
})
export class EnrollmentDateDialogueComponent implements OnInit {

  constructor(
    @Optional() public mdDialogRef: MatDialogRef<QTDDialogueComponent>,
    public dialog: MatDialog /*   @Inject(MAT_DIALOG_DATA) public dialogData: any */,
    private alert: SweetAlertService
  ) {}


  dateForm: UntypedFormGroup = new UntypedFormGroup({
    endDate: new UntypedFormControl('', Validators.required),
    startDate: new UntypedFormControl('', [Validators.required]),
  });
  datePipe = new DatePipe('en-us');
 
  @Input() selectedClassitem: any;

  @Output() canceled = new EventEmitter<any>();

  @Output()
  confirmed = new EventEmitter<any>();
  ngOnInit(): void {
    this.setFormCurrentValues();
  }

  setFormCurrentValues(){
    
    this.dateForm.get("endDate")?.setValue(this.datePipe.transform(this.selectedClassitem.endDate, 'yyyy-MM-dd'));
    this.dateForm.get("startDate")?.setValue(this.datePipe.transform(this.selectedClassitem.startDate, 'yyyy-MM-dd'));
  }
  

  submitFormData() {
   this.confirmed.emit({startDate:this.dateForm.get("startDate")?.value,endDate:this.dateForm.get("endDate")?.value});
   this.dialog.closeAll();
  
  }


  closeDialog() {
    this.canceled.emit('cancelled clicked');
    this.dialog.closeAll();
  }
}
