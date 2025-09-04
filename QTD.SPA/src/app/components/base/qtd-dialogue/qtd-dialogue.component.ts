import { DatePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Optional,
  Output,
} from '@angular/core';
import {
  MatLegacyDialog as MatDialog,
  MatLegacyDialogRef as MatDialogRef,
  MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA,
} from '@angular/material/legacy-dialog';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-qtd-dialogue',
  templateUrl: './qtd-dialogue.component.html',
  styleUrls: ['./qtd-dialogue.component.scss'],
})
export class QTDDialogueComponent implements OnInit {
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
  @Input() reasonOnly:boolean = false;
  @Input() isHTML:boolean = false;
  @Input() detailsInfo:any;
  @Input() isQuestion: boolean = true;

  @Input() showEffectiveDateAndReason: boolean = false;
  @Output()
  canceled = new EventEmitter<any>();

  @Output()
  confirmed = new EventEmitter<any>();

  showReasonModal = false;

  effectiveDate =this.datePipe.transform(Date.now(), "yyyy-MM-dd");
  reason!: string;

  ngOnInit(): void {}

  onConfirm(event: any) {
    if (!this.showEffectiveDateAndReason) this.confirmed.emit(event);
    else this.showReasonModal = true;
  }

  onSaveReason(e: any) {
    if (!this.effectiveDate || !this.reason) {
      //this.alert.errorToast('Please provide Effective date and reason');
      //return;
    }
    let data = {
      effectiveDate: this.effectiveDate,
      reason: this.reason,
    };
    this.confirmed.emit(JSON.stringify(data));
    this.dialog.closeAll();
  }

  closeDialog() {
    this.canceled.emit('cancelled clicked');
    this.dialog.closeAll();
  }

  isWhitespace = false;

  checkWhitespace(value: string) {
// check for white sapce
    this.isWhitespace = /^\s*$/.test(value);
  }
}
