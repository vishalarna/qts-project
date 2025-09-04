import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TQReleasedToEMPVM } from 'src/app/_DtoModels/TaskQualification/TQReleasedToEMPVM';

@Component({
  selector: 'app-recall-task-qualification-dialog',
  templateUrl: './recall-task-qualification-dialog.component.html',
  styleUrls: ['./recall-task-qualification-dialog.component.scss']
})
export class RecallTaskQualificationDialogComponent implements OnInit {
  @Output() canceled = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();

  @Input() multiSelectData: TQReleasedToEMPVM[] = [];
  paginator: MatPaginator;
  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.setPaginator()
  }
  // @ViewChild('paginator') paginator!:MatPaginator;

  dataSource = new MatTableDataSource<TQReleasedToEMPVM>();
  displayedColumns = ["emp", "task", "evaluator"];
  constructor() { }

  ngOnInit(): void {
    this.readyData();
  }

  emitDate() {
    this.confirmed.emit();
    this.canceled.emit();
  }

  readyData() {
    this.dataSource.data = Object.assign(this.multiSelectData)
  }

  setPaginator() {
    this.dataSource.paginator = this.paginator;
  }

}
