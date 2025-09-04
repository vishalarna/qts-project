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
} from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-dialogue-unlink-r5-tasks',
  templateUrl: './dialogue-unlink-r5-tasks.component.html',
  styleUrls: ['./dialogue-unlink-r5-tasks.component.scss'],
})
export class DialogueUnlinkR5TasksComponent implements OnInit {
  constructor(
    @Optional() public mdDialogRef: MatDialogRef<DialogueUnlinkR5TasksComponent>,
    public dialog: MatDialog,
    private alert: SweetAlertService
  ) {}

  datePipe = new DatePipe('en-us');
  @Input() header: string;
  @Input() r5TasksToUnlink : any[] =[];
  @Input() description: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() defaultValue: string;
  @Input() reasonDescription: string;
  @Input() reasonName: string;
  @Input() reasonOnly:boolean = false;
  @Input() isHTML:boolean = false;
  @Input() detailsInfo:any;

  @Input() showEffectiveDateAndReason: boolean = false;
  @Output()
  canceled = new EventEmitter<any>();

  @Output()
  confirmed = new EventEmitter<any>();

  showReasonModal = false;

  r5TasksToUnlinkData = new MatTableDataSource<any>();
  effectiveDate =this.datePipe.transform(Date.now(), "yyyy-MM-dd");
  reason!: string;
  displayColumns: string[] = ['number', 'description'];

  ngOnInit(): void {
    this.r5TasksToUnlinkData = new MatTableDataSource(this.r5TasksToUnlink);
  }

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
