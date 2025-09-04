import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingIssue_ActionItem_VM } from '@models/TrainingIssues/TrainingIssue_ActionItem_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';

@Component({
  selector: 'app-training-issues-review-and-publish',
  templateUrl: './training-issues-review-and-publish.component.html',
  styleUrls: ['./training-issues-review-and-publish.component.scss']
})
export class TrainingIssuesReviewAndPublishComponent implements OnInit {
  @Input() inputTrainingIssue_Vm: TrainingIssue_VM;
  dataSource: any;
  tableColumns: string[];
  @ViewChild('metaPaginator') set metaPaginator(paging: MatPaginator) {
    if (paging) this.dataSource.paginator = paging;
  }
  constructor() { }

  ngOnInit(): void {
    this.tableColumns = ['actionStep','assignedTo','priority','dateAssigned','dueDate', 'status' ];
    this.dataSource = new MatTableDataSource<TrainingIssue_ActionItem_VM>();
  }

}
