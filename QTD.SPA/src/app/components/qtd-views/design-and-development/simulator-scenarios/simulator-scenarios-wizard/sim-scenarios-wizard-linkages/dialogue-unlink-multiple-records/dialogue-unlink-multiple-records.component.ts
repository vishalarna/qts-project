import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Optional, Output} from '@angular/core';
import {MatLegacyDialog as MatDialog,MatLegacyDialogRef as MatDialogRef} from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Component({
  selector: 'app-dialogue-unlink-multiple-records',
  templateUrl: './dialogue-unlink-multiple-records.component.html',
  styleUrls: ['./dialogue-unlink-multiple-records.component.scss'],
})
export class DialogueUnlinkMultipleRecordsComponent implements OnInit {
  constructor(
    @Optional() public mdDialogRef: MatDialogRef<DialogueUnlinkMultipleRecordsComponent>,
    public dialog: MatDialog,
  ) {}

  datePipe = new DatePipe('en-us');
  @Input() header: string;
  @Input() recordsToUnlink : any[] =[];
  @Input() description: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() isQuestion: boolean = true;
  @Output() canceled = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();
  unlinkDataSource = new MatTableDataSource<any>();
  displayColumns: string[] = ['number', 'description'];

  ngOnInit(): void {
    this.unlinkDataSource = new MatTableDataSource(this.recordsToUnlink);
  }

  onConfirm(event: any) {
    this.confirmed.emit(event);
  }
  closeDialog() {
    this.canceled.emit('cancelled clicked');
  }
  
}
