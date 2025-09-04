import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Optional, Output } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { QTDDialogueComponent } from 'src/app/components/base/qtd-dialogue/qtd-dialogue.component';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-publish-student-evaluation',
  templateUrl: './fly-panel-publish-student-evaluation.component.html',
  styleUrls: ['./fly-panel-publish-student-evaluation.component.scss']
})
export class FlyPanelPublishStudentEvaluationComponent implements OnInit {
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
  @Output() canceled = new EventEmitter<any>();

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
}
