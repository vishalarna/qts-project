import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';

@Component({
  selector: 'app-conclusion-and-training',
  templateUrl: './conclusion-and-training.component.html',
  styleUrls: ['./conclusion-and-training.component.scss']
})
export class ConclusionAndTrainingComponent implements OnInit {
  @Input() inputTaskListReviewVM : TaskListReview_VM ;
  Editor = ckcustomBuild;
  publishTaskListReviewForm!: UntypedFormGroup ;
  constructor(private fb: UntypedFormBuilder,private datepipe: DatePipe,) { }

  ngOnInit(): void {
    this.initializePublishReview();
  }

  initializePublishReview(){
    this.publishTaskListReviewForm = this.fb.group({
      conclusion: new UntypedFormControl(this.inputTaskListReviewVM?.conclusion),
      signature: new UntypedFormControl(this.inputTaskListReviewVM?.signature),
      approvalDate: new UntypedFormControl(this.inputTaskListReviewVM?.approvalDate != null ? this.datepipe.transform(this.inputTaskListReviewVM?.approvalDate,'yyyy-MM-dd') :null),
    });
  }

  signatureChange() {
    let signature = this.publishTaskListReviewForm.get('signature').value;
    this.inputTaskListReviewVM.signature = signature;
  }

  conclusionChange() {
    let conclusion = this.publishTaskListReviewForm.get('conclusion').value;
    this.inputTaskListReviewVM.conclusion = conclusion;
  }

  approvalDateChange() {
    let approvalDate = this.publishTaskListReviewForm.get('approvalDate').value;
    this.inputTaskListReviewVM.approvalDate = approvalDate;
  }

}
