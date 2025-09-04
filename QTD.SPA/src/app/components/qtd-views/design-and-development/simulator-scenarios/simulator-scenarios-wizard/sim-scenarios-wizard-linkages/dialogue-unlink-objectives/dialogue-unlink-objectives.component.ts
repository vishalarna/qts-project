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
import {MatLegacyDialog as MatDialog,MatLegacyDialogRef as MatDialogRef} from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Component({
  selector: 'app-dialogue-unlink-objectives',
  templateUrl: './dialogue-unlink-objectives.component.html',
  styleUrls: ['./dialogue-unlink-objectives.component.scss'],
})
export class DialogueUnlinkObjectivesComponent implements OnInit {
  constructor(
    @Optional() public mdDialogRef: MatDialogRef<DialogueUnlinkObjectivesComponent>,
    public dialog: MatDialog,
  ) {}

  datePipe = new DatePipe('en-us');
  @Input() header: string;
  @Input() objectivesToUnlink : any[] =[];
  @Input() description: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() isQuestion: boolean = true;
  @Output() canceled = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();
  unlinkDataSource = new MatTableDataSource<any>();
  displayColumns: string[] = ['number','type', 'description'];

  ngOnInit(): void {
    this.unlinkDataSource = new MatTableDataSource(this.objectivesToUnlink);
  }

  onConfirm(event: any) {
    this.confirmed.emit(event);
  }
  closeDialog() {
    this.canceled.emit('cancelled clicked');
  }
  
}
