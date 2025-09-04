import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Optional, Output } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { QTDDialogueComponent } from 'src/app/components/base/qtd-dialogue/qtd-dialogue.component';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-planned-date-dialogue',
  templateUrl: './planned-date-dialogue.component.html',
  styleUrls: ['./planned-date-dialogue.component.scss']
})
export class PlannedDateDialogueComponent implements OnInit {

  constructor(
    @Optional() public mdDialogRef: MatDialogRef<QTDDialogueComponent>,
    public dialog: MatDialog /*   @Inject(MAT_DIALOG_DATA) public dialogData: any */,
    private alert: SweetAlertService
  ) {}

  datePipe = new DatePipe('en-us');
  @Input() header: string;
  @Input() description: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() defaultValue: string;
  @Input() reasonDescription: string;
  @Input() reasonName: string;

  @Input() showEffectiveDateAndReason: boolean = false;
  @Input() DateStart: any;
  @Input() IlaName: string = "";
  @Input() EmployeeName: string = "";
  @Output() canceled = new EventEmitter<any>();

  @Output()
  confirmed = new EventEmitter<any>();

  @Input() isPastClass:boolean = false;

  showReasonModal = false;

  effectiveDate :any = null;
  reason!: string;

  ngOnInit(): void {}

  onConfirm(event: any) {
    if (!this.showEffectiveDateAndReason) this.confirmed.emit({ef:this.effectiveDate?? ""});
    else this.showReasonModal = true;
  }

  onSaveReason(e: any) {
    this.confirmed.emit(this.effectiveDate);
    this.dialog.closeAll();
  }

  closeDialog() {
    this.canceled.emit('cancelled clicked');
    this.dialog.closeAll();
  }

}
