import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-fly-panel-position-r6-task-information',
  templateUrl: './fly-panel-position-r6-task-information.component.html',
  styleUrls: ['./fly-panel-position-r6-task-information.component.scss']
})
export class FlyPanelPositionR6TaskInformationComponent implements OnInit {
  datePipe = new DatePipe('en-us');

  @Input() reason: string;
  @Input() effectiveDate: Date;
  @Output() closed = new EventEmitter<any>();

  effectiveDateString: string;

  constructor() { }

  ngOnInit(): void {
    this.effectiveDateString = this.datePipe.transform(this.effectiveDate, "yyyy-MM-dd");
  }

}
