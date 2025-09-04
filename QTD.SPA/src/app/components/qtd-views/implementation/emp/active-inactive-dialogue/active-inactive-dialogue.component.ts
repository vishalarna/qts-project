import { DatePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {  UntypedFormBuilder } from '@angular/forms';
import {
  MatLegacyDialog as MatDialog,
} from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-active-inactive-dialogue',
  templateUrl: './active-inactive-dialogue.component.html',
  styleUrls: ['./active-inactive-dialogue.component.scss']
})
export class ActiveInactiveDialogueComponent implements OnInit {
  datePipe = new DatePipe('en-us');
  @Input() header: string;
  @Input() description: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() defaultValue: string;
  @Input() reasonDescription: string;
  @Input() reasonName: string;
  @Input() empActiveStatus:any;
  @Input() empIdActive:any;
  @Input() showEffectiveDateAndReason: boolean = false;
  @Output() confirmed = new EventEmitter<any>();
  @Output() canceled = new EventEmitter<any>();
  @Output() dataTransfer = new EventEmitter<any>();

  showReasonModal = false;

  effectiveDate =this.datePipe.transform(Date.now(), "yyyy-MM-dd");
  reason: any;
  
  constructor(
  public dialog: MatDialog, /*   @Inject(MAT_DIALOG_DATA) public dialogData: any */
  private fb: UntypedFormBuilder) { }

  ngOnInit(): void {
    
  }

  closeDialog() {
    this.canceled.emit('cancelled clicked');
    this.dialog.closeAll();
  }

  /* readyActiveForm() {
    this.ActiveForm = this.fb.group({

      locNumber: new FormControl('', Validators.required),
      effectiveDate: new FormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
    });
  } */

  onSaveReason(e: any) {
    /* if (!this.effectiveDate && !this.reason) {
      this.alert.errorToast('Please provide Effective date and reason');
      return;
    } */
      let data = {
        effectiveDate: this.effectiveDate,
        reason: this.reason,
      };
      this.dataTransfer.emit(JSON.stringify({data})); 
      this.showReasonModal = true;
    //this.dialog.closeAll();
  }

  async onConfirm(event: any) {
    if (!this.showEffectiveDateAndReason) {
      /*  await this.empService
       .delete(this.empIdActive, this.empActiveStatus.toLowerCase() == 'active' ? 'inactive' : 'active')
       .then((res)=>{
        this.alert.successToast("Employee" + this.empActiveStatus);
       }
       ); */
       this.confirmed.emit(true);
    }
    this.dialog.closeAll();
  }
}
