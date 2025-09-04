import { Component, Input, OnInit } from '@angular/core';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';

@Component({
  selector: 'app-supporting-documents',
  templateUrl: './supporting-documents.component.html',
  styleUrls: ['./supporting-documents.component.scss']
})
export class SupportingDocumentsComponent implements OnInit {

  @Input() inputTaskListReviewVM : TaskListReview_VM ;
  constructor() { }

  ngOnInit(): void {
  }
  
}
